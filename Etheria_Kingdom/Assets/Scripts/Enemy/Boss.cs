using UnityEngine;
using System.Collections.Generic;

public class Boss : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 5f;
    public float fireballCooldown = 2f;

    public Vector2 fireballOffset = new Vector2(0, 0);  // Offset for the spawn position of the fireball

    private bool isShootingEnabled = true;  // Flag to control whether the boss is shooting
    private float fireballTimer = 0f;

    // AudioSource and List of AudioClips
    public AudioSource source;  // Reference to the AudioSource component
    public List<AudioClip> shootClips;  // List of audio clips for shooting
    public List<AudioClip> damageClips; // List of audio clips for when the boss takes damage
    public List<AudioClip> deathClips;  // List of audio clips for when the boss dies

    void Start()
    {
        // Get the AudioSource component attached to this GameObject if not assigned
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }

        // Ensure the AudioSource is found
        if (source == null)
        {
            Debug.LogWarning("No AudioSource component found on the Boss object.");
        }

        StartShooting();
    }

    void Update()
    {
        if (!isShootingEnabled) return;  // If shooting is disabled, skip the shooting logic

        fireballTimer += Time.deltaTime;
        if (fireballTimer >= fireballCooldown)
        {
            ShootFireball();
            fireballTimer = 0f;
        }
    }

    private void ShootFireball()
    {
        // Apply the offset to the fireball spawn position
        Vector3 spawnPosition = fireballSpawnPoint.position + (Vector3)fireballOffset;

        // Instantiate the fireball at the new spawn position
        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * fireballSpeed;

        // Play a random shooting sound from the list if available
        PlayRandomSound(shootClips);
    }

    // This method stops the boss from shooting fireballs
    public void SetShootingEnabled(bool enabled)
    {
        isShootingEnabled = enabled;  // Set flag to enable or disable shooting
    }

    // Optionally, you could also implement a method to restart shooting if needed
    public void StartShooting()
    {
        SetShootingEnabled(true);  // Set flag to true to enable shooting again
    }

    // Method to play a random sound from a provided list
    private void PlayRandomSound(List<AudioClip> clips)
    {
        if (source != null && clips != null && clips.Count > 0)
        {
            int randomClipIndex = Random.Range(0, clips.Count);  // Get a random clip index
            source.PlayOneShot(clips[randomClipIndex]);  // Play the random sound
        }
    }

    // This method is triggered when the boss takes damage
    public void soundWhenDamaged()
    {
        Debug.Log("Damaged");
        PlayRandomSound(damageClips);  // Play a random damage sound from the list
    }

    // Optional: Play a random death sound when the boss dies
    public void soundWhenDead()
    {
        Debug.Log("Boss Dead");
        PlayRandomSound(deathClips);  // Play a random death sound from the list
    }
}
