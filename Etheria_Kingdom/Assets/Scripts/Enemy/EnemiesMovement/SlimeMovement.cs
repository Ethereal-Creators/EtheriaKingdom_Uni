using UnityEngine;
using System.Collections;

public class SlimeMovement : EnemyMovement
{
    [SerializeField] private float _hopSpeed = 3f;
    [SerializeField] private float _hopInterval = 2f;
    [SerializeField] private float _pauseAfterHop = 1f;
    [SerializeField] private float _hopDurationMin = 0.3f; // Min hop duration
    [SerializeField] private float _hopDurationMax = 0.5f; // Max hop duration
    [SerializeField] private float _bounceSpeed = 1f; // Small bounce effect after hop

    private bool _isHopping = false;  // Flag to check if the slime is currently hopping
    private float _timeSinceLastHop = 0f; // Timer to track hop intervals

    protected override void Start()
    {
        base.Start(); // Calls base Start to find the Crystal
    }

    protected override void Update()
    {
        base.Update(); // Calls base Update to move towards the Crystal

        // Update the timer for hop intervals
        _timeSinceLastHop += Time.deltaTime;

        // If it's time for the next hop and the slime isn't already hopping
        if (_timeSinceLastHop >= _hopInterval && !_isHopping)
        {
            StartCoroutine(HopAndPause());
        }
    }

    private IEnumerator HopAndPause()
    {
        _isHopping = true; // Set the flag that the slime is hopping

        // Save current horizontal velocity to preserve it during the hop
        float horizontalVelocity = _rigidbody.velocity.x;

        // Add a vertical hop force to simulate hopping
        _rigidbody.velocity = new Vector2(horizontalVelocity, _hopSpeed);

        // Randomize the hop duration to add some unpredictability
        float hopDuration = Random.Range(_hopDurationMin, _hopDurationMax);

        // Wait for the hop to complete (this can be adjusted if needed)
        yield return new WaitForSeconds(hopDuration); // Randomized hop duration

        // Add a small bounce effect after the hop to simulate landing
        float bounceTimer = 0f;
        while (bounceTimer < 0.1f) // Small duration for bounce
        {
            _rigidbody.velocity = new Vector2(horizontalVelocity, Mathf.Sin(bounceTimer * Mathf.PI) * _bounceSpeed);
            bounceTimer += Time.deltaTime;
            yield return null;
        }

        // Stop vertical movement for a moment after the bounce
        _rigidbody.velocity = new Vector2(horizontalVelocity, 0f);

        // Wait for the pause time before the next hop
        yield return new WaitForSeconds(_pauseAfterHop);

        _timeSinceLastHop = 0f;

        _isHopping = false; // Reset the hopping flag, ready for the next hop
    }
}
