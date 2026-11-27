using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Normal,
    Knockbacked,
}
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public static PlayerState Player_State { get; set; }

    [Header("Move")]
    public float MoveSpeed = 5f;
    private float xMovement;

    [Header("Jumb")]
    public float JumbForce = 6f;
    public int MaxJumb = 2;
    private int JumbLeft;

    [Header("Dash")]
    public float DashSpeed = 5f;
    public float DashDuration = 0.2f;
    public float DashCoolDown = 0.2f;
    private bool CanDash = true;
    private bool IsDashing = false;

    [Header("CheckGround")]
    public Transform GroundCheck;
    public Vector2 GroundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask GroundLayer;

    [Header("Gravity")]
    public float BaseGravity = 2f;
    public float MaxFallingSpeed = 20f;
    public float GravityIncreading = 0.7f;

    [Header("Animation")]
    public Animator animator;
    public bool isRunning = false;

    [Header("GhostSpawn")]
    public bool TurnGhostEffect = false;
    public GhostSpawner ghostSpawner;

    [Header("Practicle System Effects")]
    public bool TurnParticleSystem = false;
    public ParticleSystem nparticleSystem;

    // tác dụng của trọng lực
    private void Gravity()
    {
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale += GravityIncreading * Time.deltaTime;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -MaxFallingSpeed));
        }
        else
        {
            rb.gravityScale = BaseGravity;
        }
    }


    // đổi hướng nhìn khi di chuyển
    private int FacingPositiveDirection = 1;
    private void Flip()
    {
        if (xMovement > 0) FacingPositiveDirection = 1;
        else if (xMovement < 0) FacingPositiveDirection = -1;
        transform.localScale = new Vector3(FacingPositiveDirection, transform.localScale.y, transform.localScale.z);
    }

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        PlayerCollider2D = gameObject.GetComponent<Collider2D>();
    }
    private void Start()
    {
        Player_State = PlayerState.Normal;
    }
    private void Update()
    {
        if (Player_State != PlayerState.Normal) return;

        if (OpenedDialouge()) return;

        if (!IsDashing) rb.linearVelocity = new Vector2(xMovement * MoveSpeed, rb.linearVelocityY);

        animator.SetBool("isGround", CheckGrounded() != null);
        Gravity();
        Flip();
        if (TurnGhostEffect) CallGhost();
        if (TurnParticleSystem) CallParticleSystem();

        if (rb.linearVelocityX != 0 && Mathf.Abs(rb.linearVelocityY) < 0.01f) isRunning = true;
        else isRunning = false;
        animator.SetBool("isRunning", isRunning);
    }
    private bool OpenedDialouge()
    {
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsTalking)
        {
            rb.linearVelocity = Vector2.zero;

            return true;
        }
        return false;
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (CanDash)
        {
            _ = Dashing();
        }
    }
    private async Task Dashing()
    {
        IsDashing = true;
        CanDash = false;
        Physics2D.IgnoreLayerCollision(6, 9, true);
        ghostSpawner.isMakingGhost = true;

        int Direction = (transform.localScale.x >= 0) ? 1 : -1;
        rb.linearVelocity = new Vector2(Direction * DashSpeed, rb.linearVelocityY);

        await Awaitable.WaitForSecondsAsync(DashDuration);

        IsDashing = false;
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        Physics2D.IgnoreLayerCollision(6, 9, false);
        ghostSpawner.isMakingGhost = false;

        await Awaitable.WaitForSecondsAsync(DashCoolDown);

        CanDash = true;
    }

    // di chuyển theo chiều ngang
    public void Move(InputAction.CallbackContext callbackContext)
    {
        xMovement = callbackContext.ReadValue<Vector2>().x;
    }

    // nhảy
    public void Jumb(InputAction.CallbackContext callbackContext)
    {
        if (JumbLeft > 0)
        {
            if (callbackContext.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, JumbForce);
                JumbLeft--;
                if (!OpenedDialouge())
                {
                    animator.SetTrigger("jumb");
                }
            }
        }
    }
    // xuống khỏi platform
    [Header("DropFromPlatform")]
    public Collider2D PlayerCollider2D;
    public float TimeDropThourgh = 0.4f;
    private bool IsDropingFromPlatform = false;
    public void DropDown(InputAction.CallbackContext context)
    {
        Collider2D PlatFormCollider2D = CheckGrounded();
        if (context.performed && PlatFormCollider2D && !IsDropingFromPlatform)
        {
            _ = Drop(PlatFormCollider2D);
        }
    }
    private async Task Drop(Collider2D PlatFormCollider2D)
    {
        IsDropingFromPlatform = true;
        Physics2D.IgnoreCollision(PlayerCollider2D, PlatFormCollider2D, true);

        await Awaitable.WaitForSecondsAsync(TimeDropThourgh);

        Physics2D.IgnoreCollision(PlayerCollider2D, PlatFormCollider2D, false);
        IsDropingFromPlatform = false;
    }


    // check vị tri mặt đất và chân
    private Collider2D CheckGrounded()
    {
        Collider2D hits = Physics2D.OverlapBox(GroundCheck.position, GroundCheckSize, 0, GroundLayer);
        if (hits != null)
        {
            JumbLeft = MaxJumb;
            return hits;
        }
        else return null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(GroundCheck.position, GroundCheckSize);
    }

    // tạo bóng ma khi chuyển động
    private void CallGhost()
    {
        if (rb.linearVelocityX != 0 || rb.linearVelocityY != 0)
        {
            ghostSpawner.isMakingGhost = true;
        }
        else ghostSpawner.isMakingGhost = false;
    }

    // tạo particleSystem đơn giản
    private void CallParticleSystem()
    {
        if (rb.linearVelocityX != 0 || rb.linearVelocityY != 0) nparticleSystem.Play();
    }
    private void ResetTransform()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }

    private void OnEnable()
    {
        GameControllerScript.GameHasReset += ResetTransform;
    }
    private void OnDisable()
    {
        GameControllerScript.GameHasReset -= ResetTransform;
    }
}
