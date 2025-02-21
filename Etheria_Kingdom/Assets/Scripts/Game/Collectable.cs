using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour _collectableBehaviour;

    private void Awake()
    {
        _collectableBehaviour = GetComponent<ICollectableBehaviour>();
    }

 private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player")) // Ensures it's the player
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null && _collectableBehaviour != null)
        {
            _collectableBehaviour.OnCollected(player.gameObject);
            Debug.Log("Item collected!");
            Destroy(gameObject);
        }
        else
        {
            // Log a message if PlayerMovement or ICollectableBehaviour is missing
            if (player == null)
                Debug.LogWarning("PlayerMovement component not found on player.");
            if (_collectableBehaviour == null)
                Debug.LogWarning("ICollectableBehaviour not assigned.");
        }
    }
}
}
