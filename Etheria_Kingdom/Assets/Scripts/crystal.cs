using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactCrystal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ennemie"))
        {
            Destroy(collision.gameObject,0.05f);
        }

    }
}
