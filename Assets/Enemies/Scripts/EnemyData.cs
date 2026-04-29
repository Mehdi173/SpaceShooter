using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public int maxHealth;
    public float firingDelayTime;
}
