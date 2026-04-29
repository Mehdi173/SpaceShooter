using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform gun;
    // will prob keep this prefab here instead of EnemyData, as for now all enemies shoot the exact same bullet
    [SerializeField] private BulletBehaviour bulletPrefab;
    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private EnemyData enemyData;
    // [SerializeField] private float delayTime;
    
    private float nextShotTime;

    private void Awake()
    {
        healthBehaviour = GetComponent<HealthBehaviour>();
    }

    public void setEnemyData(EnemyData enemyData)
    {
        this.enemyData = enemyData;
        healthBehaviour.InitialiseHealth(this.enemyData.maxHealth);
    }
    
    private void Update()
    {
        if (enemyData != null && Time.time > nextShotTime)
        {
            Instantiate(bulletPrefab, gun.position, gun.rotation);
            nextShotTime = Time.time + enemyData.firingDelayTime;
        }
        // transform.Translate(Vector3.up * speed * Time.deltaTime);
    
        // if (Time.time > nextShotTime)
        // {
        //     Instantiate(bulletPrefab, gun.position, gun.rotation);
        //     nextShotTime = Time.time + delayTime;
        // }
    }
}
