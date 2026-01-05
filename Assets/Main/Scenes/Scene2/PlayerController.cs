using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] float moveForce = 50f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float friction = 5f;
    private float _currentInput;
    private const float InputThreshold = 0.01f;

    void Update()
    {
        _currentInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
        ApplyFriction();
        ClampSpeed();
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
}
