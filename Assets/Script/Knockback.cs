using System.Threading.Tasks;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float KnockbackForce = 10f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;

            Vector2 knockbackDirection = new Vector2(Mathf.Sign(direction.x), Mathf.Abs(direction.y)).normalized;

            _ = ApplyKnockBack(knockbackDirection);
        }
    }

    private async Task ApplyKnockBack(Vector2 Direction)
    {
        rb.linearVelocity = Vector2.zero;
        PlayerMovement.Player_State = PlayerState.Knockbacked;

        if (GameControllerScript.gameStatus == GameStatus.Continues) rb.AddForce(Direction * KnockbackForce, ForceMode2D.Impulse);

        await Awaitable.WaitForSecondsAsync(0.5f);
        PlayerMovement.Player_State = PlayerState.Normal;

    }
}
