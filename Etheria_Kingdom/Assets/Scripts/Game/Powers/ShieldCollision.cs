using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public float damageAmount = 20f; // The damage to apply to the enemy
    public float shieldDuration = 10f; // Duration for how long the shield will deal damage
    public float fadeDuration = 2f; // Duration for the fade effect
    private float shieldTimer; // Timer to track shield duration
    private float fadeTimer; // Timer to handle the fade-out duration

    private SpriteRenderer shieldSpriteRenderer; // Reference to the SpriteRenderer of the shield

    private void Start()
    {
        // Initialize the shield timer to the duration at the start
        shieldTimer = shieldDuration;
        fadeTimer = fadeDuration;

        // Get the SpriteRenderer component
        shieldSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Decrease the shield timer each frame
        if (shieldTimer > 0)
        {
            shieldTimer -= Time.deltaTime;
        }
        else if (fadeTimer > 0)
        {
            // Start the fade effect once the shield duration is over
            fadeTimer -= Time.deltaTime;

            // Gradually reduce the alpha (transparency) of the shield sprite
            float fadeAmount = fadeTimer / fadeDuration;
            Color currentColor = shieldSpriteRenderer.color;
            shieldSpriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, fadeAmount);

            // If fading is done, disable the shield
            if (fadeTimer <= 0)
            {
                DisableShieldDamage();
            }
        }
        else
        {
            // Once the fade is complete, disable the shield immediately
            DisableShieldDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the shield is still active (timer > 0)
        if (shieldTimer > 0)
        {
            // Check if the collided object has the "Enemy" tag
            if (collision.CompareTag("ennemie"))
            {
                Debug.Log("sheild colider with " + collision.name);
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

    private void DisableShieldDamage()
    {
        // Disable the shield's collider so it no longer detects collisions
        Collider2D shieldCollider = GetComponent<Collider2D>();
        if (shieldCollider != null)
        {
            shieldCollider.enabled = false;
        }

        // Disable the SpriteRenderer so the shield is no longer visible
        if (shieldSpriteRenderer != null)
        {
            shieldSpriteRenderer.enabled = false;
        }

        // Optionally, you can log that the shield is no longer dealing damage
        Debug.Log("Shield damage has stopped and sprite is deactivated.");
    }
}
