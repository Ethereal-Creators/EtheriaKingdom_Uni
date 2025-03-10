using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 5f;
    public float fireballCooldown = 2f;

    private bool isShootingEnabled = true;  // Flag to control whether the boss is shooting
    private float fireballTimer = 0f;

    void Update()
    {
        if (isShootingEnabled)
        {
            fireballTimer += Time.deltaTime;
            if (fireballTimer >= fireballCooldown)
            {
                ShootFireball();
                fireballTimer = 0f;
            }
        }
    }

    void ShootFireball()
    {
        // Logic to shoot a fireball
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * fireballSpeed;
    }

    // This method stops the boss from shooting fireballs
    public void StopShooting()
    {
        isShootingEnabled = false;  // Set flag to false to stop shooting
    }

    // Optionally, you could also implement a method to restart shooting if needed
    public void StartShooting()
    {
        isShootingEnabled = true;  // Set flag to true to enable shooting again
    }
}
