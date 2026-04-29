using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        InitialiseHealth();
    }

    private void InitialiseHealth()
    {
        currentHealth = maxHealth;
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private void Kill()
    {
        Debug.Log("I'm dead!");
    }
}
