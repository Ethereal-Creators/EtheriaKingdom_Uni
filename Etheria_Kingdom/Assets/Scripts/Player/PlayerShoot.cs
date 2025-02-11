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
        isAutoShootActive = false;
    }

    private void Update()
    {   // Start the automatic shooting when the game starts
        if (isAutoShootActive == false) {
            StartCoroutine(AutoShoot());
        } else {

        }
        
        // Any additional logic you might need to check each frame
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
        isAutoShootActive = true;
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(_timeBetweenShots); // Wait for the specified interval before shooting again
        }
    }
}

