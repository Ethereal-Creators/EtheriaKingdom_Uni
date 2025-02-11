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
    private float _timeBetweenShots; // Time between each shot (in seconds)

    private bool isAutoShootActive = false;


    private void Start()
    {
        // Start the automatic shooting when the game starts
        StartCoroutine(AutoShoot());
    }

    private void Update()
    {   
        
        
        // Any additional logic you might need to check each frame
    }

    void OnEnable()
    {
        isAutoShootActive = false;
        if (isAutoShootActive == false)
        {
            StartCoroutine(AutoShoot());
            isAutoShootActive = true;
        }
        Debug.Log("Player activted.");
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, transform.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.velocity = _bulletSpeed * transform.up;
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

