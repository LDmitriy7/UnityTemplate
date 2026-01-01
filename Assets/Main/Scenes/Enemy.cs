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
    private Coroutine _flashCoroutine;
    private Color _originalcolor;

    private void Awake()
    {
        _originalcolor = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        health--;
        Flash();
        square.Squash();
        if (health <= 0)
        {
            Destroy(gameObject);
            var effect = Instantiate(deathEffectPrefab);
            effect.transform.position = transform.position;
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
