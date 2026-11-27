using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("SetPos")]
    private Rigidbody2D rb;

    public Transform PointA;
    public Transform PointB;
    public float Speed = 2f;

    private Vector3 NextPos;
    private bool IsQuit = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        transform.position = PointA.position;
        NextPos = PointB.transform.position;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, NextPos, Speed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, NextPos) < 0.001f)
        {
            if (NextPos == PointB.position)
            {
                NextPos = PointA.position;
            }
            else
            {
                NextPos = PointB.position;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsQuit) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsQuit) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private void OnApplicationQuit()
    {
        IsQuit = true;
    }
}
