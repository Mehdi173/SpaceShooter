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

    // need to think of how to add the following waves, also need the logic to increate difficulty after

    private IEnumerator SpawnWaveRoutine()
    {
        enemyTypesString = GetEnemyTypesString();
        waveUI.UpdateUIWaveDetails(currentWaveIndex, enemyTypesString);
        yield return waveUI.ShowNewWaveAnnouncement(currentWaveIndex, enemyTypesString);

        achievementManager.HandleWaveReached(currentWaveIndex);
        enemiesAliveCount = enemyCount;
        for (int i = 0; i < enemyCount; i++)
        {
            // Debug.Log(i);
            if ((i + currentWaveIndex) <= enemyCount)
            {
                currentEnemyData = smallUnitData;
            } else
            {
                currentEnemyData = largeUnitData;
            }
            GameObject enemyObject = Instantiate(currentEnemyData.enemyPrefab, spawnPositions[i].position, spawnPositions[i].rotation);
            enemyObject.GetComponent<EnemyBehaviour>()?.SetEnemyData(currentEnemyData, GetDifficultyMultiplier());
            // removing the listener that was attached to the object in the previous wave
            HealthBehaviour healthBehaviour = enemyObject.GetComponent<HealthBehaviour>();
            // enemyObject.GetComponent<HealthBehaviour>()?.onEnemyDeath.RemoveListener(OnEnemyDeath);
            // enemyObject.GetComponent<HealthBehaviour>()?.onEnemyDeath.AddListener(OnEnemyDeath);
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
        // notify achievement maybe?
        // achievementManager.HandleEnemyKilled();
        if (enemiesAliveCount <= 0)
        {
            // SpawnWave();
            StartCoroutine(DelayedNextWaveSpawn());
        }
    }

    private IEnumerator DelayedNextWaveSpawn()
    {
        Debug.Log("Wave finished");
        // maybe I trigger UI stuff here?? like "Next Wave Incoming ..." or something like that?
        
        yield return new WaitForSeconds(3f);
        
        currentWaveIndex++;
        // SpawnWave();
        StartCoroutine(SpawnWaveRoutine());
    }
        // [SerializeField] private EnemyData[] waveData;
        // [SerializeField] private Transform[] spawnPoints;
    
        // private int currentWaveIndex;
    
        // private void Start()
        // {
        //     currentWaveIndex = 0;
        //     SpawnWave();
        // }
    
        // private void SpawnWave()
        // {
        //     if (currentWaveIndex < waveData.Length)
        //     {
        //         EnemyData currentWave = waveData[currentWaveIndex];
        //         foreach (Transform spawnPoint in spawnPoints)
        //         {
        //             GameObject enemy = Instantiate(currentWave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        //             EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        //             if (enemyBehaviour != null)
        //             {
        //                 enemyBehaviour.SetEnemyData(currentWave);
        //             }
        //         }
        //     }
        // }
}
