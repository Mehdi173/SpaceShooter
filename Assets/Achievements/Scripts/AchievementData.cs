using UnityEngine;

public enum AchievementType
{
    KillEnemies,
    ReachWave,
    CollectPowerUps
}

[CreateAssetMenu(fileName = "AchievementData", menuName = "Scriptable Objects/AchievementData")]
public class AchievementData : ScriptableObject
{
    public string achievementName;
    public AchievementType type;
    public int targetValue;
}
