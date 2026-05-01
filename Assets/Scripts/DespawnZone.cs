using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    private BulletBehaviour bulletBehaviour;
    private PowerUpBehaviour powerUpBehaviour;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bulletBehaviour = collision.GetComponent<BulletBehaviour>();
        if (bulletBehaviour)
        {
            BulletPool.Instance.Release(bulletBehaviour);
        } else if (collision.GetComponent<PowerUpBehaviour>())
        {
            collision.gameObject.SetActive(false);
        }
    }
}
