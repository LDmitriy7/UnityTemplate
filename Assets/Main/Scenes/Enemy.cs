using System.Collections;
using UnityEngine;

// TODO: squash player when shoot

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private float squashDuration = 0.1f;
    [SerializeField] private float squashFactor = 1.2f;
    private Coroutine _flashCoroutine;
    private Coroutine _squashCoroutine;
    private Color _originalcolor;
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalcolor = spriteRenderer.color;
        _originalScale = transform.localScale;
    }

    public void TakeDamage()
    {
        health--;
        Flash();
        Squash();
        if (health <= 0) Destroy(gameObject);
    }

    public void Flash()
    {
        if (_flashCoroutine != null) StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    public void Squash()
    {
        if (_squashCoroutine != null) StopCoroutine(_squashCoroutine);
        _squashCoroutine = StartCoroutine(SquashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = _originalcolor;
        _flashCoroutine = null;
    }

    private IEnumerator SquashCoroutine()
    {
        float stretchFactor = squashFactor == 0 ? 1f : 1f / squashFactor;
        var endScale = new Vector3
        (
            _originalScale.x * squashFactor,
            _originalScale.y * stretchFactor,
            _originalScale.z
        );
        var halfDuration = squashDuration / 2;
        yield return LerpScale(transform.localScale, endScale, halfDuration);
        yield return LerpScale(endScale, _originalScale, halfDuration);
        _squashCoroutine = null;
    }

    private IEnumerator LerpScale(Vector3 from, Vector3 to, float duration)
    {
        float startTime = Time.time;
        while (true)
        {
            float elapsed = Time.time - startTime;
            if (elapsed >= duration) break;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localScale = Vector3.Lerp(from, to, t);
            yield return null;
        }
        transform.localScale = to;
    }
}
