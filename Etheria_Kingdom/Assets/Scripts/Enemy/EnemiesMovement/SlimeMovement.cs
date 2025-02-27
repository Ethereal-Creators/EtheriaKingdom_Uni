using UnityEngine;
using System.Collections; 

public class SlimeMovement : EnemyMovement
{
    [SerializeField] private float _hopSpeed = 3f;         
    [SerializeField] private float _hopInterval = 2f;     
    [SerializeField] private float _pauseAfterHop = 1f;   
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

        // Add a vertical hop force to simulate hopping
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _hopSpeed);

        // Wait for the hop to complete (this can be adjusted if needed)
        yield return new WaitForSeconds(0.3f); // The hop duration

        // Stop the movement for a moment after the hop
        _rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(_pauseAfterHop);

        _timeSinceLastHop = 0f;

        _isHopping = false; // Reset the hopping flag, ready for the next hop
    }
}
