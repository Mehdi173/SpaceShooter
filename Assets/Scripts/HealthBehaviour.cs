using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    public UnityEvent onDeath;
    // public UnityEvent<int> OnHealthChange; //public so that other objects can subscribe to it, also int or String?

    private void Start()
    {
        InitialiseHealth();
    }

    private void InitialiseHealth()
    {
        currentHealth = maxHealth;
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
        Debug.Log("I'm dead!");
        onDeath?.Invoke();
    }
}
