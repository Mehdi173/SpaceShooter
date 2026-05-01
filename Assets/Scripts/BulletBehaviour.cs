using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void InitialiseBullet()
    {
        rb2D.linearVelocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthBehaviour health = collision.GetComponent<HealthBehaviour>();
        if (health)
        {
            health.AddHealth(-damage);
            BulletPool.Instance.Release(this);
        }
    }
}
