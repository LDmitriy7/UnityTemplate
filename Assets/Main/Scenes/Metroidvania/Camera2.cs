using UnityEngine;

public class Camera2 : MonoBehaviour
{
    [SerializeField] Transform target;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = target.position + _offset;
    }
}
