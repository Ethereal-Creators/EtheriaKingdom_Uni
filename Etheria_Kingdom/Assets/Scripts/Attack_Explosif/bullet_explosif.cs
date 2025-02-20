using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet_explosif : MonoBehaviour
{
    private Camera _camera;
    public GameObject bulletPrefab;  // Reference to the bullet prefab
    public float shootInterval = 0.5f; // Time interval between shots (in seconds)
    public float bulletLifetime = 3f; // Lifetime of the bullet before it disappears
    public float SplashArea = 5f;
    public float Damage = 10;

    private float timeTilAnim = 2f;
    private float timeWhenAnim;

    Animator myAnimator;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        // Destroy bullet after a set time
        Destroy(gameObject, bulletLifetime);
        myAnimator = GetComponent<Animator>();
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
                //var enemy = hitCollider.GetComponent<HealthController>();
                var enemyExist = hitCollider.GetComponent<EnemyMovement>();
                if (enemyExist)
                {
                    HealthController healthControllerExplosion = hitCollider.GetComponent<HealthController>();
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(SplashArea, 0, distance);
                    healthControllerExplosion.TakeDamage(10);
                    myAnimator.SetTrigger("explosion");
                    Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
                    rigidbody.velocity *= 0f;
                    //this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

                    timeWhenAnim = Time.time + timeTilAnim;
                    timeTilAnim -= Time.deltaTime;
                    if (timeTilAnim < 0)
                    {
                        Destroy(gameObject);
                    }
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
