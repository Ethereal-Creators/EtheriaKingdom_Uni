using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactProjectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ennemie"))
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }
}

