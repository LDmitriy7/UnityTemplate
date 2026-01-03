using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float knockbackForce = 1f;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float shakeIntensity1 = 0.1f;
    [SerializeField] private float shakeDuration1 = 0.1f;

    void Start()
    {
        body.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            return;
        }
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage();
            enemy.Knockback(transform.right, knockbackForce);
        }
        CameraSystem.Instance.Shake(shakeIntensity1, shakeDuration1);
        Instantiate(effectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
