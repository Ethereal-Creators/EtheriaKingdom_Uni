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
<<<<<<< HEAD
    private float maxSwarmerInterval = 7f;
=======
    private float maxSwarmerInterval = 5f; 
>>>>>>> parent of e74a5fa (oh my lerd)
    [SerializeField]
    private float minBigSwarmerInterval = 7f;
    [SerializeField]
    private float maxBigSwarmerInterval = 12f;

    [SerializeField]
<<<<<<< HEAD
    private float maximumRandom = 5f;
    [SerializeField]
    private float minimumRandom = -5f;

    [SerializeField]
    private int maxEnemies = 10;
    private int currentEnemyCount = 0;

    [Header("------- Audio Effects Spawn -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

=======
    private int maxEnemies = 10; 
    private int currentEnemyCount = 0;

    
>>>>>>> parent of e74a5fa (oh my lerd)

    void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();

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
            if (source != null && clipsStart.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.PlayOneShot(clipsStart[randomClipIndex]);
            }

        }
        StartCoroutine(spawnEnemy(enemy, minInterval, maxInterval));
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
    }
}
