using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBossNotProjectile : MonoBehaviour
{
    public GameObject enemyOne;
    //public GameObject enemyTwo;

    public GameObject Spawner;

    public float timeBetweenSpawn;

    public float timeWhenScream;

    [Header("------- Seulement pour visualiser pas toucher (timeUntilSpawn) -------")]
    public float timeUntilSpawn = 0.0f;
    public float timeUntilScream = 0.0f;

    private bool isSreamSoundPlayed = false;

    [Header("------- Audio Effects Spawn -------")]
    public AudioSource sourceBoss;

    public AudioSource sourceEnnemy;
    public List<AudioClip> clipsBossSpawn = new List<AudioClip>();

    public List<AudioClip> clipsEnemySpawn = new List<AudioClip>();

    // Update is called once per frame
    void Update()
    {
        timeUntilScream += Time.deltaTime;
        if (timeUntilScream >= timeWhenScream && isSreamSoundPlayed == false)
        {
            isSreamSoundPlayed = true;
            if (sourceBoss != null && clipsEnemySpawn.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsBossSpawn.Count);
                sourceBoss.PlayOneShot(clipsBossSpawn[randomClipIndex]);
            }
        }
        timeUntilSpawn += Time.deltaTime;
        if (timeUntilSpawn >= timeBetweenSpawn)
        {
            timeUntilSpawn = 0.0f;
            SpawnEnnemy(Spawner);
        }

    }

    public void SpawnEnnemy(GameObject objectSpawner)
    {
        if (enemyOne != null /*|| enemyTwo != null*/)
        {
            if (sourceEnnemy != null && clipsEnemySpawn.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsEnemySpawn.Count);
                sourceEnnemy.PlayOneShot(clipsEnemySpawn[randomClipIndex]);
            }
        }

        if (enemyOne != null)
        {
            GameObject newEnemyOne = Instantiate(enemyOne, (new Vector2(objectSpawner.gameObject.transform.position.x, objectSpawner.gameObject.transform.position.y)), Quaternion.identity);
        }

        /*
        if (enemyTwo != null)
        {
            GameObject newEnemyTwo = Instantiate(enemyTwo, (new Vector2(objectSpawner.gameObject.transform.position.x, objectSpawner.gameObject.transform.position.y)), Quaternion.identity);
        }*/

    }
}
