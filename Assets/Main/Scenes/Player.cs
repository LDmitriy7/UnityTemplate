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
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
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