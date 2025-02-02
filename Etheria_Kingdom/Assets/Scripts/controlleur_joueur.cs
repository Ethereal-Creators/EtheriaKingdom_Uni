using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleurJoueur : MonoBehaviour
{
    public float vitesse = 5f;
    public Rigidbody2D rb;
    public Weapon viseur;

    Vector2 direction;
    Vector2 positionSouris;

    // Mise à jour appelée une fois par frame
    void Update()
    {
        float deplacementX = Input.GetAxisRaw("Horizontal");
        float deplacementY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            viseur.Tire();
        }

        direction = new Vector2(deplacementX, deplacementY).normalized;
        positionSouris = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction.x * vitesse, direction.y * vitesse);

        Vector2 directionVise = positionSouris - rb.position;
        float angleVisée = Mathf.Atan2(directionVise.y, directionVise.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angleVisée;
    }
}
