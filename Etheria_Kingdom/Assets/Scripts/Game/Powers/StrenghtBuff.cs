using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/StrengthBuff")]
public class StrengthBuffEffect : PowerUpEffect
{
    public int damageBuffAmount = 5; // How much damage is added to the player's bullet damage
    public GameObject animationPrefab; // Reference to the animation prefab (e.g., a particle effect)
    public Vector3 animationOffset = new Vector3(0f, 1f, 0f); // Offset for animation position relative to the player
    public float animationDuration = 2f; // Duration before the animation prefab is destroyed

    public override void Apply(GameObject target)
    {
        // Find the PlayerShoot component (if we assume the player has this component)
        PlayerShoot playerShoot = target.GetComponent<PlayerShoot>();
        if (playerShoot != null)
        {
            // Apply the strength buff to the bullet damage
            Bullet bullet = target.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.minDamage += damageBuffAmount;
                bullet.maxDamage += damageBuffAmount;
                Debug.Log("Strength buff applied: " + damageBuffAmount + " damage added.");
            }
        }

        // Find the player who picked up the strength buff (assuming the player has the tag "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Instantiate the animation prefab at the player's position with an offset
            if (animationPrefab != null)
            {
                // Create the animation at the player's position with the defined offset
                Vector3 animationPosition = player.transform.position + animationOffset;
                GameObject animationInstance = GameObject.Instantiate(animationPrefab, animationPosition, Quaternion.identity);

                // Optionally, adjust the position relative to the player if needed
                animationInstance.transform.SetParent(player.transform); // Make it a child of the player
                animationInstance.transform.localPosition = new Vector3(0, 1, 0); // Adjust as necessary

                // Destroy the animation prefab after the duration
                GameObject.Destroy(animationInstance, animationDuration);
            }
            else
            {
                Debug.LogWarning("No animation prefab assigned to StrengthBuffEffect.");
            }
        }
        else
        {
            Debug.LogWarning("Player not found to apply animation.");
        }
    }
}
