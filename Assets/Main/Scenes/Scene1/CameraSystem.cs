using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public static CameraSystem Instance { get; private set; }

    [SerializeField] private new Camera camera;
    private float shakeIntensity = 0f;
    private float shakeTimer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float intensity, float time)
    {
        shakeIntensity = intensity;
        shakeTimer = time;
    }

    void Update()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        var position = camera.transform.localPosition;
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            var randomPoint = Random.insideUnitCircle * shakeIntensity;
            camera.transform.localPosition = new Vector3(randomPoint.x, randomPoint.y, position.z);
        }
        else
        {
            camera.transform.localPosition = new Vector3(0, 0, position.z);
        }
        // else if (position.x != 0 || position.y != 0)
        // {
        //     var targetPosition = new Vector3(0, 0, position.z);
        //     // camera.transform.localPosition = Vector3.MoveTowards(position, targetPosition, Time.deltaTime);
        //     camera.transform.localPosition = targetPosition;
        // }
    }
}
