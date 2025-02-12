using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactCrystal : MonoBehaviour
{
    [SerializeField]
    private float destructionDelay = 0.5f; // Adjustable delay before destroying the enemy

    [SerializeField]
    private GameObject destructionEffectPrefab; // Particle effect for enemy destruction (Optional)

    [SerializeField]
    private AudioClip destructionSound; // Sound effect when an enemy is destroyed
    private AudioSource audioSource;

    private void Awake()
    {
        // Set up the audio source component
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the crystal is an enemy
        if (collision.gameObject.CompareTag("ennemie"))
        {
         

            // Optional: Play destruction sound
            if (destructionSound && audioSource)
            {
                audioSource.PlayOneShot(destructionSound);
            }

            // Optional: Instantiate particle effect for destruction
            if (destructionEffectPrefab)
            {
                Instantiate(destructionEffectPrefab, collision.transform.position, Quaternion.identity);
            }

           

            // Destroy the enemy after a short delay
            Destroy(collision.gameObject, destructionDelay);
        }
    }
}
