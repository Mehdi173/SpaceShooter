using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float xAxisSpawnRange = 8f;
    [SerializeField] private float yAxisSpawnPosition = 5f;
    [SerializeField] private float spawnDelay;

    private float xAxisSpawnPosition;
    private Vector2 spawnPosition;
    private int randomPrefabIndex;

    public void SpawnPowerUp()
    {
        StartCoroutine(SpawnPowerUpAfterDelay());
    }

    private IEnumerator SpawnPowerUpAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        randomPrefabIndex = Random.Range(0, powerUpPrefabs.Length);
        xAxisSpawnPosition = Random.Range(0 - xAxisSpawnRange, xAxisSpawnRange);
        spawnPosition = new Vector2(xAxisSpawnPosition, yAxisSpawnPosition);
        Instantiate(powerUpPrefabs[randomPrefabIndex], spawnPosition, Quaternion.identity);
    }
}
