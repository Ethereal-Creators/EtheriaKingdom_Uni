using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> OnEnemyDestroyed; // Event to notify the spawner

    [SerializeField] float health = 2f;
    [SerializeField] float moveSpeed = 3f;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
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
        if (health <= 0)
        {
            OnEnemyDestroyed?.Invoke(gameObject); // Notify spawner
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke(gameObject); // Safety check to remove enemy when destroyed
    }
}