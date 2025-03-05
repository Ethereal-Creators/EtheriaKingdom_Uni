using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float blinkDuration = 1f;
    public float despawnDelay = 1f;
    public Vector3 spawnOffset = Vector3.zero;
    public AudioSource hitAudioSource;    // Audio source for hit sound
    public AudioSource spawnAudioSource;  // Audio source for spawn sound
    public AudioClip[] hitSounds;         // Array of hit sounds
    public AudioClip[] spawnSounds;       // Array of spawn sounds
    public ParticleSystem destructionEffect;
    public ParticleSystem hitEffect;
    public float itemDropChance = 0.75f;
    public Vector3 scaleShrinkAmount = new Vector3(0.1f, 0.1f, 0.1f);
    public float scaleShrinkDuration = 0.3f;

    private Renderer boxRenderer;
    private Color originalColor;
    private Vector3 originalScale;

    private void Start()
    {
        boxRenderer = GetComponent<Renderer>();
        originalScale = transform.localScale;

        if (boxRenderer != null)
        {
            originalColor = boxRenderer.material.color;
        }
        else
        {
            Debug.LogWarning("No Renderer component found on this object.");
        }

        PlaySpawnSound();
    }

    public void OnHit()
    {
        if (boxRenderer != null)
        {
            BoxCollider2D colliderOfBox = GetComponent<BoxCollider2D>();
            colliderOfBox.enabled = false;
            PlayHitSound();
            PlayHitEffect();
            StartCoroutine(BlinkAndDespawn());
        }
    }

    private IEnumerator BlinkAndDespawn()
    {
        boxRenderer.material.color = Color.red;
        yield return new WaitForSeconds(blinkDuration);
        boxRenderer.material.color = originalColor;
        yield return StartCoroutine(ShrinkAndDestroy());
        yield return new WaitForSeconds(despawnDelay);
        PlayDestructionEffect();

        if (Random.value <= itemDropChance)
        {
            SpawnRandomItem();
        }

        Destroy(gameObject);
    }

    private IEnumerator ShrinkAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale - scaleShrinkAmount;

        while (elapsedTime < scaleShrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / scaleShrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            Debug.Log("Spawning random item: " + itemPrefabs[randomIndex].name);
            Instantiate(itemPrefabs[randomIndex], transform.position + spawnOffset, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No items to spawn. Please assign item prefabs.");
        }
    }

    private void PlayHitSound()
    {
        if (hitAudioSource != null && hitSounds.Length > 0)
        {
            AudioClip randomClip = hitSounds[Random.Range(0, hitSounds.Length)];
            hitAudioSource.pitch = Random.Range(0.8f, 1.2f);
            hitAudioSource.PlayOneShot(randomClip);
        }
        else
        {
            Debug.LogWarning("Hit AudioSource or hit sounds are not assigned.");
        }
    }

    private void PlaySpawnSound()
    {
        if (spawnAudioSource != null && spawnSounds.Length > 0)
        {
            AudioClip randomClip = spawnSounds[Random.Range(0, spawnSounds.Length)];
            spawnAudioSource.PlayOneShot(randomClip);
        }
        else
        {
            Debug.LogWarning("Spawn AudioSource or spawn sounds are not assigned.");
        }
    }

    private void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            hitEffect.Play();
        }
        else
        {
            Debug.LogWarning("Hit effect is not assigned.");
        }
    }

    private void PlayDestructionEffect()
    {
        if (destructionEffect != null)
        {
            Instantiate(destructionEffect, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Destruction effect is not assigned.");
        }
    }
}