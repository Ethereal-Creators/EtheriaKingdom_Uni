using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/Health Buff")]
public class HealthBuff : PowerUpEffect
{
    public float healthRegenAmount = 50f; // Total health to regenerate
    public float healthRegenDuration = 5f;    // Duration of the regeneration effect
    public GameObject animationPrefab; // Reference to the animation prefab (e.g., a particle system)

    public Vector3 animationOffset = new Vector3(0f, 1f, 0f); // Offset for animation position relative to the crystal
    public float animationDuration = 2f; // Duration before the animation prefab is destroyed

    public override void Apply(GameObject target)
    {
        // Find all game objects with the "Crystal" tag
        GameObject[] crystals = GameObject.FindGameObjectsWithTag("Crystal");

        // Loop through all crystals and apply the health regeneration buff
        foreach (GameObject crystal in crystals)
        {
            // Get the HealthController component of the crystal
            HealthController crystalHealthController = crystal.GetComponent<HealthController>();

            if (crystalHealthController != null)
            {
                // Log health regen for each crystal
                Debug.Log("Applying health regen to crystal: " + crystal.name);

                
                crystalHealthController.StartRegeneratingHealth(healthRegenAmount, healthRegenDuration);

                // Instantiate the animation prefab at the crystal's position with an offset
                if (animationPrefab != null)
                {
                    // Calculate the position with offset relative to the crystal's position
                    Vector3 animationPosition = crystal.transform.position + animationOffset;

                    // Instantiate the animation prefab at the calculated position and rotation
                    GameObject animationObject = Instantiate(animationPrefab, animationPosition, crystal.transform.rotation);

                    // Optionally, destroy the animation prefab after a certain time (for particle systems or animations)
                    Destroy(animationObject, animationDuration);
                }
                else
                {
                    Debug.LogWarning("No animation prefab assigned to HealthBuff.");
                }
            }
            else
            {
                Debug.LogWarning("Crystal does not have a HealthController component attached.");
            }
        }
    }
}
