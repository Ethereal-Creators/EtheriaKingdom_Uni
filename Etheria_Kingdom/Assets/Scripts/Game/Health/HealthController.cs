using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public GameObject FloatingTextPrefab;
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
            // Ensure the blood particles are not too far or too close
            Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0); // Slightly larger range
            Vector3 particlePosition = transform.position + randomOffset;

            // Debug log to check if the blood particle effect is being positioned correctly
            Debug.Log("Blood particle effect played at position: " + particlePosition);

            // Set the particle position and play it
            bleedingParticles.transform.position = particlePosition;
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

    private IEnumerator BlinkDamageEffect()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(blinkDuration);
        spriteRenderer.color = originalColor;
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

    private void HandleDeath()
    {
        if (rigidbody2D != null)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.isKinematic = true;
        }
    }

    public void soundWhenDamaged()
    {
        Debug.Log("Damaged");
        if (source != null)
        {
            int randomClipIndex = Random.Range(0, clips.Count);
            source.PlayOneShot(clips[randomClipIndex]);
        }
    }
}
