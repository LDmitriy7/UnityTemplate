using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float knockbackForce = 1f;
    private float _startTime;

    void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
        if (Time.time - _startTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: hit not only Enemy
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.TakeDamage();
            enemy.Knockback(transform.right, knockbackForce);
            Instantiate(effectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
