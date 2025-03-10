using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;  // Reference to the boss prefab
    public Vector2 spawnPosition;  // Position to spawn the boss
    private bool bossSpawned = false;

    void Update()
    {
        // Spawn the boss once when it's not spawned yet
        if (!bossSpawned)
        {
            SpawnBoss();
            bossSpawned = true;
        }
    }

    void SpawnBoss()
    {
        // Instantiate the boss at the specified position
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}
