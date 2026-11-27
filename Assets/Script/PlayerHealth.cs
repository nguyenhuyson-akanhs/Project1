using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour
{
    public int MaxHeart = 5;
    public int CurrentHeart;
    public HealthUI healthUI;
    private SpriteRenderer spriteRenderer;
    public static event Action OnDeath;
    private void Start()
    {
        CurrentHeart = MaxHeart; // số tim ban đầu;
        healthUI.SetMaxHearts(CurrentHeart);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemisInfo>(out var enemisInfo)) OnTakeDamage(enemisInfo.EnemisDamage);
    }
    private void OnTakeDamage(int DamageDeal)
    {
        ObjectShaker.CallObjectShake("Healthbar", 5, 0.1f);
        _ = FlashOnHit();

        CurrentHeart -= DamageDeal;
        healthUI.UpdateHeart(CurrentHeart);
        if (CurrentHeart <= 0) OnDeath.Invoke();
    }
    private async Task FlashOnHit()
    {
        spriteRenderer.color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.1f);
        spriteRenderer.color = Color.white;
    }
    private void PlayerHealthReset()
    {
        CurrentHeart = MaxHeart;
        healthUI.UpdateHeart(CurrentHeart);
    }
    private void CollectedHeart(int HealValue)
    {
        CurrentHeart = Mathf.Min(CurrentHeart + HealValue, MaxHeart);
        healthUI.UpdateHeart(CurrentHeart);
    }
    private void OnEnable()
    {
        GameControllerScript.GameHasReset += PlayerHealthReset;
        HeartLoot.HeartCollected += CollectedHeart;
    }
    private void OnDisable()
    {
        GameControllerScript.GameHasReset -= PlayerHealthReset;
    }
}
