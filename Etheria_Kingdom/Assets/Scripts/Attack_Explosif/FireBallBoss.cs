using UnityEngine;

public class FireballBoss : MonoBehaviour
{
    public float lifespan = 5f;  // Time before the fireball disappears
    public int damage = 1;  // Damage the fireball will cause (if collision is detected)
    public GameObject explosionEffect;  // Reference to the explosion particle effect prefab

    void Start()
    {
        // Destroy the fireball after a set lifespan
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the fireball collides with an object tagged "Crystal"
        if (collision.gameObject.CompareTag("Crystal"))
        {
            // Apply damage to the Crystal GameObject
            GameObject crystal = collision.gameObject;

            // Get the HealthController script from the Crystal GameObject
            HealthController healthController = crystal.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(damage);  // Call the TakeDamage method on the HealthController script
            }

            // Create the explosion effect at the point of collision
            if (explosionEffect != null)
            {
                // Instantiate the particle system at the collision point with no rotation (Quaternion.identity)
                Instantiate(explosionEffect, collision.contacts[0].point, Quaternion.identity);
            }

            // Destroy the fireball after the collision and effect
            Destroy(gameObject);
        }
        else
        {
            // Optionally handle other types of collisions, e.g., with walls or enemies
            Destroy(gameObject);
        }
    }
}
