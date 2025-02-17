using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _shootSound;

    private bool isAutoShootActive = false;

    private float time = 0.0f;

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

    GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, _gunOffset.rotation);
    
    Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
    
    rigidbody.velocity = _bulletSpeed * _gunOffset.up;

    Destroy(bullet, 0.1f);

    if (_audioSource != null && _shootSound != null)
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_shootSound);
    }
}
}



