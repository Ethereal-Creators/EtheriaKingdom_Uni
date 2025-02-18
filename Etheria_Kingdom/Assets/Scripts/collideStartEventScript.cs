using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideStartEventScript : MonoBehaviour
{

    public GameObject ProtectionObject;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (/*coll.gameObject.tag == "ennemie" && coll.gameObject.tag != "projectile" &&*/ coll.gameObject.tag == "joueur")
        {
            Debug.Log("Collided with event");
            this.gameObject.SetActive(false);
            ProtectionObject.gameObject.SetActive(true);
        }

    }
}
