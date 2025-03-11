using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;  // Reference to the boss prefab
    public Vector2 spawnPosition;  // Position to spawn the boss
    public float spawnDelay = 3f;  // Delay before the boss spawns

    private bool bossSpawned = false;

    void Update()
    {
        // Spawn the boss once after a delay
        if (!bossSpawned)
        {
            Invoke("SpawnBoss", spawnDelay);  // Invoke SpawnBoss after a delay
            bossSpawned = true;
        }
    }

    void SpawnBoss()
    {
        // Instantiate the boss at the specified position
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
}
