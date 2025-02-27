using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/StrengthBuff")]
public class StrengthBuffEffect : PowerUpEffect
{
    public int damageBuffAmount = 5;  // Amount to increase the bullet damage by
    public GameObject animationPrefab;  // Animation effect (like a particle effect)
    public Vector3 animationOffset = new Vector3(0f, 1f, 0f);  // Position offset for the animation relative to the player
    public float animationDuration = 2f;  // Duration before the animation prefab is destroyed
    public float buffDuration = 10f;  // Duration for the strength buff effect

    private int originalMinDamage;  // To store the original minDamage before the buff
    private int originalMaxDamage;  // To store the original maxDamage before the buff
    private float buffTimer;  // Timer to track the buff duration

    public override void Apply(GameObject target)
    {
        // Find the PlayerShoot component (assume player has this component)
        PlayerShoot playerShoot = target.GetComponent<PlayerShoot>();
        if (playerShoot != null)
        {
            // Get the player's Bullet component (assuming the player shoots bullets)
            Bullet playerBullet = playerShoot.GetComponentInChildren<Bullet>();
            if (playerBullet != null)
            {
                // Save the original bullet damage values before the buff
                originalMinDamage = playerBullet.minDamage;
                originalMaxDamage = playerBullet.maxDamage;

                // Apply the damage buff to the bullet (increase both minDamage and maxDamage)
                playerBullet.SetDamage(originalMinDamage + damageBuffAmount, originalMaxDamage + damageBuffAmount);
                Debug.Log("Strength buff applied: " + damageBuffAmount + " damage added. New damage range: " +
                          playerBullet.minDamage + " - " + playerBullet.maxDamage);

                // Apply the animation (visual effect) at the player’s position
                ApplyAnimation(target);

                // Start a timer to remove the buff after a certain duration
                buffTimer = buffDuration;
                Debug.Log("Buff duration started: " + buffDuration + " seconds.");
            }
        }
    }

    private void ApplyAnimation(GameObject target)
    {
        // Instantiate the animation prefab at the player's position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && animationPrefab != null)
        {
            Vector3 animationPosition = player.transform.position + animationOffset;
            GameObject animationInstance = Instantiate(animationPrefab, animationPosition, Quaternion.identity);
            animationInstance.transform.SetParent(player.transform);
            animationInstance.transform.localPosition = new Vector3(0, 1, 0);  // Adjust as needed
            Destroy(animationInstance, animationDuration);  // Destroy animation after the given duration
            Debug.Log("Animation prefab instantiated at: " + animationPosition);
        }
        else
        {
            Debug.LogWarning("No animation prefab assigned to StrengthBuffEffect.");
        }
    }

    private void Update()
    {
        // If buff is active, track the buff timer
        if (buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;  // Decrease timer based on time passed

            if (buffTimer <= 0)
            {
                // Revert bullet damage back to original values once the buff time expires
                RevertDamage();
            }
        }
    }

    private void RevertDamage()
    {
        // Find the PlayerBullet and reset the damage back to its original state
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Bullet playerBullet = player.GetComponentInChildren<Bullet>();
            if (playerBullet != null)
            {
                // Revert damage values back to the original
                playerBullet.SetDamage(originalMinDamage, originalMaxDamage);
                Debug.Log("Strength buff removed. Damage reverted to: " +
                          originalMinDamage + " - " + originalMaxDamage);
            }
        }
    }
}
