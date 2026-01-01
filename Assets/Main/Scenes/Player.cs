using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float maxSpeed = 8f;
    [SerializeField] private float moveForce = 0.5f;
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private float recoilForce = 5f;
    [SerializeField] private Square square;
    [SerializeField] private ParticleSystem jumpEffect;
    private bool _canJump = false;
    private const string groundTag = "Ground";
    private float moveInput;
    private bool jumpRequested;
    private bool shootRequested;

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            shootRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (moveInput != 0)
        {
            body.linearVelocityX = Mathf.MoveTowards(body.linearVelocityX, moveInput * maxSpeed, moveForce);
        }
        if (jumpRequested)
        {
            jumpRequested = false;
            if (_canJump) Jump();
        }
        if (shootRequested)
        {
            shootRequested = false;
            Shoot();
        }
    }

    void Shoot()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePos - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(projectilePrefab, transform.position, rotation);
        square.Squash();
        square.Knockback(-direction, recoilForce);
    }

    void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpEffect.Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            _canJump = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            _canJump = false;
        }
    }
}