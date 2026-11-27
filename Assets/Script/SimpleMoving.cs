using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SimpleMoving : MonoBehaviour
{
    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;

    [Header("MoveOption")]
    public float _Speed = 3f;
    private Vector2 _Movement;
    private bool _IsRunning = false;
    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        _Rigidbody2D.linearVelocity = new Vector2(_Movement.x * _Speed, _Rigidbody2D.linearVelocityY);
        transform.parent.gameObject.GetComponent<Rigidbody2D>().linearVelocity = _Rigidbody2D.linearVelocity;

        if(_IsRunning) Flip();
    }
    public void Move(InputAction.CallbackContext context)
    {
        _Movement = context.ReadValue<Vector2>();

        if (context.performed && _IsRunning == false) _IsRunning = true;
        else if (context.canceled) _IsRunning = false;

        _Animator.SetBool("_IsRunning", _IsRunning);
    }
    private void Flip()
    {
        int _direction = (_Movement.x >= 0) ? 1 : -1;
        transform.parent.gameObject.transform.localScale = new Vector3(_direction, 1, 1);
    }
}
