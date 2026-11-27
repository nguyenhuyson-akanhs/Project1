using System;
using System.Threading.Tasks;
using UnityEngine;

public class EnemisInfo : MonoBehaviour, EnemisController
{
    [Header("EnemisInfo")]
    public int EnemisDamage = 1;
    public int EnemisMaxHealth = 3;
    public int EnemisCurrentHealth = 3;

    //public static event Action EnemyDead;
    private void Start()
    {
        EnemisCurrentHealth = EnemisMaxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Bullet>(out var bullet))
        {
            EnemisCurrentHealth -= bullet.BulletDamage;
        }
        if (EnemisCurrentHealth <= 0)
        {
            if(TryGetComponent<EnemisDrop>(out var enemisDrop)) enemisDrop.Drop();
            //EnemyDead?.Invoke();

            _=  DeadAction();
        }
    }
    private async Task DeadAction()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        await Awaitable.WaitForSecondsAsync(0.1f);

        Destroy(gameObject);
    }
}
