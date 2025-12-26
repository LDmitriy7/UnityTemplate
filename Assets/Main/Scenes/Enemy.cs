using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void TakeDamage()
    {
        health--;
        if (health <= 0) Destroy(gameObject);
    }
}
