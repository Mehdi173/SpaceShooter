using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isShielded = false;
    private int currentHealth;
    private bool isPlayer = false;

    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;
    public UnityEvent<string> OnHealthChange;

    private void Start()
    {
        if (GetComponent<PlayerInputBehaviour>() != null)
        {
            isPlayer = true;
        }

        if (isPlayer)
        {
            InitialiseHealth(maxHealth);
        }
    }

    public void InitialiseHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = this.maxHealth;
        if (isPlayer) OnHealthChange?.Invoke(currentHealth.ToString());
    }

    public void AddHealth(int value)
    {
        if (value < 0 && isShielded) return;

        currentHealth += value;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        if (isPlayer)
        {
            // so that health does not show negative
            currentHealth = Mathf.Max(0, currentHealth);
            OnHealthChange?.Invoke(currentHealth.ToString());
        }
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        if (isPlayer)
        {
            onPlayerDeath?.Invoke();
        } else
        {
            onEnemyDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void UpdateIsShielded(bool isShielded)
    {
        this.isShielded = isShielded;
    }
}
