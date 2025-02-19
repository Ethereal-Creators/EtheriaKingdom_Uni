using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerupEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has a PlayerMovement component
        PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();

        if (playerMovement != null) // This means the PlayerMovement component exists on the object
        {
            powerupEffect.Apply(collision.gameObject);
            Destroy(gameObject); // Destroy the power-up after applying it
        }
    }
}
