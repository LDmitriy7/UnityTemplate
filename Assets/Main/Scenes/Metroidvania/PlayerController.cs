using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] float moveForce = 50f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float friction = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float fallGravityMultiplier = 2f;
    [SerializeField] Vector2 groundCheckOffset = new(0f, -1f);
    [SerializeField] Vector2 groundCheckSize = new(0.9f, 0.2f);
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpBufferDuration = 0.1f;
    [SerializeField] float jumpCutMultiplier = 0.5f;
    private float _currentInput;
    private float _jumpBufferTimer;
    private bool _isGrounded;
    private float _defaultGravityScale;
    private const float InputThreshold = 0.01f;
    private const KeyCode JumpKey = KeyCode.Space;
    [SerializeField] private bool _isJumping;

    void Awake()
    {
        _defaultGravityScale = body.gravityScale;
    }

    void Update()
    {
        _currentInput = Input.GetAxisRaw("Horizontal");
        _jumpBufferTimer -= Time.deltaTime;
        if (Input.GetKeyDown(JumpKey))
        {
            _jumpBufferTimer = jumpBufferDuration;
        }
        if (Input.GetKeyUp(JumpKey))
        {
            _isJumping = false;
        }
    }

    void FixedUpdate()
    {
        CheckGround();
        Move();
        ApplyFriction();
        ClampSpeed();
        if (_jumpBufferTimer > 0f && _isGrounded)
        {
            Jump();
        }
        if (_isGrounded && body.linearVelocity.y <= 0f)
        {
            _isJumping = false;
        }
        UpdateGravity();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        var boxCenter = (Vector2)transform.position + groundCheckOffset;
        Gizmos.DrawWireCube(boxCenter, groundCheckSize);
    }

    private void UpdateGravity()
    {
        if (body.linearVelocity.y < 0f)
        {
            body.gravityScale = _defaultGravityScale * fallGravityMultiplier;
        }
        else if (body.linearVelocity.y > 0f && !_isJumping)
        {
            body.linearVelocityY *= jumpCutMultiplier;
        }
        else
        {
            body.gravityScale = _defaultGravityScale;
        }
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
        var point = (Vector2)transform.position + groundCheckOffset;
        _isGrounded = Physics2D.OverlapBox(point, groundCheckSize, 0f, groundLayer);
    }

    private void Jump()
    {
        body.linearVelocityY = jumpForce;
        _isJumping = true;
        _jumpBufferTimer = 0f;
    }
}
