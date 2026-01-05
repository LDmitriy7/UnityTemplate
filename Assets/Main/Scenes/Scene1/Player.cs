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
    [SerializeField] private AudioClip shootSound;
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

    [SerializeField] private float minPitch1 = 0.8f;
    [SerializeField] private float maxPitch1 = 1.2f;
    [SerializeField] private float minVolume1 = 0.1f;
    [SerializeField] private float maxVolume1 = 0.2f;

    void Shoot()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = (mousePos - transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, angle);
        Instantiate(projectilePrefab, transform.position, rotation);
        if (EffectsSettings.Instance.squashEnabled) square.Squash();
        if (EffectsSettings.Instance.knockbackEnabled) square.Knockback(-direction, recoilForce);
        var pitch = Random.Range(minPitch1, maxPitch1);
        var volume = Random.Range(minVolume1, maxVolume1);
        if (EffectsSettings.Instance.soundsEnabled)
            SoundSystem.Instance.Play(shootSound, pitch, volume);
    }

    void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (EffectsSettings.Instance.particlesEnabled) jumpEffect.Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            _canJump = true;
            // cameraSystem.Shake(shakeIntensity, shakeDuration);
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