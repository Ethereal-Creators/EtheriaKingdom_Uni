using UnityEngine;

[CreateAssetMenu(fileName = "New Shield Buff", menuName = "Powerups/Shield Buff")]
public class ShieldBuff : PowerUpEffect
{
    public GameObject shieldPrefab; // The shield prefab that will appear on the player
    private GameObject currentShield;

    public override void Apply(GameObject target)
    {
        // Check if the player already has a shield to prevent multiple shields
        if (currentShield != null)
        {
            Debug.Log("Shield already applied, destroying previous shield.");
            Destroy(currentShield); // Remove the previous shield if it exists
        }

        // Instantiate the shield prefab at the player's position
        Debug.Log("Applying shield buff to player: " + target.name);
        currentShield = Instantiate(shieldPrefab, target.transform.position, Quaternion.identity);
        currentShield.transform.SetParent(target.transform); // Make the shield follow the player

        // Log when the shield is instantiated
        Debug.Log("Shield spawned at position: " + target.transform.position);
    }
}
