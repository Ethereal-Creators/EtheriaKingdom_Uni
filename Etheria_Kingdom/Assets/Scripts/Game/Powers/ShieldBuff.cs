using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Shield Buff", menuName = "Powerups/Shield Buff")]
public class ShieldBuff : PowerUpEffect
{
    public GameObject shieldPrefab; // The shield prefab that will appear on the player and Crystal
    private GameObject playerShield;
    private GameObject crystalShield;
    private float shieldDuration = 10f; // Duration for the shield to last
    private float playerShieldTimer = 0f;
    private float crystalShieldTimer;

    private float timeTilShieldStop = 10f;
    private float timeWhenShieldStop;

    public override void Apply(GameObject target)
    {
        // Find the Crystal object in the scene
        GameObject crystal = GameObject.FindGameObjectWithTag("Crystal");

        // Check if the player already has a shield to prevent multiple shields
        if (playerShield != null)
        {
            Debug.Log("Player shield already applied, destroying previous shield.");
            Destroy(playerShield); // Remove the previous shield if it exists
        }

        // Instantiate the shield prefab at the player's position
        Debug.Log("Applying shield buff to player: " + target.name);
        playerShield = Instantiate(shieldPrefab, target.transform.position, Quaternion.identity);
        playerShield.transform.SetParent(target.transform); // Make the shield follow the player

        // Log when the shield is instantiated
        Debug.Log("Player shield spawned at position: " + target.transform.position);

        // Add ShieldScaler component to handle the scaling for the player
        ShieldScaler shieldScaler = playerShield.AddComponent<ShieldScaler>();
        shieldScaler.scaleDuration = 0.6f;
        shieldScaler.targetScale = new Vector3(1f, 1f, 1f);
        shieldScaler.initialScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Apply the shield to the crystal (if it exists)
        if (crystal != null)
        {
            if (crystalShield != null)
            {
                Debug.Log("Crystal shield already applied, destroying previous shield.");
                Destroy(crystalShield); // Remove the previous shield on the Crystal
            }

            crystalShield = Instantiate(shieldPrefab, crystal.transform.position, Quaternion.identity);
            crystalShield.transform.SetParent(crystal.transform); // Make the shield follow the crystal

            // Log when the crystal shield is instantiated
            Debug.Log("Crystal shield spawned at position: " + crystal.transform.position);

            // Add ShieldScaler component to handle the scaling for the crystal
            ShieldScaler crystalShieldScaler = crystalShield.AddComponent<ShieldScaler>();
            crystalShieldScaler.scaleDuration = 0.6f;
            crystalShieldScaler.targetScale = new Vector3(0.6f, 0.6f, 0.6f);
            crystalShieldScaler.initialScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        // Start the shield timers for both shields
        playerShieldTimer = shieldDuration;
        crystalShieldTimer = shieldDuration;

        // Log the start of the shield duration
        Debug.Log("Player shield duration set to: " + playerShieldTimer + " seconds.");
        Debug.Log("Crystal shield duration set to: " + crystalShieldTimer + " seconds.");

        // Add Collider component to detect enemy collisions (ensure shield prefab has a collider)
        Collider playerCollider = playerShield.GetComponent<Collider>();
        if (playerCollider == null)
        {
            playerCollider = playerShield.AddComponent<SphereCollider>(); // Example, change to your preferred collider
            //playerCollider.isTrigger = true; // Make sure it's a trigger
        }

        // Add collider to crystal shield
        if (crystal != null)
        {
            Collider crystalCollider = crystalShield.GetComponent<Collider>();
            if (crystalCollider == null)
            {
                crystalCollider = crystalShield.AddComponent<SphereCollider>(); // Example, change to your preferred collider
                //crystalCollider.isTrigger = true; // Make sure it's a trigger
            }
        }
    }

    public void Start()
    {
        timeWhenShieldStop = Time.time + timeTilShieldStop;
    }

    public void Update()
    {
        // Decrease the timer for each shield independently
        if (playerShield != null)
        {
            playerShieldTimer -= Time.deltaTime;

            // Debug the remaining time on the player shield
            Debug.Log("Player shield time remaining: " + Mathf.Max(playerShieldTimer, 0f).ToString("F2") + " seconds.");

            // Destroy the player shield when time runs out
            if (playerShieldTimer <= 0f)
            {
                DestroyShield(playerShield);
            }
        }

        if (GameObject.Find("ShieldPowerUp") != null)
        {
            /*
            crystalShieldTimer -= Time.deltaTime;

            // Debug the remaining time on the crystal shield
            Debug.Log("Crystal shield time remaining: " + Mathf.Max(crystalShieldTimer, 0f).ToString("F2") + " seconds.");

            // Destroy the crystal shield when time runs out
            if (crystalShieldTimer <= 0f)
            {
                DestroyShield(crystalShield);
            }*/
            Debug.Log("Crystal shield time remaining: " + Mathf.Max(crystalShieldTimer, 0f).ToString("F2") + " seconds.");

            timeTilShieldStop -= Time.deltaTime;
            if (timeTilShieldStop < 0f)
            {
                DestroyShield(crystalShield);
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if the collider that touched the shield has the "ennemie" tag
        if (other.CompareTag("ennemie"))
        {
            Debug.Log("Enemy touched the shield. Destroying both shields.");
            DestroyShield(playerShield);
            DestroyShield(crystalShield); // Destroy shields when an enemy touches them
        }
    }

    private void DestroyShield(GameObject shield)
    {
        if (shield != null)
        {
            Destroy(shield); // Destroy the shield
            Debug.Log("Shield destroyed.");
        }
    }

    // Public method to dynamically change the shield timer
    public void SetShieldTimer(float newTime)
    {
        shieldDuration = newTime; // Set the new shield duration
        playerShieldTimer = newTime; // Apply to the player shield timer
        crystalShieldTimer = newTime; // Apply to the crystal shield timer
        Debug.Log("Shield timer updated to: " + newTime);
    }
}
