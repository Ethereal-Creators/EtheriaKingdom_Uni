using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Health Buff")]
public class HealthBuff : PowerUpEffect
{
    public float healthRegenAmount = 50f; // Total health to regenerate
    public float healthRegenDuration = 5f;    // Duration of the regeneration effect
    public string animationTrigger = "BuffCollected";  // Trigger name for the animation

    public override void Apply(GameObject target)
    {
        // Find all game objects with the "Crystal" tag
        GameObject[] crystals = GameObject.FindGameObjectsWithTag("Crystal");

        // Loop through all crystals and apply the health regeneration buff
        foreach (GameObject crystal in crystals)
        {
            // Get the HealthController component of the crystal
            HealthController crystalHealthController = crystal.GetComponent<HealthController>();

            // Check for Animator component in the crystal
            Animator crystalAnimator = crystal.GetComponent<Animator>();

            if (crystalHealthController != null)
            {
                // Log health regen for each crystal
                Debug.Log("Applying health regen to crystal: " + crystal.name);

                // Trigger the animation on the crystal
                if (crystalAnimator != null)
                {
                    crystalAnimator.SetTrigger(animationTrigger);
                }
                else
                {
                    Debug.LogWarning("Crystal does not have an Animator component attached.");
                }

                // Start health regeneration for the crystal
                crystalHealthController.StartRegeneratingHealth(healthRegenAmount, healthRegenDuration);
            }
            else
            {
                Debug.LogWarning("Crystal does not have a HealthController component attached.");
            }
        }
    }
}
