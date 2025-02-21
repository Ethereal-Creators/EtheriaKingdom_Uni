using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerupEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has a PlayerMovement component
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

        var player = collision.gameObject.CompareTag("joueur");

        if (collision.gameObject.tag == "joueur") // This means the PlayerMovement component exists on the object
        {
            powerupEffect.Apply(collision.gameObject);
            Destroy(gameObject); // Destroy the power-up after applying it
        }
    }
}
