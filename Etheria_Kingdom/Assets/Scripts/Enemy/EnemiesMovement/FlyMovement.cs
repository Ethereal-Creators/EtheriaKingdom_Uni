using UnityEngine;

public class FlyMovement : EnemyMovement
{
    [SerializeField] private float _zigzagSpeed = 2f;
    [SerializeField] private float _zigzagAmplitude = 1f;
    [SerializeField] private float _zigzagFrequency = 2f;

    private float _time;

    // Start method - no override keyword
    private void Start()
    {
        base.Awake(); // Call the base class's Awake() method if necessary
    }

    // Update method - no override keyword
    private void Update()
    {
        HandleZigzagMovement();
    }

    private void HandleZigzagMovement()
    {
        _time += Time.deltaTime * _zigzagFrequency;
        float zigzagOffset = Mathf.Sin(_time) * _zigzagAmplitude;

        _rigidbody.velocity = new Vector2(_zigzagSpeed, zigzagOffset);
    }
}
