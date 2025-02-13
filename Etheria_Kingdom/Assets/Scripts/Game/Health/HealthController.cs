using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    // Reference to the sprite renderer
    private SpriteRenderer spriteRenderer;

    // The damaged color to blink
    [SerializeField]
    private Color damagedColor = Color.red;

    // Duration for the blink effect
    [SerializeField]
    private float blinkDuration = 0.2f;

    // Number of blinks before death
    [SerializeField]
    private int deathBlinkCount = 3;

    // Reference to the bleeding particle system
    [SerializeField]
    private ParticleSystem bleedingParticles;

    // Reference to the Rigidbody2D component for movement
    private Rigidbody2D rigidbody2D;

    // Event declarations
    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    public List<AudioClip> clips = new List<AudioClip>();

    [SerializeField]
    private AudioSource source;

    void Start()
    {
        //Debug.Log(clips.Count);
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("projectile") /*&& clips.Count > 0*/)
        {
            //Debug.Log("collision 2d healt + projectile");
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
    }

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvincible { get; set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0 || IsInvincible) return;

        _currentHealth -= damageAmount;
        OnHealthChanged.Invoke();

        if (bleedingParticles != null)
        {
            bleedingParticles.transform.position = transform.position;
            bleedingParticles.Play();
        }

        if (spriteRenderer != null)
        {
            StartCoroutine(BlinkDamageEffect());
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

    private IEnumerator BlinkDamageEffect()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = originalColor;
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;
        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    private void HandleDeath()
    {
        if (rigidbody2D != null)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.isKinematic = true;
        }

        // Start blinking before despawning
        /*StartCoroutine(BlinkDeathEffect());*/
    }

    private IEnumerator BlinkDeathEffect()
    {
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < deathBlinkCount; i++)
        {
            spriteRenderer.color = damagedColor;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }

        Destroy(gameObject);
    }
}
