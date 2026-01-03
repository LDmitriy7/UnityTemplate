using UnityEngine;

public class EffectsSettings : MonoBehaviour
{
  public static EffectsSettings Instance { get; private set; }

  public bool colorFlashEnabled = true;
  public bool squashEnabled = true;
  public bool particlesEnabled = true;
  public bool knockbackEnabled = true;
  public bool cameraShakeEnabled = true;
  public bool soundsEnabled = true;

  private void Awake()
  {
    Instance = this;
  }
}
