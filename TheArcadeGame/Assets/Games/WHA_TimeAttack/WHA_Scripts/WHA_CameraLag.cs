using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHA_CameraLag : MonoBehaviour
{
    public Transform target; // Assign the car's transform
    public float rotationLag = 0.5f; // Lag factor

    private void LateUpdate()
    {
        if (target)
        {
            // Smoothly interpolate rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime / rotationLag);
        }
    }
}