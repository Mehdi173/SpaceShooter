using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private Transform gun;
    [SerializeField] private BulletBehaviour bulletPrefab;
    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private EnemyData enemyData;
    
    private float difficultyMultiplier;
    private float nextShotTime;

    private void Awake()
    {
        healthBehaviour = GetComponent<HealthBehaviour>();
    }

    public void SetEnemyData(EnemyData enemyData, float difficultyMultiplier)
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
            BulletPool.Instance.SpawnBullet(gun.position, gun.rotation);
            
            // increasing the enemies fire rate after wave 6 to make the waves more difficult
            nextShotTime = Time.time + enemyData.firingDelayTime / difficultyMultiplier;
        }
    }
}
