using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    private BulletBehaviour bulletBehaviour;
    private PowerUpBehaviour powerUpBehaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // might need to separate the checks/conditions later when using pooling
        bulletBehaviour = collision.GetComponent<BulletBehaviour>();
        if (bulletBehaviour)
        {
            // Destroy(collision.gameObject);
            // collision.gameObject.SetActive(false);
            BulletPool.Instance.Release(bulletBehaviour);
        } else if (collision.GetComponent<PowerUpBehaviour>())
        {
            // Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
