using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public event Action<GameObject> OnEnemyDestroyed; // Event to notify the spawner

    [SerializeField] float health = 1f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] Color hitColor = Color.red;
    [SerializeField] float hitColorDuration = 0.1f;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDestroyed = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            target = player.transform;
        }
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
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
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashHitColor());
        }
        if (health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            OnEnemyDestroyed?.Invoke(gameObject); // Notify spawner
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashHitColor()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitColorDuration);
        spriteRenderer.color = originalColor;
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