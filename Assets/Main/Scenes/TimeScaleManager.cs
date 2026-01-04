using System.Collections;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{
  public static TimeScaleManager Instance { get; private set; }
  private Coroutine hitStopCoroutine;
  private float normalTimeScale = 1f;

  private void Awake()
  {
    Instance = this;
  }

  public void HitStop(float duration, float timeScale)
  {
    if (hitStopCoroutine != null)
    {
      StopCoroutine(hitStopCoroutine);
      Time.timeScale = normalTimeScale;
    }
    hitStopCoroutine = StartCoroutine(HitStopCoroutine(duration, timeScale));
  }

  private IEnumerator HitStopCoroutine(float duration, float timeScale)
  {
    normalTimeScale = Time.timeScale;
    Time.timeScale = timeScale;
    yield return new WaitForSecondsRealtime(duration);
    Time.timeScale = normalTimeScale;
  }
}
