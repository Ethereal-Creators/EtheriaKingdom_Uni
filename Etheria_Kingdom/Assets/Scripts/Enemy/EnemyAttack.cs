using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the collided object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the HealthController of the player object
            var healthController = collision.gameObject.GetComponent<HealthController>();

            // If the player has a HealthController, apply the damage
            if (healthController != null)
            {
                healthController.TakeDamage(_damageAmount);
            }
        }
    }
}
