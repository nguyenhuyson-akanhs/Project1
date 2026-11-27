using System.Threading.Tasks;
using UnityEngine;
public enum HidingState
{
    Facing,
    Hiding,
}

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HidenObject : MonoBehaviour
{
    private HidingState state;

    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    private Color color;

    [Header("HidenObjectOption")]
    public float Radius = 5f;
    public float HidingTime = 2f;
    public float TimeBetween = 0.01f;
    private float CurrentTime = 0f;
    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        state = HidingState.Hiding;

        circleCollider2D.isTrigger = true;
        circleCollider2D.radius = Radius;
        color = spriteRenderer.color;
        SetColor(0);

    }
    private void SetColor(float alpha)
    {
        if (alpha > 1) alpha = 1;
        if (alpha < 0) alpha = 0;
        color.a = alpha;
        spriteRenderer.color = color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = HidingState.Facing;
            _ = Facing();
        }
    }
    private async Task Facing()
    {
        while (CurrentTime <= HidingTime)
        {
            if (state == HidingState.Hiding) break;
            SetColor(CurrentTime / HidingTime);

            await Awaitable.WaitForSecondsAsync(TimeBetween);
            CurrentTime += TimeBetween;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = HidingState.Hiding;
            _ = Hiding();
        }
    }
    private async Task Hiding()
    {
        while (CurrentTime >= 0f)
        {
            if (state == HidingState.Facing) break;
            SetColor(CurrentTime / HidingTime);

            await Awaitable.WaitForSecondsAsync(TimeBetween);
            CurrentTime -= TimeBetween;
        }
    }
}
