using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;
    public float shootInterval = 0.5f;
    public float bulletLifetime = 3f;
    public int minDamage = 10;  // Default minimum damage
    public int maxDamage = 13;  // Default maximum damage

    public GameObject Blood;

    // Method to set or modify the bullet's minDamage and maxDamage
    public void SetDamage(int min, int max)
    {
        minDamage = min;
        maxDamage = max;
        Debug.Log("Bullet damage set to: " + minDamage + " - " + maxDamage);  // Log the new damage values
    }

    private void Awake()
    {
        _camera = Camera.main;  // Get the main camera
    }

    private void Start()
    {
        Destroy(gameObject, bulletLifetime);  // Destroy the bullet after the lifetime expires
    }

    private void Update()
    {
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            ItemBox itemBox = collision.GetComponent<ItemBox>();
            if (itemBox != null) itemBox.OnHit();  // Trigger box hit
            Destroy(gameObject);
        }

        if (collision.CompareTag("BoiteEvenement"))
        {
            var scriptEvent = collision.gameObject.GetComponent<eventContainerScript>();
            scriptEvent.actionOnCollsion();  // Handle event-specific actions
        }

        if (collision.GetComponent<EnemyMovement>())
        {
            // Instantiate blood effect at the bullet's position
            Instantiate(Blood, transform.position, Quaternion.identity);

            int randomDamage = Random.Range(minDamage, maxDamage + 1);  // Random damage between min and max damage
            Debug.Log("Bullet hit an enemy. Damage dealt: " + randomDamage);  // Log the damage dealt to the enemy

            // Apply damage to the enemy's health
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(randomDamage);

            // Get the direction of the bullet and apply knockback to the enemy
            Vector2 bulletDirection = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<EnemyMovement>().ApplyKnockback(bulletDirection);

            Destroy(gameObject);  // Destroy bullet after hitting the enemy
        }
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > _camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);  // Destroy bullet if it goes off-screen
        }
    }
}
