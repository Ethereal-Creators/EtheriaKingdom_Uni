using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDecorSpawner : MonoBehaviour
{
    public GameObject[] decorPrefabs; // Array to hold your decor prefabs
    public Transform[] spawnPoints;   // Array of spawn points in the scene
    public float spawnDelay = 1f;     // Delay before spawning decor

    void Start()
    {
        // Spawn decor objects randomly at different positions when the game starts
        SpawnDecor();
    }

    void SpawnDecor()
    {
        // Loop through spawn points and instantiate decor objects at those points
        foreach (var spawnPoint in spawnPoints)
        {
            // Choose a random decor prefab from the array
            int randomIndex = Random.Range(0, decorPrefabs.Length);

            // Choose a random position from spawn points
            Vector3 spawnPosition = spawnPoint.position;

            // Instantiate the decor prefab at the spawn position
            Instantiate(decorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
    }
}
