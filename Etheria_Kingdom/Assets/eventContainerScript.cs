using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventContainerScript : MonoBehaviour
{
    public int maxAmountOfHit;
    public int totalOfHit;

    public void actionOnCollsion()
    {
        Debug.Log("Colided with 'BoiteEvenement'");
        totalOfHit++;

        if (maxAmountOfHit <= totalOfHit)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
