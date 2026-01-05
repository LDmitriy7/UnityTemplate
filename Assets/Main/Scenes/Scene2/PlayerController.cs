using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] float moveForce = 50f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float friction = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] Vector2 groundCheckOffset = new(0f, -1f);
    [SerializeField] Vector2 groundCheckSize = new(0.9f, 0.2f);
    [SerializeField] LayerMask groundLayer;
    private float _currentInput;
    private bool _jumpRequested;
    private bool _isGrounded;
    private const float InputThreshold = 0.01f;

    void Update()
    {
        _currentInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        CheckGround();
        Move();
        ApplyFriction();
        ClampSpeed();

        if (_jumpRequested)
        {
            if (_isGrounded)
            {
                Jump();
            }
            _jumpRequested = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        var boxCenter = (Vector2)transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(boxCenter, groundCheckSize);
    }

    private void Move()
    {
        if (Mathf.Abs(_currentInput) > InputThreshold)
        {
            var force = new Vector2(_currentInput * moveForce, 0f);
            body.AddForce(force);
        }
    }

    private void ApplyFriction()
    {
        // if (!_isGrounded) return; // TODO
        bool isTurning = (_currentInput * body.linearVelocity.x) < 0;
        if (Mathf.Abs(_currentInput) < InputThreshold || isTurning)
        {
            var force = new Vector2(-body.linearVelocity.x * friction, 0f);
            body.AddForce(force);
        }
    }

    private void ClampSpeed()
    {
        var velocity = body.linearVelocity.x;
        if (Mathf.Abs(velocity) > maxSpeed)
        {
            body.linearVelocityX = maxSpeed * Mathf.Sign(velocity);
        }
    }

    private void CheckGround()
    {
        var origin = (Vector2)transform.position + groundCheckOffset;
        _isGrounded = Physics2D.BoxCast(origin, groundCheckSize, 0f, Vector2.down, 0f, groundLayer);
    }

    private void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
