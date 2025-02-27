using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _screenBorder;

    protected Rigidbody2D _rigidbody;  // Changed to protected so derived classes can access it
    private PlayerAwarenessController _playerAwarenessController;
    protected Vector2 _targetDirection;  // Changed to protected so derived classes can access it
    private float _changeDirectionCooldown;
    protected Camera _camera;  // Changed to protected so derived classes can access it

    // For crystal detection
    protected Transform target;  // Changed to protected so derived classes can access it
    protected Vector2 moveDirection;  // Changed to protected so derived classes can access it

    // Called when the script is initialized
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
        _camera = Camera.main;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Get the Crystal's position
        GameObject crystal = GameObject.FindGameObjectWithTag("Crystal");
        if (crystal != null)
        {
            target = crystal.transform;
        }
        else
        {
            Debug.LogError("Crystal not found in the scene!");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Override in derived classes to provide custom movement logic
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleEnemyOffScreen();
    }

    // FixedUpdate is called at a fixed time interval
    protected virtual void FixedUpdate()
    {
        // Override in derived classes to modify movement
        SetVelocity();

        // Handle movement towards the target (if any)
        if (target)
        {
            // Calculate direction towards the target (Crystal)
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rigidbody.rotation = Mathf.LerpAngle(_rigidbody.rotation, angle, _rotationSpeed * Time.deltaTime);

            // Move towards the target (Crystal)
            moveDirection = direction;
            _rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * _speed;
        }
    }

    // Handle random direction changes
    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;

        if (_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;
            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    // Handle player targeting if the player is within range
    private void HandlePlayerTargeting()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    // Prevent the enemy from going off-screen by reversing its direction when it hits the screen edge
    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < _screenBorder && _targetDirection.x < 0) ||
            (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }

        if ((screenPosition.y < _screenBorder && _targetDirection.y < 0) ||
            (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    // Set the velocity of the enemy to move towards the target direction
    protected void SetVelocity()
    {
        _rigidbody.velocity = _targetDirection * _speed;
    }

    // New method to apply knockback when the enemy is hit
    public void ApplyKnockback(Vector2 direction, float force = 5f)
    {
        // Apply force in the opposite direction of the bullet's impact
        _rigidbody.AddForce(-direction * force, ForceMode2D.Impulse);
    }
}
