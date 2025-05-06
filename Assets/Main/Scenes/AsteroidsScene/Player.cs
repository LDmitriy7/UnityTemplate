using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float thrustForce = 10f;
    [SerializeField, Range(0, 1)] private float dampingFactor = 0.6f;
    [SerializeField, Min(0)] private float minDamping = 1f;
    [SerializeField] private float rotationSpeed = 200f;

    private Camera _camera;
    private float _moveSpeed;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Rotate();
        Translate();
    }

    private void Rotate()
    {
        var input = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward, -input * rotationSpeed * Time.deltaTime);
    }

    private void Translate()
    {
        var input = Input.GetAxis("Vertical");
        _moveSpeed += Mathf.Clamp01(input) * thrustForce * Time.deltaTime;
        if (_moveSpeed < minDamping * Time.deltaTime) _moveSpeed = 0;
        else _moveSpeed *= Mathf.Pow(1f - dampingFactor, Time.deltaTime);

        var distance = _moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * distance);

        var bounds = spriteRenderer.bounds;
        var viewportExtentY = _camera.orthographicSize;
        var viewportExtentX = viewportExtentY * _camera.aspect;

        var position = transform.position;
        position.x = WrapPositionAxis(position.x, bounds.center.x, bounds.extents.x, viewportExtentX);
        position.y = WrapPositionAxis(position.y, bounds.center.y, bounds.extents.y, viewportExtentY);
        transform.position = position;
    }

    private static float WrapPositionAxis
    (
        float axis,
        float boundsCenter,
        float boundsExtent,
        float viewportExtent
    )
    {
        var wrapDistance = viewportExtent + boundsExtent;
        if (boundsCenter - boundsExtent > viewportExtent) return -wrapDistance;
        if (boundsCenter + boundsExtent < -viewportExtent) return wrapDistance;
        return axis;
    }
}