using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    private float bulletLastTime;
    private bool bulletActive = false;


    private void Start()
    {
        // Start the automatic shooting when the game starts
        // StartCoroutine(AutoShoot());
    }

    private void Update()
    {
        if (isAutoShootActive == false) {
            bulletActive = true;
            if (bulletActive)
            {
                FireBullet();

                Debug.Log("Shoot");

                bulletLastTime = Time.time;
            }
            else
            {

                if (Time.time - bulletLastTime > 0.8f)
                {
                    Debug.Log("Don't shoot");
                    bulletActive = false;
                }
                else
                {
                    Debug.Log("To be set incative");
                    
                }

            }
        }
        

        // Any additional logic you might need to check each frame
    }

    void OnEnable()
    {
        isAutoShootActive = false;
        if (isAutoShootActive == false)
        {

            isAutoShootActive = true;

        }
    }

    void OnDisable()
    {
        isAutoShootActive = false;
    }

    private void FireBullet()
    {
        Debug.Log("Shoting!");
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

