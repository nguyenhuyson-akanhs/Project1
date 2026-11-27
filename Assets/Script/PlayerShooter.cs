
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerShooter : MonoBehaviour
{
    [Header("BulletOption")]
    public GameObject BulletPrefabs;
    public int PoolSize = 10;
    private GameObject BulletPool;
    private List<GameObject> Bullets = new();
    public float BulletSpeed;

    [Header("Camera")]
    public Camera MainCamera;

    private Collider2D myCollider2D;

    private void Awake()
    {
        GenerateBulletPool();
        myCollider2D = GetComponent<Collider2D>();
    }
    private void GenerateBulletPool()
    {
        BulletPool = new GameObject("Bullet_Pool_Container");
        Bullets.Clear();

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(BulletPrefabs, BulletPool.transform);
            obj.SetActive(false);
            Bullets.Add(obj);
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started && GameControllerScript.gameStatus == GameStatus.Continues)
        {
            GameObject bullet = GetTheBulletFromPool();

            if (bullet == null) { Debug.Log("Hết đạn òi"); return; }

            Vector3 MousePos = GetMousePos();
            Vector3 Direction = (MousePos - gameObject.transform.position).normalized;

            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Physics2D.IgnoreCollision(myCollider2D, bullet.GetComponent<Collider2D>());

            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = (Vector2)Direction * BulletSpeed;
        }
    }
    private Vector3 GetMousePos()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        float distanceToCamera = Mathf.Abs(MainCamera.transform.position.z);
        Vector3 screenPosWithDepth = new Vector3(mouseScreenPos.x, mouseScreenPos.y, distanceToCamera);

        Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(screenPosWithDepth);
        mouseWorldPos.z = 0f;

        return mouseWorldPos;
    }
    private GameObject GetTheBulletFromPool()
    {
        for (int i = 0; i < PoolSize; i++)
        {
            if (Bullets[i].activeInHierarchy == false) return Bullets[i];
        }
        return null;
    }
}
