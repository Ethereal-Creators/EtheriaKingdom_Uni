using System.Collections;
using UnityEngine;

public class BossDeathEvents : MonoBehaviour
{
    public GameObject explosionPrefab;  // Reference to the explosion prefab
    public int numberOfExplosions = 5;  // Number of explosions to spawn
    public float explosionHeight = 3f;  // Height above the boss for the explosions
    public float explosionDelay = 0.1f;  // Delay between each explosion
    public Animator animator;  // Reference to the boss's Animator component
    public BossMovement bossMovement;  // Reference to the BossMovement script

    public AudioClip[] explosionSounds;  // Array of explosion sounds to play

    private bool isDead = false; // Flag to check if the boss is dead

    private Renderer bossRenderer; // The renderer to control fading effect
    private Material bossMaterial; // The material of the boss
    private Color initialColor; // The initial color of the boss material

    public float fadeDuration = 2f; // Duration of the fade-out effect

    public void Died()
    {
        if (isDead) return;  // Prevent multiple death calls

        Debug.Log("Died events started");

        // Stop movement and animation
        StopMovement();
        StopAnimation();

        // Get the boss's renderer and material
        bossRenderer = GetComponent<Renderer>();
        if (bossRenderer != null)
        {
            bossMaterial = bossRenderer.material;
            initialColor = bossMaterial.color; // Store the initial color of the boss
        }

        // Start the explosion sequence before the boss disappears
        StartCoroutine(SpawnExplosions());
    }

    public void StopMovement()
    {
        // Disable the BossMovement script to stop movement
        if (bossMovement != null)
        {
            bossMovement.enabled = false;  // Disable the movement script
        }
    }

    public void StopAnimation()
    {
        // Stop the boss's animation by setting the animator's speed to zero
        if (animator != null)
        {
            animator.speed = 0;  // Pause the animation
        }
    }

    private IEnumerator SpawnExplosions()
    {
        // Get the position of the boss (this will be the base position for explosions)
        Vector3 bossPosition = transform.position;

        // Spawn explosions one by one with a delay between them
        for (int i = 0; i < numberOfExplosions; i++)
        {
            // Calculate a random position in a spherical area above the boss
            Vector3 randomOffset = Random.insideUnitSphere;  // Random point inside a unit sphere
            randomOffset.y = Mathf.Abs(randomOffset.y);  // Ensure it's above the boss (positive y-axis)

            // Apply the offset to the boss position, keeping the explosion height fixed
            Vector3 explosionPosition = bossPosition + randomOffset * 1f;  // 2f is the radius for randomization
            explosionPosition.y = bossPosition.y + explosionHeight;  // Set the explosion height to be above the boss

            // Instantiate the explosion prefab at the calculated position
            GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);

            // Ensure the explosion prefab has an AudioSource
            AudioSource audioSource = explosion.GetComponent<AudioSource>();

            if (audioSource != null && explosionSounds.Length > 0)
            {
                // Choose a random sound from the explosionSounds array
                AudioClip selectedSound = explosionSounds[Random.Range(0, explosionSounds.Length)];

                // Set the clip and play the sound
                audioSource.clip = selectedSound;
                audioSource.Play();  // Play the selected explosion sound
            }
            else
            {
                Debug.LogWarning("Explosion prefab does not have an AudioSource component or explosionSounds is empty!");
            }

            // Wait for the explosion delay before spawning the next one
            yield return new WaitForSeconds(explosionDelay);
        }

        // Wait for 3 seconds after all explosions have finished
        yield return new WaitForSeconds(2f);

        // Start fading out the boss before it despawns
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        if (bossMaterial == null) yield break;

        float elapsedTime = 0f;
        Color startColor = bossMaterial.color;

        while (elapsedTime < fadeDuration)
        {
            // Lerp between the initial color and fully transparent color
            float alpha = Mathf.Lerp(startColor.a, 0f, elapsedTime / fadeDuration);
            bossMaterial.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final color is fully transparent
        bossMaterial.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Despawn (destroy) the boss after fading out
        Destroy(gameObject);
    }
}
