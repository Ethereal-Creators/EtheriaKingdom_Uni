using UnityEngine;

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

    // Offset for the player's shield position
    public Vector3 playerShieldOffset = new Vector3(0f, 1f, 0f);
    public Vector3 crystalShieldOffset = Vector3.zero; // Allow customization of crystal shield offset

    // Events for when the shield expires (optional)
    public delegate void ShieldExpired(GameObject shield);
    public event ShieldExpired OnShieldExpired;

    public override void Apply(GameObject target)
    {
        // Ensure the target is not null
        if (target == null) return;

        // Find the Crystal object in the scene
        GameObject crystal = GameObject.FindGameObjectWithTag("Crystal");

        // Check if the player already has a shield to prevent multiple shields
        if (playerShield != null)
        {
            Debug.Log("Player shield already applied, destroying previous shield.");
            DestroyShield(playerShield); // Remove the previous shield if it exists
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
        shieldScaler.targetScale = new Vector3(1.3f, 1.3f, 1.3f);
        shieldScaler.initialScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Apply the shield to the crystal (if it exists)
        if (crystal != null)
        {
            if (crystalShield != null)
            {
                Debug.Log("Crystal shield already applied, destroying previous shield.");
                DestroyShield(crystalShield); // Remove the previous shield on the Crystal
            }

            crystalShield = Instantiate(shieldPrefab, crystal.transform.position, Quaternion.identity);
            crystalShield.transform.SetParent(crystal.transform); // Make the shield follow the crystal

            // Log when the crystal shield is instantiated
            Debug.Log("Crystal shield spawned at position: " + crystal.transform.position);

            // Add ShieldScaler component to handle the scaling for the crystal
            ShieldScaler crystalShieldScaler = crystalShield.AddComponent<ShieldScaler>();
            crystalShieldScaler.scaleDuration = 0.6f;
            crystalShieldScaler.targetScale = new Vector3(0.75f, 0.75f, 0.75f);
            crystalShieldScaler.initialScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

        // Start the shield timers for both shields
        playerShieldTimer = shieldDuration;
        crystalShieldTimer = shieldDuration;

        // Log the start of the shield duration
        Debug.Log("Player shield duration set to: " + playerShieldTimer + " seconds.");
        Debug.Log("Crystal shield duration set to: " + crystalShieldTimer + " seconds.");

        // Add Collider2D component to detect enemy collisions (ensure shield prefab has a collider)
        CircleCollider2D playerCollider = playerShield.GetComponent<CircleCollider2D>();
        if (playerCollider == null)
        {
            playerCollider = playerShield.AddComponent<CircleCollider2D>(); // Example, change to your preferred collider
            playerCollider.isTrigger = true; // Make sure it's a trigger
        }

        // Add collider to crystal shield
        if (crystal != null)
        {
            CircleCollider2D crystalCollider = crystalShield.GetComponent<CircleCollider2D>();
            if (crystalCollider == null)
            {
                crystalCollider = crystalShield.AddComponent<CircleCollider2D>(); // Example, change to your preferred collider
                crystalCollider.isTrigger = true; // Make sure it's a trigger
            }
        }
    }

    public void Start()
    {
        timeWhenShieldStop = Time.time + timeTilShieldStop;
    }

    public void Update()
    {
        // Update shield timers only when necessary
        if (playerShield != null)
        {
            playerShield.transform.position = playerShield.transform.parent.position + playerShieldOffset;

            // Reduce player shield timer
            playerShieldTimer -= Time.deltaTime;
            if (playerShieldTimer <= 0f)
            {
                DestroyShield(playerShield);
            }
        }

        if (crystalShield != null)
        {
            crystalShield.transform.position = crystalShield.transform.parent.position + crystalShieldOffset;

            // Reduce crystal shield timer
            crystalShieldTimer -= Time.deltaTime;
            if (crystalShieldTimer <= 0f)
            {
                DestroyShield(crystalShield);
            }
        }

        timeTilShieldStop -= Time.deltaTime;
        if (timeTilShieldStop <= 0f)
        {
            DestroyShield(crystalShield); // Destroy the crystal shield after the powerup duration ends
        }

        // Check if the shield duration has ended and destroy the scriptable object
        if (playerShieldTimer <= 0f && crystalShieldTimer <= 0f)
        {
            Destroy(this);  // Destroy the ScriptableObject itself
            Debug.Log("Shield Buff has expired and ScriptableObject is destroyed.");
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
            // Optionally, invoke a callback if there are listeners
            OnShieldExpired?.Invoke(shield);

            Destroy(shield); // Destroy the shield
            Debug.Log("Shield destroyed.");
        }
    }

    public void SetShieldTimer(float newTime)
    {
        shieldDuration = newTime; // Set the new shield duration
        playerShieldTimer = newTime; // Apply to the player shield timer
        crystalShieldTimer = newTime; // Apply to the crystal shield timer
        Debug.Log("Shield timer updated to: " + newTime);
    }
}
