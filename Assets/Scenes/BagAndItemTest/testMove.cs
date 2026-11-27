using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class testMove : MonoBehaviour
{
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = Vector3.right;
        float x = transform.position.x;
        x %= 7;
        transform.position = new Vector3(x, 0, 0);
    }
}
