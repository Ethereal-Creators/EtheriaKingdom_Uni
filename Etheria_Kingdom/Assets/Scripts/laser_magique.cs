using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;
    public float shootInterval = 0.5f;
    public float bulletLifetime = 3f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Box"))
        {
            ItemBox itemBox = collision.GetComponent<ItemBox>();
            if (itemBox != null) itemBox.OnHit();  // Trigger box hit
            Destroy(gameObject);
        }
        
        if (collision.CompareTag("BoiteEvenement"))
        {
            var scriptEvent = collision.gameObject.GetComponent<eventContainerScript>();

            scriptEvent.actionOnCollsion();
        }*/

        if (collision.GetComponent<EnemyMovement>())
        {
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(50);
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
