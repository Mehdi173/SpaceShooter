using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemyData smallUnitData;
    [SerializeField] private EnemyData largeUnitData;
    [SerializeField] private Transform[] spawnPositions;

    private int currentWaveIndex;
    private int enemyCount;
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
            enemyObject.GetComponent<EnemyBehaviour>()?.setEnemyData(currentEnemyData);
        }
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
