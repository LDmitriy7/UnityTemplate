using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject projectilePrefab;
    private bool _canJump = false;
    private const string groundTag = "Ground";

    void Update()
    {
        var input = Input.GetAxis("Horizontal");
        body.linearVelocityX = 5f * input;
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePos - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(projectilePrefab, transform.position, rotation);
    }

    void Jump()
    {
        body.AddForce(Vector2.up * 300f);
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