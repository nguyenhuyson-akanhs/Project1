using UnityEngine;
// StateMachine + RayCast;
public class EnemyAI_FSM : MonoBehaviour
{
    // Định nghĩa các trạng thái của AI bằng Enum
    private enum AIState
    {
        Patrol, // Đi tuần tra qua lại
        Chase   // Đuổi theo người chơi
    }

    [Header("Trạng Thái Hiện Tại")]
    [SerializeField] private AIState currentState = AIState.Patrol;

    [Header("Cấu Hình Di Chuyển")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    private bool isMovingRight = true;

    [Header("Cảm Biến (Raycast Settings)")]
    public Transform groundCheckPoint; // Điểm đặt ở dưới chân, dịch ra phía trước mặt quái
    public Transform wallCheckPoint;   // Điểm đặt ở ngang hông, dịch ra phía trước mặt quái
    public float checkDistance = 0.5f; // Khoảng cách quét của tia check đất/tường
    public float sightDistance = 5f;   // Tầm nhìn xa để phát hiện Player
    public LayerMask groundLayer;      // Layer của nền đất
    public LayerMask playerLayer;      // Layer của Player

    private Rigidbody2D rb;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tự động tìm kiếm Player thông qua Tag "Player"
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    void Update()
    {
        // 1. Liên tục kiểm tra xem Player có lọt vào tầm nhìn không
        bool canSeePlayer = CheckForPlayer();

        // 2. Bộ điều khiển trạng thái (State Machine)
        switch (currentState)
        {
            case AIState.Patrol:
                PatrolBehavior();

                // Điều kiện chuyển trạng thái: Nếu thấy Player -> Chuyển sang Đuổi theo
                if (canSeePlayer)
                    currentState = AIState.Chase;
                break;

            case AIState.Chase:
                ChaseBehavior();

                // Điều kiện chuyển trạng thái: Nếu mất dấu Player -> Quay lại Tuần tra
                if (!canSeePlayer)
                    currentState = AIState.Patrol;
                break;
        }
    }

    // --- LOGIC HÀNH VI ---

    // Hành vi 1: Tuần tra
    private void PatrolBehavior()
    {
        // Di chuyển AI tiến về phía trước
        float currentSpeed = isMovingRight ? patrolSpeed : -patrolSpeed;
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        // Bắn tia xuống dưới: Kiểm tra phía trước còn đất không (tránh lao xuống vực)
        bool hasGroundAhead = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, checkDistance, groundLayer);

        // Bắn tia ra trước: Kiểm tra xem có đụng tường không
        Vector2 moveDirection = isMovingRight ? Vector2.right : Vector2.left;
        bool isHittingWall = Physics2D.Raycast(wallCheckPoint.position, moveDirection, checkDistance, groundLayer);

        // Nếu hết đất HOẶC đụng tường -> Quay đầu lại
        if (!hasGroundAhead || isHittingWall)
        {
            FlipX();
        }
    }

    // Hành vi 2: Đuổi theo Player
    private void ChaseBehavior()
    {
        if (playerTransform == null) return;

        // Xác định hướng của Player so với quái (Trái hay Phải)
        float directionToPlayer = playerTransform.position.x - transform.position.x;

        // Quay mặt quái về hướng của Player
        if (directionToPlayer > 0 && !isMovingRight) FlipX();
        else if (directionToPlayer < 0 && isMovingRight) FlipX();

        // Di chuyển về phía Player với tốc độ nhanh hơn
        float currentSpeed = isMovingRight ? chaseSpeed : -chaseSpeed;
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);
    }

    // --- LOGIC CẢM BIẾN ---

    // Kiểm tra xem Player có nằm trong tầm nhìn thẳng của quái không
    private bool CheckForPlayer()
    {
        if (playerTransform == null) return false;

        // Bắn một tia Raycast dài thẳng ra trước mặt theo hướng đang nhìn
        Vector2 lookDirection = isMovingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheckPoint.position, lookDirection, sightDistance, playerLayer);

        // Nếu tia va chạm trúng vào Layer của Player
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    // Hàm quay mặt AI (Lật trục X)
    private void FlipX()
    {
        isMovingRight = !isMovingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Vẽ các đường tia trong tab Scene giúp bạn dễ dàng căn chỉnh khoảng cách bằng mắt
    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null || wallCheckPoint == null) return;

        // Màu đỏ: Tia quét mặt đất (Check vực)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * checkDistance);

        // Màu xanh dương: Tia quét tường trước mặt
        Gizmos.color = Color.blue;
        Vector3 faceDir = isMovingRight ? Vector3.right : Vector3.left;
        Gizmos.DrawLine(wallCheckPoint.position, wallCheckPoint.position + faceDir * checkDistance);

        // Màu xanh lá: Tầm nhìn phát hiện người chơi
        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallCheckPoint.position, wallCheckPoint.position + faceDir * sightDistance);
    }
}