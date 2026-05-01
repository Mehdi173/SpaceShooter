using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemyData smallUnitData;
    [SerializeField] private EnemyData largeUnitData;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private PowerUpSpawner powerUpSpawner;
    [SerializeField] private AchievementManager achievementManager;
    [SerializeField] private WaveUI waveUI;
    [SerializeField] private SFXPlayer sfxPlayer;
    [SerializeField] private AudioClip deathExplosionClip;
    [SerializeField] private AudioClip newWaveAlertClip;

    private int currentWaveIndex;
    private int enemyCount;
    private int enemiesAliveCount;
    private EnemyData currentEnemyData;
    private string enemyTypesString;

    private void Start()
    {
        enemyCount = spawnPositions.Length;
        currentWaveIndex = 1;
        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        enemyTypesString = GetEnemyTypesString();
        waveUI.UpdateUIWaveDetails(currentWaveIndex, enemyTypesString);
        sfxPlayer.PlaySound(newWaveAlertClip);
        yield return waveUI.ShowNewWaveAnnouncement(currentWaveIndex, enemyTypesString);

        achievementManager.HandleWaveReached(currentWaveIndex);
        enemiesAliveCount = enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            if ((i + currentWaveIndex) <= enemyCount)
            {
                currentEnemyData = smallUnitData;
            } else
            {
                currentEnemyData = largeUnitData;
            }
            GameObject enemyObject = Instantiate(currentEnemyData.enemyPrefab, spawnPositions[i].position, spawnPositions[i].rotation);
            enemyObject.GetComponent<EnemyBehaviour>()?.SetEnemyData(currentEnemyData, GetDifficultyMultiplier());

            HealthBehaviour healthBehaviour = enemyObject.GetComponent<HealthBehaviour>();
            // removing the listener that was attached to the object in the previous wave
            healthBehaviour?.onEnemyDeath.RemoveListener(OnEnemyDeath);
            healthBehaviour?.onEnemyDeath.AddListener(OnEnemyDeath);

            healthBehaviour?.onEnemyDeath.RemoveListener(achievementManager.HandleEnemyKilled);
            healthBehaviour?.onEnemyDeath.AddListener(achievementManager.HandleEnemyKilled);
        }
        powerUpSpawner.SpawnPowerUp();
    }

    private string GetEnemyTypesString()
    {
        if (currentWaveIndex == 1)
        {
            return "Small";
        } else if (currentWaveIndex <= enemyCount)
        {
            return "Small + Large";
        } else
        {
            return "Large";
        }
    }

    // Calculates the difficulty multiplier that gets applied after wave 6 to ensure the waves become more difficult
    private float GetDifficultyMultiplier()
    {
        return 1f + Mathf.Max(0, currentWaveIndex - 6) * 0.1f;
    }

    private void OnEnemyDeath()
    {
        sfxPlayer.PlaySound(deathExplosionClip);
        
        enemiesAliveCount--;
        if (enemiesAliveCount <= 0)
        {
            StartCoroutine(DelayedNextWaveSpawn());
        }
    }

    private IEnumerator DelayedNextWaveSpawn()
    {   
        yield return new WaitForSeconds(3f);
        
        currentWaveIndex++;
        StartCoroutine(SpawnWaveRoutine());
    }
}
