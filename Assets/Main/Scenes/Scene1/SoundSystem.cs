using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Play(AudioClip clip, float pitch = 1f, float volume = 1f)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, clip.length / pitch);
    }
}
