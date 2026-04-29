using UnityEngine;

public enum PowerUpType
{
    SpeedBoost,
    FireRateBoost,
    Shield
}

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Scriptable Objects/PowerUpData")]
public class PowerUpData : ScriptableObject
{
    public PowerUpType type;
    public GameObject powerUpPrefab;
    public float duration;
    public float multiplier;
}
