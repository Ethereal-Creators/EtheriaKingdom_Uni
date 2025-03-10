using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public string targetTag = "Crystal";  // Tag of the target (e.g., the crystal)
    public string obstacleTag = "Box";  // Tag of the obstacle (Box)
    public float moveSpeed = 3f;  // Speed at which the boss moves
    public float obstacleDetectionRange = 2f;  // Range to detect obstacles ahead
    public float avoidanceStrength = 3f;  // Strength of the avoidance (how much it will move around the obstacle)
    public float backUpDistance = 2f;  // Distance to back up when colliding with the crystal
    public float backUpDuration = 1f;  // Duration for the backup movement

    private Transform target;  // The target the boss is moving towards (e.g., the crystal)
    private bool isCollidingWithBox = false;  // Flag to check if the boss collided with the Box
    private bool hasTeleported = false;  // Flag to check if the boss has already teleported
    private bool isBackingUp = false;  // Flag to check if the boss is backing up after hitting the crystal
    private float backUpTimer = 0f;  // Timer to control the backup duration

    private void Start()
    {
        // Find the GameObject with the "Crystal" tag and get its transform
        GameObject crystal = GameObject.FindGameObjectWithTag(targetTag);
        if (crystal != null)
        {
            target = crystal.transform;  // Set the target to the crystal's position
        }
        else
        {
            Debug.LogWarning("No GameObject with the 'Crystal' tag found!");
        }
    }

    private void Update()
    {
        if (isCollidingWithBox && !hasTeleported)
        {
            // Boss is colliding with the Box, stop shooting and teleport only if it hasn't teleported already
            StopShootingFireballs();
            TeleportToRandomSide();
            hasTeleported = true;  // Set flag to prevent teleporting again until the next collision
            return; // Prevent the boss from moving towards the target or detecting obstacles
        }

        // If the boss is colliding with the Crystal, back up and avoid the crystal
        if (isBackingUp)
        {
            BackUpFromCrystal();
            return;  // Skip the movement towards the target while backing up
        }

        // If a target exists, move towards it
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;  // Direction to the target (crystal)

        // Check if there is an obstacle directly in front of the boss
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, obstacleDetectionRange);

        if (hit.collider != null)
        {
            // If the hit object is tagged "Box", avoid it
            if (hit.collider.CompareTag(obstacleTag))
            {
                AvoidObstacle(hit.collider, direction);
            }
            else
            {
                // If it's another object, move forward (or stop based on your needs)
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            // No obstacle detected, move directly towards the target
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    void AvoidObstacle(Collider2D obstacle, Vector2 originalDirection)
    {
        // Simple avoidance logic: if an obstacle is detected, try moving around it
        Vector2 avoidanceDirection = Vector2.zero;

        // Raycast to the left and right of the boss to find an open path
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, (Vector2)transform.up + new Vector2(-1, 0), obstacleDetectionRange);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, (Vector2)transform.up + new Vector2(1, 0), obstacleDetectionRange);

        // If there's an open space to the left, avoid to the left
        if (hitLeft.collider == null)
        {
            avoidanceDirection = new Vector2(-1, 0);  // Move left
        }
        // If there's an open space to the right, avoid to the right
        else if (hitRight.collider == null)
        {
            avoidanceDirection = new Vector2(1, 0);  // Move right
        }

        // If there is space to avoid, move around the obstacle
        if (avoidanceDirection != Vector2.zero)
        {
            // Move the boss to the side (left or right)
            transform.Translate(avoidanceDirection * avoidanceStrength * Time.deltaTime);

            // After avoiding, continue moving toward the target (adjust as needed)
            transform.Translate(originalDirection * moveSpeed * Time.deltaTime);
        }
        else
        {
            // If no avoidance direction is found, simply stop (or use other fallback logic)
            // For now, the boss will stop in front of the obstacle
            transform.Translate(originalDirection * 0f * Time.deltaTime);
        }
    }

    // Method to stop shooting fireballs (called when the boss collides with the "Box")
    void StopShootingFireballs()
    {
        // Get the Boss script attached to the same GameObject as this BossMovement script
        Boss bossScript = GetComponent<Boss>();
        if (bossScript != null)
        {
            bossScript.StopShooting();  // Call StopShooting on the Boss script
        }
    }

    // Method to teleport the boss to a random side of the screen
    void TeleportToRandomSide()
    {
        // Determine random screen side (left, right, top, or bottom)
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Convert screen coordinates to world coordinates
        Vector2 randomPosition = Vector2.zero;
        int side = Random.Range(0, 4);

        // Randomly pick a side to teleport to
        switch (side)
        {
            case 0: // Left side
                randomPosition = Camera.main.ScreenToWorldPoint(new Vector2(0, Random.Range(0, screenHeight)));
                break;
            case 1: // Right side
                randomPosition = Camera.main.ScreenToWorldPoint(new Vector2(screenWidth, Random.Range(0, screenHeight)));
                break;
            case 2: // Top side
                randomPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, screenWidth), screenHeight));
                break;
            case 3: // Bottom side
                randomPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, screenWidth), 0));
                break;
        }

        // Set the boss's position to the random position on the chosen side
        transform.position = randomPosition;
    }

    // Method to handle collision with Box (called when collision occurs)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            // Boss collided with a Box, stop shooting, teleport and prevent multiple teleports
            isCollidingWithBox = true;
        }

        if (collision.gameObject.CompareTag(targetTag))
        {
            // Boss collided with the Crystal, start backing up
            isBackingUp = true;
            backUpTimer = 0f;  // Reset the backup timer
        }
    }

    // Method to handle the backup behavior when colliding with the Crystal
    void BackUpFromCrystal()
    {
        if (backUpTimer < backUpDuration)
        {
            // Back up by moving in the opposite direction from the target
            Vector2 backUpDirection = (transform.position - target.position).normalized;
            transform.Translate(backUpDirection * moveSpeed * Time.deltaTime);
            backUpTimer += Time.deltaTime;
        }
        else
        {
            // After backing up, start moving around the crystal
            isBackingUp = false;
            AvoidCrystal();
        }
    }

    // Method to move around the Crystal
    void AvoidCrystal()
    {
        Vector2 avoidanceDirection = Vector2.zero;

        // Raycast to the left and right to find an open path around the crystal
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, (Vector2)transform.up + new Vector2(-1, 0), obstacleDetectionRange);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, (Vector2)transform.up + new Vector2(1, 0), obstacleDetectionRange);

        // If there's an open space to the left, move left
        if (hitLeft.collider == null)
        {
            avoidanceDirection = new Vector2(-1, 0);  // Move left
        }
        // If there's an open space to the right, move right
        else if (hitRight.collider == null)
        {
            avoidanceDirection = new Vector2(1, 0);  // Move right
        }

        if (avoidanceDirection != Vector2.zero)
        {
            // Move around the crystal
            transform.Translate(avoidanceDirection * avoidanceStrength * Time.deltaTime);
        }
        else
        {
            // If no avoidance direction is found, move back slightly or continue your logic
            transform.Translate(Vector2.zero);
        }
    }

    // Reset the teleport flag when the boss exits the collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            // Boss has exited the box collision, allow teleport again when next collision happens
            isCollidingWithBox = false;
            hasTeleported = false;  // Allow teleportation on the next collision
        }

        if (collision.gameObject.CompareTag(targetTag))
        {
            // Reset backup behavior after leaving the crystal
            isBackingUp = false;
        }
    }
}
