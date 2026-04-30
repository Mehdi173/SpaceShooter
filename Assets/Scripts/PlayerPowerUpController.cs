using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInputBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
public class PlayerPowerUpController : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject;
    [SerializeField] private PowerUpUI powerUpUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip powerUpCollectClip;
    [SerializeField] private AudioClip powerUpExpireClip;

    private PlayerInputBehaviour playerInputBehaviour;
    private HealthBehaviour healthBehaviour;

    private void Awake()
    {
        playerInputBehaviour = GetComponent<PlayerInputBehaviour>();
        healthBehaviour = GetComponent<HealthBehaviour>();
    }

    public void ApplyPowerUp(PowerUpData powerUpData)
    {
        audioSource.PlayOneShot(powerUpCollectClip);
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
        // playerInputBehaviour.SetSpeedMultiplier(powerUpData.multiplier);
        // yield return new WaitForSeconds(powerUpData.duration);
        // playerInputBehaviour.SetSpeedMultiplier(1f);
        playerInputBehaviour.SetSpeedMultiplier(powerUpData.multiplier);
        float remainingTime = powerUpData.duration;
        while (remainingTime > 0)
        {
            powerUpUI.UpdatePowerUpStatus(powerUpData.powerUpDisplayName, remainingTime);
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        playerInputBehaviour.SetSpeedMultiplier(1f);
        audioSource.PlayOneShot(powerUpExpireClip);
        powerUpUI.ClearPowerUpStatus();
    }

    private IEnumerator ApplyFireRateBoost(PowerUpData powerUpData)
    {
        // playerInputBehaviour.SetFireRateMultiplier(powerUpData.multiplier);
        // yield return new WaitForSeconds(powerUpData.duration);
        // playerInputBehaviour.SetFireRateMultiplier(1f);
        playerInputBehaviour.SetFireRateMultiplier(powerUpData.multiplier);
        float remainingTime = powerUpData.duration;
        while (remainingTime > 0)
        {
            powerUpUI.UpdatePowerUpStatus(powerUpData.powerUpDisplayName, remainingTime);
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        playerInputBehaviour.SetFireRateMultiplier(1f);
        audioSource.PlayOneShot(powerUpExpireClip);
        powerUpUI.ClearPowerUpStatus();
    }

    private IEnumerator ApplyShield(PowerUpData powerUpData)
    {
        // shieldObject.SetActive(true);
        // healthBehaviour.UpdateIsShielded(true);
        // yield return new WaitForSeconds(powerUpData.duration);
        // healthBehaviour.UpdateIsShielded(false);
        // shieldObject.SetActive(false);

        shieldObject.SetActive(true);
        healthBehaviour.UpdateIsShielded(true);
        float remainingTime = powerUpData.duration;
        while (remainingTime > 0)
        {
            powerUpUI.UpdatePowerUpStatus(powerUpData.powerUpDisplayName, remainingTime);
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        healthBehaviour.UpdateIsShielded(false);
        shieldObject.SetActive(false);
        audioSource.PlayOneShot(powerUpExpireClip);
        powerUpUI.ClearPowerUpStatus();
    }
}
