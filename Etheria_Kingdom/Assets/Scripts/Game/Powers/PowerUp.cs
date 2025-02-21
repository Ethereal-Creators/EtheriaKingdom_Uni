using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerupEffect;

    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    public float minPitch = 0.8f; // Minimum pitch
    public float maxPitch = 1.2f; // Maximum pitch
    public float minVolume = 0.8f; // Minimum volume
    public float maxVolume = 1.0f; // Maximum volume

    private Collider2D powerUpCollider;
    private GameObject graphicObject; // The GameObject holding the SpriteRenderer

    void Start()
    {
        source = GetComponent<AudioSource>();
        powerUpCollider = GetComponent<Collider2D>();
        graphicObject = transform.Find("Graphic")?.gameObject; // Find the 'Graphic' child GameObject

        // If the "Graphic" GameObject doesn't exist, log a warning
        if (graphicObject == null)
        {
            Debug.LogWarning("No 'Graphic' child GameObject found on PowerUp.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has a PlayerMovement component
        if (collision.gameObject.CompareTag("joueur")) // This means the PlayerMovement component exists on the object
        {
            // Apply the power-up effect
            powerupEffect.Apply(collision.gameObject);

            // Play the sound effect with a random pitch and volume
            if (source != null && clipsStart.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.clip = clipsStart[randomClipIndex];

                // Set a random pitch between minPitch and maxPitch
                source.pitch = Random.Range(minPitch, maxPitch);

                // Set a random volume between minVolume and maxVolume
                source.volume = Random.Range(minVolume, maxVolume);

                source.Play(); // Play the clip with random pitch and volume
            }

            // Flash the player by changing their color to white for a short time
            StartCoroutine(FlashPlayer(collision.gameObject));

            // Disable the collider immediately to prevent further interaction
            if (powerUpCollider != null)
            {
                powerUpCollider.enabled = false;
            }

            // Disable the SpriteRenderer on the Graphic GameObject
            if (graphicObject != null)
            {
                SpriteRenderer graphicRenderer = graphicObject.GetComponent<SpriteRenderer>();
                if (graphicRenderer != null)
                {
                    graphicRenderer.enabled = false; // Hide the sprite
                }
            }

            // Optional: Add a small visual feedback, like scaling the power-up before destruction
            // Scaling up temporarily before destruction
            StartCoroutine(ScaleUpAndDestroy());

            // Invoke the method to destroy the power-up after the sound finishes
            if (source.clip != null)
            {
                float soundDuration = source.clip.length;
                Invoke("DestroyPowerUp", soundDuration); // Calls DestroyPowerUp after the duration of the clip
            }
        }
    }

    // Method to destroy the power-up after the sound has finished playing
    private void DestroyPowerUp()
    {
        // Optional: Add a destruction sound or feedback just before destroy
        // source.PlayOneShot(destroyClip); // (if you want a destruction sound)

        Destroy(gameObject); // Destroy the GameObject
    }

    // Coroutine to scale the power-up for visual feedback before destruction
    private IEnumerator ScaleUpAndDestroy()
    {
        Vector3 originalScale = transform.localScale;
        transform.localScale = originalScale * 1.2f; // Slightly scale up the power-up

        // Wait for a short duration
        yield return new WaitForSeconds(0.2f);

        transform.localScale = originalScale; // Restore original scale
    }

    // Coroutine to make the player flash white for a short duration
    private IEnumerator FlashPlayer(GameObject player)
    {
        SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();

        if (playerRenderer != null)
        {
            Color originalColor = playerRenderer.color; // Save the original color

            // Flash the player by changing their color to white a couple of times
            for (int i = 0; i < 3; i++) // Flash 3 times
            {
                playerRenderer.color = Color.white; // Change to white
                yield return new WaitForSeconds(0.1f); // Wait for a short time
                playerRenderer.color = originalColor; // Restore the original color
                yield return new WaitForSeconds(0.1f); // Wait for a short time before flashing again
            }
        }
    }
}
