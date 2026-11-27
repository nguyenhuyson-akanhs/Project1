using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletDamage = 1;
    private void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(ReturnToGunBarrel), 2f);
    }
    private void Update()
    {
        transform.Rotate(0, 0, 720 * Time.deltaTime);
    }
    private void ReturnToGunBarrel()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemisInfo>(out var enemisInfo))
        {
            ReturnToGunBarrel();
        }
    }
}
