using System.Collections;
using UnityEngine;

public class SquareSprite : MonoBehaviour
{
    [SerializeField] private float squashDuration = 0.1f;
    [SerializeField] private float squashFactor = 1.2f;
    private Vector3 _originalScale;
    private Coroutine _squashCoroutine;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    public void Squash()
    {
        if (_squashCoroutine != null) StopCoroutine(_squashCoroutine);
        _squashCoroutine = StartCoroutine(SquashCoroutine());
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
