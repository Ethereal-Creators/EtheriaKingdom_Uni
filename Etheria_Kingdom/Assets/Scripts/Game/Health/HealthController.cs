using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public GameObject FloatingTextPrefab;  // Reference to the FloatingTextPrefab (for damage numbers)
    public GameObject explosionPrefab;  // Reference to the explosion prefab
    public int explosionCount = 5;  // Number of explosions to create
    public float explosionDelay = 0.1f;  // Delay between each explosion

    [Header("------- Health Variables -------")]
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    [Header("------- Component references -------")]
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    [Header("------- Visual and Audio Effects -------")]
    [SerializeField]
    private Color damagedColor = Color.red;

    [SerializeField]
    private float blinkDuration = 0.2f;

    [SerializeField]
    private int deathBlinkCount = 3;

    [SerializeField]
    private ParticleSystem bleedingParticles;

    [SerializeField]
    private AudioSource source;

    public Animator myCrystal;

    public List<AudioClip> clips = new List<AudioClip>();

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    // Health regeneration variables
    private bool isRegenerating = false;
    private float regenAmountPerSecond;
    private float regenDuration;
    private float regenEndTime;

    // Explosion timers
    private float explosionTimer = 0f;
    private int currentExplosionCount = 0;

    // Blink damage effect variables
    private float blinkTimer = 0f;
    private bool isBlinking = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public float RemainingHealthPercentage => _currentHealth / _maximumHealth;
    public bool IsInvincible { get; set; }

    public void TakeDamage(float damageAmount)
    {
        ShowFloatingText(damageAmount.ToString());
        if (_currentHealth == 0 || IsInvincible) return;

        _currentHealth -= damageAmount;
        OnHealthChanged.Invoke();

        if (bleedingParticles != null)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0);
            Vector3 particlePosition = transform.position + randomOffset;
            Debug.Log("Blood particle effect played at position: " + particlePosition);
            bleedingParticles.transform.position = particlePosition;
            bleedingParticles.Play();
        }

        if (spriteRenderer != null)
        {
            isBlinking = true;  // Start the blinking effect
        }

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDied.Invoke();
            HandleDeath();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void changementCrystal()
    {
        if (myCrystal != null)
        {
            var percentageHealth = _currentHealth / _maximumHealth * 100;
            if (percentageHealth <= 70)
            {
                Debug.Log("less than 70% health");
                myCrystal.SetBool("70", true);
            }
            if (percentageHealth <= 50)
            {
                Debug.Log("less than 50% health");
                myCrystal.SetBool("50", true);
            }
            if (percentageHealth <= 25)
            {
                Debug.Log("less than 25% health");
                myCrystal.SetBool("25", true);
            }
        }
    }

    void ShowFloatingText(string text)
    {
        if (FloatingTextPrefab)
        {
            GameObject prefab = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMeshPro>().text = text;
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth) return;

        _currentHealth += amountToAdd;
        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    private void Update()
    {
        // Handle explosion spawning for the boss when it dies
        if (_currentHealth <= 0 && gameObject.name == "Demon")
        {
            HandleExplosion();
        }

        // Handle blink effect timing
        if (isBlinking)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer <= blinkDuration)
            {
                spriteRenderer.color = damagedColor;  // Show damage color
            }
            else if (blinkTimer <= blinkDuration * 2)
            {
                spriteRenderer.color = Color.white;  // Reset to original color
            }
            else
            {
                blinkTimer = 0f;  // Reset blink timer
                isBlinking = false;  // Stop blinking after cycle
            }
        }
    }

    private void HandleExplosion()
    {
        // Spawn explosions at regular intervals
        if (currentExplosionCount < explosionCount)
        {
            explosionTimer += Time.deltaTime;

            if (explosionTimer >= explosionDelay)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosionTimer = 0f;  // Reset timer
                currentExplosionCount++;  // Increase explosion count
            }
        }
        else
        {
            Destroy(gameObject);  // Destroy the boss after all explosions
        }
    }

    private void HandleDeath()
    {
        if (gameObject.name == "Demon")
        {
            if (rigidbody2D != null)
            {
                rigidbody2D.velocity = Vector2.zero;
                rigidbody2D.isKinematic = true;
            }

            // Ensure explosions happen on death
            HandleExplosion();
        }
        else
        {
            // Handle death normally for other enemies (without explosions)
            Destroy(gameObject);
        }
    }

    public void soundWhenDamaged()
    {
        Debug.Log("Damaged");
        if (source != null && clips.Count > 0)
        {
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
    }

    // Health regeneration methods
    public void StartRegeneratingHealth(float regenAmount, float duration)
    {
        if (!isRegenerating)
        {
            regenAmountPerSecond = regenAmount / duration;
            regenDuration = duration;
            regenEndTime = Time.time + duration;
            isRegenerating = true;
            StartRegeneratingHealth();
        }
    }

    private void StartRegeneratingHealth()
    {
        while (Time.time < regenEndTime && _currentHealth < _maximumHealth)
        {
            _currentHealth += regenAmountPerSecond;
            _currentHealth = Mathf.Min(_currentHealth, _maximumHealth); // Ensure health does not exceed maximum
            OnHealthChanged.Invoke();
        }
        isRegenerating = false;
    }
}
