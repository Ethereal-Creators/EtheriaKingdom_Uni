using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed;

    /* RigidBody2D rb;

    void Start()
    {
        rb = GetComponent<RigidBody2D>();
    }

    void fixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(x,y) * speed;
    }
    */
}
