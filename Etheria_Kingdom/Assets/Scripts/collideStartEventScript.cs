using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collideStartEventScript : MonoBehaviour
{

    public GameObject ProtectionObject;

    public float timeTilEventStop; // Time until scene change
    private float timeWhenEventStop;

    private bool eventIsActivated = false;

    [SerializeField] UnityEvent eventStart;
    [SerializeField] UnityEvent eventEnd;


    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (/*coll.gameObject.tag == "ennemie" && coll.gameObject.tag != "projectile" &&*/ coll.gameObject.tag == "joueur")
        {
            Debug.Log("Collided with event");
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //ProtectionObject.gameObject.SetActive(true);
            eventStart.Invoke();
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
                eventEnd.Invoke();
                //ProtectionObject.gameObject.SetActive(false);
                eventIsActivated = false;
            }

        }

    }
}
