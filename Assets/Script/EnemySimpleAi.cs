using UnityEngine;
// idiot things lol
public class EnemySimpleAi : MonoBehaviour
{
    public Transform PlayerTransform;
    public LayerMask PlayerLayer;
    public LayerMask GroundLayer;
    public Rigidbody2D rb;
    public bool IsGround;
    public bool IsMoving;
    public float SpeedChase = 2f;
    public float JumbForce = 2f;
    public bool ShouldJump = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        IsGround = Physics2D.Raycast(transform.position, Vector2.down, 1f, GroundLayer);
        IsMoving = (rb.linearVelocityX != 0 || rb.linearVelocityY != 0) ? true : false;
        float Direction = Mathf.Sign(PlayerTransform.position.x - transform.position.x);

        if (IsGround)
        {
            rb.linearVelocity = new Vector2(Direction * SpeedChase, rb.linearVelocityY);

            bool GroundInFront = Physics2D.Raycast(transform.position, new Vector2(Direction, 0), 2f,GroundLayer);
            bool GroundInAbove = Physics2D.Raycast(transform.position, Vector2.up, 2f,GroundLayer);
            bool GroundInFrontMove = Physics2D.Raycast(transform.position + new Vector3(Direction, 0, 0), Vector2.down,2f, GroundLayer);
            bool PlayerInAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << PlayerLayer);

            if (!GroundInFront || !GroundInFrontMove) ShouldJump = true; // phía trước không có vật cản và có hố
            else if (PlayerInAbove && GroundInAbove) ShouldJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (IsGround && ShouldJump)
        {
            Debug.Log("ok");
            ShouldJump = false;
            Vector2 Direction = (transform.position - PlayerTransform.position).normalized;
            Vector2 JumpDirection = Direction * JumbForce;

            rb.AddForce(new Vector2(JumpDirection.x, JumbForce), ForceMode2D.Impulse);
        }
    }
}