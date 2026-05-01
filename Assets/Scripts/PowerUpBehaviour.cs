using UnityEngine;
using UnityEngine.Events;

public class PowerUpBehaviour : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData;
    private AchievementManager achievementManager;
    public UnityEvent onPowerUpCollected;

    private void OnEnable()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        if (achievementManager)
        {
            onPowerUpCollected.RemoveListener(achievementManager.HandlePowerUpCollected);
            onPowerUpCollected.AddListener(achievementManager.HandlePowerUpCollected);
        }
    }

    private void OnDisable()
    {
        if (achievementManager)
        {
            onPowerUpCollected.RemoveListener(achievementManager.HandlePowerUpCollected);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPowerUpController playerPowerUpController = collision.GetComponent<PlayerPowerUpController>();
        if (playerPowerUpController != null)
        {
            playerPowerUpController.ApplyPowerUp(powerUpData);
            onPowerUpCollected?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
