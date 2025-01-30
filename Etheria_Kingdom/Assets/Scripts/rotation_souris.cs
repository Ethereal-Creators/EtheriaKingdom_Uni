using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour

{

    public float rotationSpeed = 5f; // Speed of rotation

    private Quaternion targetRotation;



    void Start()

    {

        // Initialize the target rotation to the current rotation

        targetRotation = transform.rotation;

    }



    void Update()

    {

        // Check if the left mouse button is held down

        if (Input.GetMouseButton(0)) // 0 is the left mouse button

        {

            // Get the mouse position in world coordinates

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePos.z = 0; // Set z to 0 because it's 2D



            // Calculate the direction from the object to the mouse position

            Vector3 direction = mousePos - transform.position;



            // Calculate the angle

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;



            // Set the target rotation based on the calculated angle

            targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }



        // Smoothly rotate towards the target rotation

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

}


