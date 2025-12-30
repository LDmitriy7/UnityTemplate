using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles2;

    private void Start()
    {
        var rotation = Random.Range(0f, 360f);
        particles2.transform.eulerAngles = new Vector3(0f, 0f, rotation);
        particles2.Play();
        Destroy(gameObject, 5f);
    }
}
