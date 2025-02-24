using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Set the health to max at the start
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // Prevent going over max health
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0f); // Prevent going below 0 health
    }
}
