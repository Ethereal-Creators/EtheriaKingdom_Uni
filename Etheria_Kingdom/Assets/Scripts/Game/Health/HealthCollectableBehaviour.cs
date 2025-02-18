using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectableBehaviour : MonoBehaviour, ICollectableBehaviour
{
    [SerializeField]
    private float _healthAmount;

    // Assuming you have a reference to the Crystal object
    [SerializeField]
    private GameObject crystal;

    public void OnCollected(GameObject player)
    {
        if (crystal != null)
        {
            HealthController healthController = crystal.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.AddHealth(_healthAmount); // Add health to the crystal
            }
            else
            {
                Debug.LogWarning("HealthController not found on the Crystal.");
            }
        }
        else
        {
            Debug.LogWarning("Crystal reference is missing.");
        }

        // Destroy the collectable after collection
        Destroy(gameObject);
    }
}