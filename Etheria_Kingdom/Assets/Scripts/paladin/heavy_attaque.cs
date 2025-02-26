using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigmelee : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float bulletLifetime = 5f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(15);
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
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(shootInterval);
        }
    }
}
