using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    private Coroutine _flashCoroutine;
    private Color _color;

    private void Awake()
    {
        _color = spriteRenderer.color;
    }

    public void TakeDamage()
    {
        health--;
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashCoroutine());
        if (health <= 0) Destroy(gameObject);
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = _color;
        _flashCoroutine = null;
    }
}
