using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> OnEnemyDestroyed; // Event to notify the spawner

    [SerializeField] float health = 1f;
    [SerializeField] float moveSpeed = 2f;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDestroyed = false;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            target = player.transform;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            OnEnemyDestroyed?.Invoke(gameObject); // Notify spawner
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!isDestroyed)
        {
            OnEnemyDestroyed?.Invoke(gameObject); // Safety check to remove enemy when destroyed
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}