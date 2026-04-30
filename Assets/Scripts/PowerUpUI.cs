using UnityEngine;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private TMP_Text powerUpStatusText;

    public void ClearPowerUpStatus()
    {
        powerUpStatusText.text = "Power-Up: None";
    }

    public void UpdatePowerUpStatus(string powerUpDisplayName, float remainingTime)
    {
        // powerUpStatusText.SetText("Power-Up: {0} {1:0.0}s", powerUpDisplayName, remainingTime);
        powerUpStatusText.text = $"Power-Up: {powerUpDisplayName} {remainingTime:0.0}s";
    }
}
