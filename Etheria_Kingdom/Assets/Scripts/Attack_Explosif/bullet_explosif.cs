using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_explosif : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float shootInterval = 0.5f; // Time interval between shots (in seconds)
    public float bulletLifetime = 3f; // Lifetime of the bullet before it disappears
    public float SplashArea = 10f;
    public float Damage = 10;

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
        if(SplashArea > 0)
        {
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashArea);
            foreach(var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<HealthController>();
                if (enemy)
                {
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(SplashArea, 0, distance);
                    enemy.TakeDamage(10);
                }
            }
        }


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
