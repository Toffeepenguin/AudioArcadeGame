using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_CarController : MonoBehaviour
{

    private float speed;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        // Move car forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}


