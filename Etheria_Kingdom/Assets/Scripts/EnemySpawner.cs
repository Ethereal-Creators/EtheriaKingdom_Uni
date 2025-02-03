using System.Collections;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    public static event Action<EnemySpawner> OnEnemyKilled;

    [SerializeField] float health, maxHealth = 2f;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject enemyPrefab; // Enemy prefab to spawn
    [SerializeField] float spawnInterval = 2f; // Base time between each enemy spawn (minimum delay)
    [SerializeField] float spawnTimeVariance = 1f; // Maximum variance for spawn delay (in seconds)

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void Start()
    {
        health = maxHealth;
        target = GameObject.Find("Player").transform;

        // Start the coroutine to spawn enemies one at a time with a random delay between each spawn
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle; // Update rotation
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    // Coroutine to spawn enemies one at a time with randomized spawn times
    private IEnumerator SpawnEnemies()
    {
        while (true) // Infinite loop to keep spawning enemies
        {
            SpawnEnemy();
            float randomSpawnTime = UnityEngine.Random.Range(spawnInterval, spawnInterval + spawnTimeVariance); // Randomize spawn time
            yield return new WaitForSeconds(randomSpawnTime); // Wait for the random time before spawning the next enemy
        }
    }

    // Function to spawn a single enemy at a random position outside the screen
    private void SpawnEnemy()
    {
        // Calculate random spawn position outside the screen
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect; // Screen width in world units
        float screenHeight = Camera.main.orthographicSize; // Screen height in world units

        Vector2 spawnPosition = Vector2.zero;
        
        // Choose a random position outside the screen
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            spawnPosition.x = UnityEngine.Random.Range(-screenWidth - 1f, screenWidth + 1f); // Outside screen horizontally
            spawnPosition.y = UnityEngine.Random.Range(-screenHeight - 1f, screenHeight + 1f); // Outside screen vertically
        }
        else
        {
            spawnPosition.x = UnityEngine.Random.Range(-screenWidth - 1f, screenWidth + 1f); // Outside screen horizontally
            spawnPosition.y = UnityEngine.Random.Range(-screenHeight - 1f, screenHeight + 1f); // Outside screen vertically
        }

        // Instantiate the enemy at the calculated spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // Method to handle taking damage and destroying the enemy
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            // Notify that an enemy has been killed
            OnEnemyKilled?.Invoke(this);
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
