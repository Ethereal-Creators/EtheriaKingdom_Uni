using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyController : MonoBehaviour
{
    [SerializeField]
    private float blinkDuration = 1f; // Time for blinking before destruction
    [SerializeField]
    private int blinkCount = 5; // Number of times to blink before destruction
    [SerializeField]
    private AudioClip destructionSound; // Optional sound to play when destroyed

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get SpriteRenderer for blinking effect
        audioSource = GetComponent<AudioSource>(); // Get AudioSource for sound effect
    }

    public void DestroyEnemy(float delay)
    {
        // Play sound if specified
        if (destructionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(destructionSound);
        }

        // Start the blinking process immediately
        if (spriteRenderer != null)
        {
            StartCoroutine(BlinkAndDestroy(delay));
        }
        else
        {
            // If no sprite renderer is found, just destroy the object after the delay
            Destroy(gameObject, delay);
        }
    }

    private IEnumerator BlinkAndDestroy(float delay)
    {
        int blinkTimes = 0;

        // Start blinking immediately
        while (blinkTimes < blinkCount)
        {
            // Toggle visibility by enabling/disabling the sprite renderer
            spriteRenderer.enabled = !spriteRenderer.enabled;

            // Wait for half of the blink interval
            yield return new WaitForSeconds(blinkDuration / (2f * blinkCount));

            blinkTimes++;
        }

        // Ensure the sprite is disabled before destroying
        spriteRenderer.enabled = false;

        // Wait for the specified delay after blinking
        yield return new WaitForSeconds(delay);

        // Destroy the object after the final blink and delay
        Destroy(gameObject);
    }
}
