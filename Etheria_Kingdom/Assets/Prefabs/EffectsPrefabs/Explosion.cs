using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDuration = 1f;  // Duration of the explosion effect
    public AudioClip[] explosionSounds;  // Array of sounds to play for the explosion
    private AudioSource audioSource;  // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to the explosion prefab
        audioSource = GetComponent<AudioSource>();

        // If an AudioSource exists and there are explosion sounds, play one of them
        if (audioSource != null && explosionSounds.Length > 0)
        {
            // Randomly choose one of the explosion sounds
            AudioClip selectedSound = explosionSounds[Random.Range(0, explosionSounds.Length)];

            // Play the randomly selected explosion sound
            audioSource.PlayOneShot(selectedSound);
        }

        // Destroy the explosion after the effect is played
        Destroy(gameObject, explosionDuration);
    }
}
