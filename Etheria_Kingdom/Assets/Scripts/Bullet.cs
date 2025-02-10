using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*public class projectile : MonoBehaviour
{
    public GameObject projectiles;
    public Transform pointProjectile;
    public float forceProjectile = 20f;
    public float tirInterval = 1f;
    
    private float nextShotTime = 0f;

    void Update()
    {

        if (Time.time >= nextShotTime)
        {
            Tire();
            nextShotTime = Time.time + tirInterval;
        }
    }

    public void Tire()
    {
       GameObject tire = Instantiate(projectiles, pointProjectile.position, pointProjectile.rotation);
       tire.GetComponent<Rigidbody2D>().AddForce(pointProjectile.up * forceProjectile, ForceMode2D.Impulse);
        Destroy(tire, 0.5f);
    }
}*/






public class Bullet : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float shootInterval = 0.5f; // Time interval between shots (in seconds)
    public float bulletLifetime = 3f; // Lifetime of the bullet before it disappears

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
        if (collision.GetComponent<EnemyMovement>())
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(10);
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

