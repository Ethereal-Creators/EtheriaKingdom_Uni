using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // Array of item prefabs to spawn
    public Transform spawnPoint;       // Where to spawn the items (can be the box's position)

    public void SpawnRandomItem()
    {
        if (itemPrefabs.Length > 0)
        {
            // Randomly pick an item prefab from the array
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject randomItem = itemPrefabs[randomIndex];

            // Log which item is being spawned
            Debug.Log("Spawning item: " + randomItem.name);

            // Spawn the item at the box's position or a specified spawn point
            Instantiate(randomItem, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Item prefabs array is empty. No items to spawn.");
        }
    }
}
