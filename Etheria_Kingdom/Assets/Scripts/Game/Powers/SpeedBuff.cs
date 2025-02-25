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
        // Apply the effect to the PlayerShoot component
        PlayerShoot playerShoot = target.GetComponent<PlayerShoot>();
        if (playerShoot != null)
        {
            // Decrease the time between shots to make shooting faster
            playerShoot._timeBetweenShots -= amount;

            // Ensure the time between shots doesn't go below a minimum threshold (e.g., 0.1 seconds)
            playerShoot._timeBetweenShots = Mathf.Max(playerShoot._timeBetweenShots, 0.1f);
        }

        // Try to find the child object "Archer" and get the SpriteRenderer component
        Transform archerTransform = target.transform.Find("Archer");
        if (archerTransform != null)
        {
            SpriteRenderer spriteRenderer = archerTransform.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.blue; // Change color to blue (or any color you prefer)
            }

            // Try to get the Animator component from the "Archer" child
            Animator animator = archerTransform.GetComponent<Animator>();
            if (animator != null)
            {
                // Speed up the animation by modifying the Animator's speed
                animator.speed += amount; // You can adjust this logic as per your need
            }
        }

        // Try to find the player GameObject using the "joueur" tag
        GameObject player = GameObject.FindGameObjectWithTag("joueur");
        if (player != null)
        {
            // Instantiate the pickup animation prefab at the player's position
            GameObject animationInstance = GameObject.Instantiate(pickupAnimationPrefab, player.transform.position, Quaternion.identity);

            // Make the animation follow the player's position
            animationInstance.transform.SetParent(player.transform); // Set the player as the parent of the animation object

            // Optionally, adjust the position relative to the player if needed, e.g., slightly above the player
            animationInstance.transform.localPosition = new Vector3(0, 1, 0); // Adjust as necessary

            // Destroy the animation after 2 seconds
            GameObject.Destroy(animationInstance, 2f);
        }
    }
}
