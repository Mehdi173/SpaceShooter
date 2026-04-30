using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isShielded = false;
    private int currentHealth;
    private bool isPlayer = false;

    // public UnityEvent onDeath;
    public UnityEvent onPlayerDeath;
    public UnityEvent onEnemyDeath;
    public UnityEvent<string> OnHealthChange; //public so that other objects can subscribe to it, also int or String?, needed for health bars but do I have enough time to do that??

    // workaround to set player health, ultimately need to make the method call from some player-related script
    private void Start()
    {
        if (GetComponent<PlayerInputBehaviour>() != null)
        {
            isPlayer = true;
        }
        // isPlayer = GetComponent<PlayerInputBehaviour>();
        // if (currentHealth == 0)
        // {
        //     InitialiseHealth(maxHealth);
        // }
        if (isPlayer)
        {
            InitialiseHealth(maxHealth);
        }
    }

    public void InitialiseHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = this.maxHealth;
        // OnHealthChange?.Invoke(currentHealth);
        if (isPlayer) OnHealthChange?.Invoke(currentHealth.ToString());
    }

    public void AddHealth(int value)
    {
        if (value < 0 && isShielded) return;

        currentHealth += value;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        // OnHealthChange?.Invoke(currentHealth); //gonna get negative so need to think
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
            Debug.Log("Player dead!");
            // fire event for game over stuff?
            // set active false?
            onPlayerDeath?.Invoke();
        } else
        {
            Debug.Log("Enemy dead!");
            onEnemyDeath?.Invoke();
            gameObject.SetActive(false);
        }
        // // currently disappearing everything including player, need separation maybe for achievement of kills?
        // // but for player maybe I will also need to pause and show gameover UI?
        // Debug.Log("I'm dead!");
        // // so maybe I separate the events into something like OnPlayerDeath and OnEnemyDeath??
        // onDeath?.Invoke();
        // gameObject.SetActive(false);
    }

    public void UpdateIsShielded(bool isShielded)
    {
        this.isShielded = isShielded;
    }
}
