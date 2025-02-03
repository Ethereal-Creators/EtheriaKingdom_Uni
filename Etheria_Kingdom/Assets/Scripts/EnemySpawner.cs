using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnTimeVariance = 1f;
    [SerializeField] int maxEnemies = 10; // Maximum number of enemies allowed at a time

    private List<GameObject> activeEnemies = new List<GameObject>(); // Track spawned enemies

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnInterval, spawnInterval + spawnTimeVariance));

            if (activeEnemies.Count < maxEnemies) // Check if limit is reached
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize;

        Vector2 spawnPosition = new Vector2(
            Random.Range(-screenWidth - 1f, screenWidth + 1f),
            Random.Range(-screenHeight - 1f, screenHeight + 1f)
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(newEnemy);

        // Attach the enemy destruction event
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDestroyed += RemoveEnemyFromList;
        }
    }

    private void RemoveEnemyFromList(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }
}
