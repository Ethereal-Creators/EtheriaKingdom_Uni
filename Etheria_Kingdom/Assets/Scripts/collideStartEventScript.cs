using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class collideStartEventScript : MonoBehaviour
{

    public GameObject ProtectionObject;

    public GameObject SelectCharachterThatActivate;

    public float timeTilEventStop; // Time until scene change
    private float timeWhenEventStop;

    private bool eventIsActivated = false;

    [Header("------- Audio Effects Start -------")]
    public AudioSource source;
    public List<AudioClip> clipsStart = new List<AudioClip>();

    [Header("------- Audio Effects End -------")]
    public List<AudioClip> clipsEnd = new List<AudioClip>();

    [Header("------- Unity Events -------")]
    [SerializeField] UnityEvent eventStart;
    [SerializeField] UnityEvent eventEnd;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (/*coll.gameObject.tag == "ennemie" && coll.gameObject.tag != "projectile" &&*/ coll.gameObject.tag == "joueur" && coll.gameObject == SelectCharachterThatActivate.gameObject)
        {
            Debug.Log("collide with right charachter");
            Debug.Log("Collided with event");

            //deactivate gameObject visibiliti and interactibility
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //Plays a sound on contact
            if (source != null && clipsStart.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clipsStart.Count);
                source.PlayOneShot(clipsStart[randomClipIndex]);
            }


            //Start event
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
                if (source != null && clipsEnd.Count > 0)
                {
                    int randomClipIndex = Random.Range(0, clipsEnd.Count);
                    source.PlayOneShot(clipsEnd[randomClipIndex]);
                }

                eventEnd.Invoke();
                eventIsActivated = false;
            }

        }

    }
}
