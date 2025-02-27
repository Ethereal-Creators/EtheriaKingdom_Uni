using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }

    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;

    private Transform _player;

    private void Awake()
    {
        // Initialize player transform if necessary (but we can detect via trigger instead)
        // _player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject that entered the trigger has the "Player" tag
        if (other.CompareTag("Crystal"))
        {
            _player = other.transform;  // Get the player's transform
            UpdateAwareness();          // Update the awareness as soon as player enters the trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone, we reset awareness
        if (other.CompareTag("Crystal"))
        {
            _player = null; // No player in range
            AwareOfPlayer = false; // Reset awareness when player is out of range
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            Vector2 enemyToPlayerVector = _player.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
    }

    // Method to handle awareness directly when needed
    private void UpdateAwareness()
    {
        if (_player != null)
        {
            Vector2 enemyToPlayerVector = _player.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;

            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }
    }
}
