using UnityEngine;

public class SkeletonMovement : EnemyMovement
{
    [SerializeField] private float _speed = 2f; // Movement speed
    [SerializeField] private float _rotationSpeed = 5f; // Rotation speed for smooth turning
    private float _changeDirectionCooldown;

    // Target (Crystal)
    private Transform _crystal;

    // Patrol range (for random movement when not heading towards the Crystal)
    private Vector2 _patrolDirection;
    private float _patrolCooldown;

    // Start method to initialize values and find the Crystal
    private void Start()
    {
        base.Awake(); // Call the base class's Awake() method
        _changeDirectionCooldown = Random.Range(1f, 5f); // Randomize cooldown
        _patrolCooldown = Random.Range(3f, 6f); // Randomize patrol cooldown

        // Find the Crystal GameObject by tag
        _crystal = GameObject.FindGameObjectWithTag("Crystal")?.transform;

        if (_crystal == null)
        {
            Debug.LogWarning("Crystal not found!");
        }
    }

    // Update method to handle movement toward the Crystal
    private void Update()
    {
        if (_crystal != null)
        {
            MoveTowardsCrystal();
        }
        else
        {
            PatrolRandomly();
        }

        HandleRandomMovement();
    }

    private void MoveTowardsCrystal()
    {
        // Get direction to the Crystal
        Vector2 directionToCrystal = (_crystal.position - transform.position).normalized;

        // Smoothly rotate towards the crystal
        float step = _rotationSpeed * Time.deltaTime;

        // Calculate the angle between the current direction and the direction to the crystal
        float angle = Mathf.Atan2(directionToCrystal.y, directionToCrystal.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, step);

        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));

        // Set velocity in the direction of the target
        _rigidbody.velocity = directionToCrystal * _speed;
    }

    private void PatrolRandomly()
    {
        // Move randomly when not targeting the crystal
        _patrolCooldown -= Time.deltaTime;

        if (_patrolCooldown <= 0)
        {
            // Randomly change patrol direction after cooldown
            _patrolDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _patrolCooldown = Random.Range(3f, 6f); // Reset patrol cooldown
        }

        // Apply patrol movement
        _rigidbody.velocity = _patrolDirection * _speed;
    }

    private void HandleRandomMovement()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            // Randomly change direction by a random angle
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _rigidbody.velocity = rotation * _rigidbody.velocity;

            // Reset cooldown
            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }
}
