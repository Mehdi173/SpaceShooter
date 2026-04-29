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
    
    private float difficultyMultiplier;
    private float nextShotTime;

    private void Awake()
    {
        healthBehaviour = GetComponent<HealthBehaviour>();
    }

    public void setEnemyData(EnemyData enemyData, float difficultyMultiplier)
    {
        this.enemyData = enemyData;
        this.difficultyMultiplier = difficultyMultiplier;
        healthBehaviour.InitialiseHealth(this.enemyData.maxHealth);
        nextShotTime = 0f;
    }
    
    private void Update()
    {
        if (enemyData != null && Time.time > nextShotTime)
        {
            Instantiate(bulletPrefab, gun.position, gun.rotation);
            // increasing the enemies fire rate after wave 6 to make the waves more difficult
            nextShotTime = Time.time + enemyData.firingDelayTime / difficultyMultiplier;
        }
        // transform.Translate(Vector3.up * speed * Time.deltaTime);
    
        // if (Time.time > nextShotTime)
        // {
        //     Instantiate(bulletPrefab, gun.position, gun.rotation);
        //     nextShotTime = Time.time + delayTime;
        // }
    }
}
