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


    // List for the enemy sound hit
    public List<AudioClip> clips = new List<AudioClip>();

    [SerializeField]
    public AudioSource source;

    // Reference to the bleeding particle system
    [SerializeField]
    private ParticleSystem bleedingParticles;

    // Reference to the Rigidbody2D component for movement
    private Rigidbody2D rigidbody2D;

    // Event declarations
    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    // charge le son au debut
    void Start()
    {
        //Debug.Log(clips.Count);
        source = GetComponent<AudioSource>();
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
        // Get the SpriteRenderer and Rigidbody2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();
        

        // Play bleeding particles when damage is taken
        if (bleedingParticles != null)
        {
            bleedingParticles.transform.position = transform.position;
            bleedingParticles.Play();
        }

        // Trigger the sprite blink effect
        if (spriteRenderer != null)
        {
            StartCoroutine(BlinkDamageEffect());
        }

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
            HandleDeath();  // Handle the death logic here
        }
        else
        {
            OnDamaged.Invoke();
        }
    }
 
    private IEnumerator BlinkDamageEffect()
    {
        // Store the original color of the sprite
        Color originalColor = spriteRenderer.color;

        // Change the sprite color to the damaged color
        spriteRenderer.color = damagedColor;
        
        // Wait for the specified blink duration
        yield return new WaitForSeconds(blinkDuration);

        // Revert the sprite color back to the original color
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

    // This method is called when the object dies
    private void HandleDeath()
    {
        // Stop movement by disabling Rigidbody2D or movement-related components
        if (rigidbody2D != null)
        {
            rigidbody2D.velocity = Vector2.zero;   // Stop any current movement
            rigidbody2D.isKinematic = true;        // Disable physics interactions (optional)
        }

        // Start blinking before despawning
        /*StartCoroutine(BlinkDeathEffect());*/

        // Additional death-related logic can go here (e.g., playing an animation or sound)
    }

    private IEnumerator BlinkDeathEffect()
    {
        // Store the original color of the sprite
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < deathBlinkCount; i++)
        {
            // Change the sprite color to the damaged color (e.g., red)
            spriteRenderer.color = damagedColor;

            // Wait for the blink duration
            yield return new WaitForSeconds(blinkDuration);

            // Revert the sprite color back to the original color
            spriteRenderer.color = originalColor;

            // Wait again before the next blink
            yield return new WaitForSeconds(blinkDuration);
        }

        // After blinking, destroy the object
        Destroy(gameObject);
    }
}
