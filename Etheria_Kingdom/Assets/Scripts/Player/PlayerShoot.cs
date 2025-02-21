using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    public float _timeBetweenShots; // Time between each shot (in seconds)

    [SerializeField]
    private AudioSource _audioSource; // Reference to AudioSource component
    [SerializeField]
    private AudioClip _shootSound; // Reference to the shooting sound effect

    private bool isAutoShootActive = false;

    private float time = 0.0f;

    private void Start()
    {
        // Start the automatic shooting when the game starts
        // StartCoroutine(AutoShoot());
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= _timeBetweenShots)
        {
            time = 0.0f;
            FireBullet();
        }
    }

    void OnEnable()
    {
        isAutoShootActive = false;
        if (isAutoShootActive == false)
        {
            Debug.Log("auto shoot active");
            isAutoShootActive = true;
        }
    }

    void OnDisable()
    {
        if (isAutoShootActive == true)
        {
            Debug.Log("auto shoot stop");
            isAutoShootActive = false;
        }
    }

 private void FireBullet()
{
    Debug.Log("Shooting!");

    // Instantiate bullet
    GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
    Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
    rigidbody.velocity = _bulletSpeed * transform.up;

    // Play the shooting sound effect with random pitch
    if (_audioSource != null && _shootSound != null)
    {
        // Set a random pitch between 0.8f and 1.2f (you can adjust these values as needed)
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_shootSound);
    }
}

    private IEnumerator AutoShoot()
    {
        // Continuously fire bullets with a time delay between each shot
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(_timeBetweenShots); // Wait for the specified interval before shooting again
        }
    }
}
