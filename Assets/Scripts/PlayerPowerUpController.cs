using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInputBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
public class PlayerPowerUpController : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject;

    private PlayerInputBehaviour playerInputBehaviour;
    private HealthBehaviour healthBehaviour;

    private void Awake()
    {
        playerInputBehaviour = GetComponent<PlayerInputBehaviour>();
        healthBehaviour = GetComponent<HealthBehaviour>();
    }

    public void ApplyPowerUp(PowerUpData powerUpData)
    {
        switch(powerUpData.type)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(ApplySpeedBoost(powerUpData));
                break;
            case PowerUpType.FireRateBoost:
                StartCoroutine(ApplyFireRateBoost(powerUpData));
                break;
            case PowerUpType.Shield:
                StartCoroutine(ApplyShield(powerUpData));
                break;
        }
    }

    private IEnumerator ApplySpeedBoost(PowerUpData powerUpData)
    {
        playerInputBehaviour.SetSpeedMultiplier(powerUpData.multiplier);
        yield return new WaitForSeconds(powerUpData.duration);
        playerInputBehaviour.SetSpeedMultiplier(1f);
    }

    private IEnumerator ApplyFireRateBoost(PowerUpData powerUpData)
    {
        playerInputBehaviour.SetFireRateMultiplier(powerUpData.multiplier);
        yield return new WaitForSeconds(powerUpData.duration);
        playerInputBehaviour.SetFireRateMultiplier(1f);
    }

    private IEnumerator ApplyShield(PowerUpData powerUpData)
    {
        shieldObject.SetActive(true);
        healthBehaviour.UpdateIsShielded(true);
        yield return new WaitForSeconds(powerUpData.duration);
        healthBehaviour.UpdateIsShielded(false);
        shieldObject.SetActive(false);
    }
}
