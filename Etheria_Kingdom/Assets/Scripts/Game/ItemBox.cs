using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject[] itemPrefabs;      // Array of item prefabs to spawn when the box is destroyed
    public float blinkDuration = 1f;       // How long the box will blink (will turn white when hit)
    public float despawnDelay = 1f;        // How long after blinking before despawning
    public Vector3 spawnOffset = Vector3.zero;  // Offset where items will spawn relative to the box
    public AudioClip hitSound;             // Sound effect to play when the box is hit
    public ParticleSystem destructionEffect; // Particle effect when the box is destroyed
    public ParticleSystem hitEffect;        // Particle effect when the box is hit
    public float itemDropChance = 0.75f;   // Chance that an item will drop (between 0 and 1)
    public Vector3 scaleShrinkAmount = new Vector3(0.1f, 0.1f, 0.1f); // How much the box will shrink before disappearing
    public float scaleShrinkDuration = 0.3f; // Duration for shrinking animation

    private Renderer boxRenderer;          // To access the box's material
    private Color originalColor;           // Store the original color of the box
    private AudioSource audioSource;       // Audio source for playing sounds
    private Vector3 originalScale;         // Store the original scale of the box

    private void Start()
    {
        // Get the renderer component of the box
        boxRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        // Store the original color of the box
        if (boxRenderer != null)
        {
            originalColor = boxRenderer.material.color;
        }
        else
        {
            Debug.LogWarning("No Renderer component found on this object.");
        }

        // Store the original scale of the box
        originalScale = transform.localScale;

        // Add an AudioSource component if not already attached
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // This method is called when the box is hit by a bullet
    public void OnHit()
    {
        if (boxRenderer != null)
        {
            BoxCollider2D colliderOfBox = this.gameObject.GetComponent<BoxCollider2D>();
            colliderOfBox.enabled = false;
            // Play hit sound with random pitch
            PlayHitSound();

            // Play hit particle effect
            PlayHitEffect();

            // Start the blinking and despawn coroutine
            StartCoroutine(BlinkAndDespawn());
        }
    }

    // Coroutine to blink the box white and then despawn it
    private IEnumerator BlinkAndDespawn()
    {
        // Change the color to white to indicate hit (box will turn white)
        boxRenderer.material.color = Color.red;

        // Wait for the blink duration to make the box stay white
        yield return new WaitForSeconds(blinkDuration);

        // Restore the original color
        boxRenderer.material.color = originalColor;

        // Shrink the box before disappearing (add animation)
        yield return StartCoroutine(ShrinkAndDestroy());

        // Wait for the despawn delay before destroying the box
        yield return new WaitForSeconds(despawnDelay);

        // Play destruction effect
        PlayDestructionEffect();

        // Randomize whether or not an item is dropped based on itemDropChance
        if (Random.value <= itemDropChance)
        {
            // Spawn a random item before despawning the box
            SpawnRandomItem();
        }

        // Destroy the box
        Destroy(gameObject);
    }

    // Shrink the box (this is for a shrinking animation before destruction)
    private IEnumerator ShrinkAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale - scaleShrinkAmount;

        while (elapsedTime < scaleShrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleShrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the box is fully shrunk
        transform.localScale = targetScale;
    }

    // Function to spawn a random item from the itemPrefabs array with an offset
    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length > 0)
        {
            // Select a random index from the itemPrefabs array
            int randomIndex = Random.Range(0, itemPrefabs.Length);

            // Log the name of the item being spawned
            Debug.Log("Spawning random item: " + itemPrefabs[randomIndex].name);

            // Instantiate the random item at the box's position + offset
            Instantiate(itemPrefabs[randomIndex], transform.position + spawnOffset, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No items to spawn. Please assign item prefabs.");
        }
    }

    // Play the hit sound effect with a random pitch
    private void PlayHitSound()
    {
        if (hitSound != null)
        {
            // Randomize pitch for more dynamic sound
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogWarning("Hit sound is not assigned.");
        }
    }

    // Play the hit effect (particle effect) when the box is hit
    private void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            // Play the hit particle effect at the box's position
            hitEffect.Play();
        }
        else
        {
            Debug.LogWarning("Hit effect is not assigned.");
        }
    }

    // Play a destruction effect when the box is destroyed
    private void PlayDestructionEffect()
    {
        if (destructionEffect != null)
        {
            // Instantiate the destruction effect at the box's position
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Destruction effect is not assigned.");
        }
    }
}
