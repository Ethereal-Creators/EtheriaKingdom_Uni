using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float shootInterval = 0.5f; // Time interval between shots (in seconds)
    public float bulletLifetime = 3f; // Lifetime of the bullet before it disappears
    public int minDamage = 10; // Minimum damage
    public int maxDamage = 13; // Maximum damage

    public GameObject Blood;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        // Destroy bullet after a set time
        Destroy(gameObject, bulletLifetime);
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box")) // If the bullet collides with the box
        {
            // Destroy the box
            Destroy(collision.gameObject);

            // Spawn a random item after the box is destroyed
            ItemBox itemBox = collision.GetComponent<ItemBox>();
            if (itemBox != null)
            {
                itemBox.SpawnRandomItem(); // Call the method to spawn an item
            }

            // Destroy the bullet immediately after the collision with the box
            Destroy(gameObject);
        }

        //ajout pour BoiteEvenement
        if (collision.CompareTag("BoiteEvenement"))
        {
            var scriptEvent = collision.gameObject.GetComponent<eventContainerScript>();

            scriptEvent.actionOnCollsion();
        }

        if (collision.GetComponent<EnemyMovement>())
        {
            Instantiate(Blood, transform.position, Quaternion.identity);

            // Generate a random damage value between minDamage and maxDamage as integers
            int randomDamage = Random.Range(minDamage, maxDamage + 1); // maxDamage + 1 to include maxDamage

            // Apply the random damage to the enemy
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(randomDamage);

            // Destroy the bullet after collision
            Destroy(gameObject);
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ShootAutomatically()
    {
        while (true)
        {
            // Shoot a bullet (instantiate at the player's position or any specific point)
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // Wait for the specified interval before shooting the next bullet
            yield return new WaitForSeconds(shootInterval);
        }
    }
}
