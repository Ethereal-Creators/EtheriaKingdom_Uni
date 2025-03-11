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
    public float circleRadius = 5f; // Radius around the crystal that the boss will move in
    public float circleSpeed = 1f; // Speed at which the boss moves around the crystal
    public float verticalAvoidanceDistance = 5f; // The height at which the boss will move to avoid the box

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

        // If a target exists, move around it
        if (target != null)
        {
            MoveAroundTarget();
        }
    }

    void MoveAroundTarget()
    {
        // Calculate the direction to move around the crystal in a circular path
        Vector3 directionToTarget = target.position - transform.position;
        Vector3 perpendicularDirection = Vector3.Cross(directionToTarget, Vector3.forward).normalized;

        // Move along the perpendicular direction (circle around the target)
        transform.Translate(perpendicularDirection * circleSpeed * Time.deltaTime, Space.World);

        // Keep facing the crystal while moving
        Vector3 directionToFace = target.position - transform.position;
        float angle = Mathf.Atan2(directionToFace.y, directionToFace.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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

    // Adjust the avoidance logic to move above the box instead of around it
    void AvoidObstacle(Collider2D obstacle)
    {
        // Check if the boss is not blocked vertically (above or below the box)
        RaycastHit2D hitAbove = Physics2D.Raycast(transform.position, Vector2.up, verticalAvoidanceDistance);
        RaycastHit2D hitBelow = Physics2D.Raycast(transform.position, Vector2.down, verticalAvoidanceDistance);

        if (hitAbove.collider == null)
        {
            // No obstacle above, move upwards to avoid the box
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        }
        else if (hitBelow.collider == null)
        {
            // No obstacle below, move downwards to avoid the box
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
        }
        else
        {
            // If blocked vertically, handle it with other logic, like stopping or trying a different approach
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
