using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private Square square;
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float pitch1;
    private Coroutine _flashCoroutine;
    private Color _originalcolor;

    private void Awake()
    {
        _originalcolor = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        health--;
        if (EffectsSettings.Instance.colorFlashEnabled) Flash();
        if (EffectsSettings.Instance.squashEnabled) square.Squash();
        if (health <= 0)
        {
            if (EffectsSettings.Instance.soundsEnabled)
                SoundSystem.Instance.Play(deathSound, pitch1);
            Destroy(gameObject);
            if (EffectsSettings.Instance.particlesEnabled)
            {
                var effect = Instantiate(deathEffectPrefab);
                effect.transform.position = transform.position;
            }
        }
    }

    public void Knockback(Vector2 direction, float force)
    {
        square.Knockback(direction, force);
    }

    public void Flash()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = _originalcolor;
        _flashCoroutine = null;
    }
}
