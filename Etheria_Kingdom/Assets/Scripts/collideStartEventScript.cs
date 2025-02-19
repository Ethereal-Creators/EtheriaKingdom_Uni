using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideStartEventScript : MonoBehaviour
{

    public GameObject ProtectionObject;

    public float timeTilEventStop; // Time until scene change
    private float timeWhenEventStop;

    private bool eventIsActivated = false;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (/*coll.gameObject.tag == "ennemie" && coll.gameObject.tag != "projectile" &&*/ coll.gameObject.tag == "joueur")
        {
            Debug.Log("Collided with event");
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ProtectionObject.gameObject.SetActive(true);
            eventIsActivated = true;
        }

    }

    public void Update()
    {
        if (eventIsActivated == true)
        {
            timeWhenEventStop = Time.time + timeTilEventStop;
            timeTilEventStop -= Time.deltaTime;
            if (timeTilEventStop < 0)
            {
                ProtectionObject.gameObject.SetActive(false);
                eventIsActivated = false;
            }

        }

    }
}
