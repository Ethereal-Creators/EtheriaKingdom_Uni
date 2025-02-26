using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerUpEffect
{
    public float amount; // The amount by which to decrease the player's shooting time
    public GameObject pickupAnimationPrefab; // The prefab of the pickup animation

    public override void Apply(GameObject target)
    {
        if (target == null)
        {
            Debug.LogError("Target is null. Make sure you're passing the player GameObject.");
            return;
        }

        // Apply the effect to the PlayerShoot component
        PlayerShoot playerShoot = target.GetComponent<PlayerShoot>();
        if (playerShoot != null)
        {
            // Decrease the time between shots to make shooting faster
            playerShoot._timeBetweenShots -= amount;

            // Ensure the time between shots doesn't go below a minimum threshold (e.g., 0.1 seconds)
            playerShoot._timeBetweenShots = Mathf.Max(playerShoot._timeBetweenShots, 0.1f);
        }

        // Now, use the `target` GameObject directly
        if (target != null)
        {
            // Find the child GameObject with the SpriteRenderer (assumed to be named "JoueurSprite")
            Transform joueurSpriteTransform = target.transform.Find("JoueurSprite");

            if (joueurSpriteTransform != null)
            {
                // Change the color of the SpriteRenderer to blue
                SpriteRenderer spriteRenderer = joueurSpriteTransform.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = Color.blue;
                    Debug.Log("JoueurSprite color changed to blue.");
                }
                else
                {
                    Debug.LogWarning("SpriteRenderer not found on JoueurSprite.");
                }

                // Speed up the animation by modifying the Animator's speed
                Animator spriteAnimator = joueurSpriteTransform.GetComponent<Animator>(); // Get the Animator from JoueurSprite
                if (spriteAnimator != null)
                {
                    spriteAnimator.speed += amount; // You can adjust this logic as needed
                    Debug.Log("JoueurSprite Animator speed modified.");
                }
                else
                {
                    Debug.LogWarning("Animator not found on JoueurSprite.");
                }
            }
            else
            {
                Debug.LogWarning("JoueurSprite child not found.");
            }

            // Instantiate the pickup animation prefab at the target's position
            if (pickupAnimationPrefab != null)
            {
                GameObject animationInstance = GameObject.Instantiate(pickupAnimationPrefab, target.transform.position, Quaternion.identity);

                // Make the animation follow the target's position
                animationInstance.transform.SetParent(target.transform); // Set the target as the parent of the animation object

                // Optionally, adjust the position relative to the target if needed, e.g., slightly above the target
                animationInstance.transform.localPosition = new Vector3(0, 1, 0); // Adjust as necessary

                // Destroy the animation after 2 seconds
                GameObject.Destroy(animationInstance, 2f);
            }
        }
    }
}
