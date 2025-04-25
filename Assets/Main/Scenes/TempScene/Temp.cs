using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float turnSpeed = 10f;

    private void Update()
    {
        var direction = Input.GetAxis("Vertical");
        var rotation = -Input.GetAxis("Horizontal");

        direction *= Time.deltaTime * speed;
        rotation *= Time.deltaTime * turnSpeed;

        transform.Rotate(0, 0, rotation);
        transform.Translate(0, direction, 0);
    }
}