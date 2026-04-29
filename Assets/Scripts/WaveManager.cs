using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemyData smallUnitData;
    [SerializeField] private EnemyData largeUnitData;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private PowerUpSpawner powerUpSpawner;

    private int currentWaveIndex;
    private int enemyCount;
    private int enemiesAliveCount;
    private EnemyData currentEnemyData;

    private void Start()
    {
        enemyCount = spawnPositions.Length;
        currentWaveIndex = 1;
        SpawnWave();
    }

    // need to think of how to add the following waves, also need the logic to increate difficulty after

    private void SpawnWave()
    {
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
            enemyObject.GetComponent<EnemyBehaviour>()?.setEnemyData(currentEnemyData, GetDifficultyMultiplier());
            // removing the listener that was attached to the object in the previous wave
            enemyObject.GetComponent<HealthBehaviour>()?.onDeath.RemoveListener(OnEnemyDeath);
            enemyObject.GetComponent<HealthBehaviour>()?.onDeath.AddListener(OnEnemyDeath);
        }
        enemiesAliveCount = enemyCount;
        powerUpSpawner.SpawnPowerUp();
    }

    // Calculates the difficulty multiplier that gets applied after wave 6 to ensure the waves become more difficult
    private float GetDifficultyMultiplier()
    {
        return 1f + Mathf.Max(0, currentWaveIndex - 6) * 0.1f;
    }

    private void OnEnemyDeath()
    {
        enemiesAliveCount--;
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
        SpawnWave();
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
        //                 enemyBehaviour.setEnemyData(currentWave);
        //             }
        //         }
        //     }
        // }
}
