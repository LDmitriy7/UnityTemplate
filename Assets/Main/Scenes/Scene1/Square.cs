using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private SquareSprite sprite;

    public void Squash()
    {
        sprite.Squash();
    }

    public void Knockback(Vector2 direction, float force)
    {
        body.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
