using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private Rigidbody2D rb2D;
    // private Vector2 newVelocity;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // newVelocity = Vector2.up * speed;
        // rb2D.linearVelocity = newVelocity;
        rb2D.linearVelocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBehaviour health = collision.GetComponent<HealthBehaviour>();
        if (health) health.AddHealth(-damage);
        // Debug.Log("Bullet hit!");
        gameObject.SetActive(false);
    }
}
