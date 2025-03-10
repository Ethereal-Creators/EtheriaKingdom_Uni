using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class eventContainerScript : MonoBehaviour
{

    public int maxAmountOfHit;
    public int totalOfHit;

    private float timeDown = 0.0f;

    public float frequecyOfCheckup;

    public GameObject sprite;

    public GameObject InfoCanvas;

    private bool isInfoCanvasActive = false;
    [SerializeField] UnityEvent actionOnDesepear;

    private bool isAnimationActive = false;

    [Header("------- Audio -------")]

    [SerializeField]
    private AudioSource source;

    public List<AudioClip> clips = new List<AudioClip>();

    public void Update()
    {

        timeDown += Time.deltaTime;
        if (timeDown >= frequecyOfCheckup)
        {
            timeDown = 0.0f;
            if (isAnimationActive == true)
            {
                sprite.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                isAnimationActive = false;
                Debug.Log("Red");
            }
            else if (isAnimationActive == false)
            {
                sprite.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                //Debug.Log("White");
            }

            if (isInfoCanvasActive == true)
            {
                isInfoCanvasActive = false;
                InfoCanvas.SetActive(true);
            }
            else if (isInfoCanvasActive == false)
            {
                InfoCanvas.SetActive(false);
            }
        }
    }

    public void actionOnCollsion()
    {
        Debug.Log("Colided with 'BoiteEvenement'");
        totalOfHit++;
        isAnimationActive = true;

        isInfoCanvasActive = true;

        if (maxAmountOfHit <= totalOfHit)
        {
            if (source != null && clips.Count > 0)
            {
                int randomClipIndex = Random.Range(0, clips.Count);
                source.PlayOneShot(clips[randomClipIndex]);
            }
            actionOnDesepear.Invoke();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (InfoCanvas != null)
        {
            if (InfoCanvas.gameObject.activeSelf == false)
            {
                InfoCanvas.SetActive(true);
            }
        }
    }

    public void OnCollisionExit2D(Collision2D coll)
    {
        if (InfoCanvas != null)
        {
            if (InfoCanvas.gameObject.activeSelf == true)
            {
                InfoCanvas.SetActive(false);
            }
        }
    }

    /*
    public void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("colideed with contianer");
        if (coll.gameObject.tag == "projectile")
        {
            Debug.Log("colideed with contianer");
            totalOfHit++;

            if (maxAmountOfHit <= totalOfHit)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }*/
}
