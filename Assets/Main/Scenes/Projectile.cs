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
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private float minVolume1 = 1f;
    [SerializeField] private float maxVolume1 = 1f;
    [SerializeField] private float minPitch1 = 1f;
    [SerializeField] private float maxPitch1 = 1f;

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
            if (EffectsSettings.Instance.knockbackEnabled) enemy.Knockback(transform.right, knockbackForce);
        }
        if (EffectsSettings.Instance.cameraShakeEnabled)
        {
            CameraSystem.Instance.Shake(shakeIntensity1, shakeDuration1);
        }
        if (EffectsSettings.Instance.particlesEnabled)
        {
            Instantiate(effectPrefab, transform.position, transform.rotation);
        }
        if (EffectsSettings.Instance.soundsEnabled)
        {
            var pitch = Random.Range(minPitch1, maxPitch1);
            var volume = Random.Range(minVolume1, maxVolume1);
            SoundSystem.Instance.Play(hitSound, pitch, volume);
        }
        Destroy(gameObject);
    }
}
