using System.Threading.Tasks;
using UnityEngine;

public class DropablePlatform : MonoBehaviour
{
    [Header("DropSetting")]
    public float WaitTime = 1f;
    public float DestroyTime = 3f;

    private bool IsFalling = false;
    private Rigidbody2D rb;
    private Collider2D ThisCollider2D;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ThisCollider2D = rb.GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsFalling && collision.gameObject.CompareTag("Player"))
        {
            _ = Drop();
        }
    }
    private async Task Drop()
    {
        IsFalling = true;

        await Awaitable.WaitForSecondsAsync(WaitTime);
        rb.bodyType = RigidbodyType2D.Dynamic;
        ThisCollider2D.enabled = false;

        Destroy(gameObject,DestroyTime);
    }
}
