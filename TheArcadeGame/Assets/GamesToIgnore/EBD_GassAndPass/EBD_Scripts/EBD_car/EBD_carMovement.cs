using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 10f; // Speed at which the car moves forward
    public float offScreenZ = -50f; // Z position at which to destroy the car

    // Update is called once per frame
    void Update()
    {
        // Move the car along the Z-axis
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Check if the car has moved off-screen and destroy it
        if (transform.position.z < offScreenZ)
        {
            Destroy(gameObject); // Destroy the car once it goes off-screen
        }
    }
}
