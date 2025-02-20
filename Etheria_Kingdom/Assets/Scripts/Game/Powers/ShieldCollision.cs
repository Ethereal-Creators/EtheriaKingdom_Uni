using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public float damageAmount = 20f; // The damage to apply to the enemy

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the "Enemy" tag
        if (collision.CompareTag("ennemie"))
        {
            // Try to get the HealthController component from the enemy
            HealthController healthController = collision.GetComponent<HealthController>();

            if (healthController != null)
            {
                // Apply damage to the enemy using the HealthController's TakeDamage method
                healthController.TakeDamage(damageAmount);
                Debug.Log("Shield hit enemy, dealing " + damageAmount + " damage.");
            }
        }
    }
}
