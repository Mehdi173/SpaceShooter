using UnityEngine;

public class DespawnZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // might need to separate the checks/conditions later when using pooling
        if (collision.GetComponent<BulletBehaviour>() || collision.GetComponent<PowerUpBehaviour>())
        {
            // Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
