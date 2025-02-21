using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerY : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmerPrefab;
    [SerializeField]
    private GameObject bigSwarmerPrefab;

    [SerializeField]
    private float minSwarmerInterval = 7f;
    [SerializeField]
    private float maxSwarmerInterval = 7f;
    [SerializeField]
    private float minBigSwarmerInterval = 7f;
    [SerializeField]
    private float maxBigSwarmerInterval = 12f;

    [SerializeField]
    private float maximumRandom = 5f;
    [SerializeField]
    private float minimumRandom = -5f;

    [SerializeField]
    private int maxEnemies = 10;
    private int currentEnemyCount = 0;

    [Header("------- Audio Effects Spawn -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();


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
            GameObject newEnemy = Instantiate(enemy, (new Vector2(this.gameObject.transform.position.x, Random.Range(minimumRandom, maximumRandom))), Quaternion.identity);
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


    /*private float timeUntilSpawn;

    void awake ()
        {
            SetTimeUntilSpawn();
        }

        void Update()
        {
            minBigSwarmerInterval -= Time.deltaTime;

            if(minBigSwarmerInterval <= 0)
            {
                Instantiate(swarmerPrefab, transform.position, Quaternion.indentity);
                SetTimeUntilSpawn();
            }
        }

        private void SetTimeUntilSpawn()
        {
            timeUntilSpawn = Random.Range(minSwarmerInterval, maxSwarmerInterval);
        }*/
}
