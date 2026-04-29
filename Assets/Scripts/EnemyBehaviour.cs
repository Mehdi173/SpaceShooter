using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform gun;
    [SerializeField] private BulletBehaviour bulletPrefab;
    [SerializeField] private float delayTime;
    
    private float nextShotTime;
    
    private void Update()
    {
        if (Time.time > nextShotTime)
        {
            Instantiate(bulletPrefab, gun.position, gun.rotation);
            nextShotTime = Time.time + delayTime;
        }
        // transform.Translate(Vector3.up * speed * Time.deltaTime);
    
        // if (Time.time > nextShotTime)
        // {
        //     Instantiate(bulletPrefab, gun.position, gun.rotation);
        //     nextShotTime = Time.time + delayTime;
        // }
    }
}
