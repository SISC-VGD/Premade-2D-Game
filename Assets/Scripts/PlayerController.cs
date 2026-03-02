using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private uint _jumpCount;
    [SerializeField] private LayerMask _groundLayer;

    private SpriteRenderer _rndr;
    private Rigidbody2D _rb;
    private Animator _anim;

    private Vector2 _movement;
    private uint _currentJumpCount;

    private bool IsGrounded() => Physics2D.BoxCast(transform.position - Vector3.up, new Vector2(0.9f, 0.1f), 0, Vector2.zero, 100, _groundLayer);

    private void Awake()
    {
        _rndr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetBool("IsGrounded", IsGrounded());
        _anim.SetFloat("YVelocity", _rb.linearVelocityY);

        _rb.linearVelocityX = _movement.x * _movementSpeed;

        if (IsGrounded())
        {
            if (_movement.x > 0)
                _rndr.flipX = false;
            else if (_movement.x < 0)
                _rndr.flipX = true;

            _currentJumpCount = _jumpCount - 1;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>();

        _anim.SetFloat("X", _movement.x);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed || _currentJumpCount == 0)
            return;

        _rb.linearVelocityY = _jumpForce;
        _anim.SetTrigger("Jump");

        --_currentJumpCount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - Vector3.up, new Vector2(0.9f, 0.1f));
    }
}
