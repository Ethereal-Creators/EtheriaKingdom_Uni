using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 5f;
    public float fireballCooldown = 2f;

    public Vector2 fireballOffset = new Vector2(0, 0);  // Offset for the spawn position of the fireball

    private bool isShootingEnabled = true;  // Flag to control whether the boss is shooting
    private float fireballTimer = 0f;

    public AudioClip shootSound;  // Sound to play when shooting a fireball
    private AudioSource audioSource;  // Reference to the AudioSource component

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure the audio source is not null
        if (audioSource == null)
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

        // Play the shooting sound effect if available
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);  // Play the sound effect
        }
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
}
