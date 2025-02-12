using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject bigSwarmerPrefab;

    [SerializeField]
    private float minSwarmerInterval = 2f;
    [SerializeField]
    private float maxSwarmerInterval = 5f; 
    [SerializeField]
    private float minBigSwarmerInterval = 7f; 
    [SerializeField]
    private float maxBigSwarmerInterval = 12f; 

    [SerializeField]
    private int maxEnemies = 10; 
    private int currentEnemyCount = 0;

    

    void Start()
    {
        StartCoroutine(spawnEnemy(swarmerPrefab, minSwarmerInterval, maxSwarmerInterval));
        StartCoroutine(spawnEnemy(bigSwarmerPrefab, minBigSwarmerInterval, maxBigSwarmerInterval));
    }

    private IEnumerator spawnEnemy(GameObject enemy, float minInterval, float maxInterval)
    {
        float interval = Random.Range(minInterval, maxInterval);

        yield return new WaitForSeconds(interval);

        if (currentEnemyCount < maxEnemies)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-7f, 7f), 0), Quaternion.identity);
            currentEnemyCount++;

        }
        StartCoroutine(spawnEnemy(enemy, minInterval, maxInterval));
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
