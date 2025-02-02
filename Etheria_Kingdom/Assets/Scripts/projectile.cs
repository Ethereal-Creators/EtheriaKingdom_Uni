using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectiles;
    public Transform pointProjectile;
    public float forceProjectile = 20f;
    public float tirInterval = 1f;
    
    private float nextShotTime = 0f;

    void Update()
    {

        if (Time.time >= nextShotTime)
        {
            Tire();
            nextShotTime = Time.time + tirInterval;
        }
    }

    public void Tire()
    {
        GameObject tire = Instantiate(projectiles, pointProjectile.position, pointProjectile.rotation);
        tire.GetComponent<Rigidbody2D>().AddForce(pointProjectile.up * forceProjectile, ForceMode2D.Impulse);
        Destroy(tire, 0.5f);
    }
}

