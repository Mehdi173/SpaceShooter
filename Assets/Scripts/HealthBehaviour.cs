using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    public UnityEvent onDeath;
    // public UnityEvent<int> OnHealthChange; //public so that other objects can subscribe to it, also int or String?, needed for health bars but do I have enough time to do that??

    // workaround to set player health, ultimately need to make the method call from some player-related script
    private void Start()
    {
        if (currentHealth == 0)
        {
            InitialiseHealth(maxHealth);
        }
    }

    public void InitialiseHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = this.maxHealth;
        // OnHealthChange?.Invoke(currentHealth);
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        // OnHealthChange?.Invoke(currentHealth); //gonna get negative so need to think
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        // currently using inspector for enemy disappearing, prob will also cause wave to end and the one after to start if there are no more enemies?
        // for player maybe I will pause and show gameover UI?
        Debug.Log("I'm dead!");
        onDeath?.Invoke();
        gameObject.SetActive(false);
    }
}
