using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigmelee : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float bulletLifetime = 5f;

    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    private void Awake()
    {
        _camera = Camera.main;
        source = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Box"))
        {
            ItemBox itemBox = collision.GetComponent<ItemBox>();
            if (itemBox != null) itemBox.OnHit();  // Trigger box hit
            Destroy(gameObject);
        }*/

        if (collision.CompareTag("BoiteEvenement"))
        {
            var scriptEvent = collision.gameObject.GetComponent<eventContainerScript>();

            scriptEvent.actionOnCollsion();
        }

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

            if (source != null && clipsStart.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.pitch = Random.Range(0.8f, 1.2f);
                source.PlayOneShot(clipsStart[randomClipIndex]);

            }

            yield return new WaitForSeconds(shootInterval);
        }


    }
}
