using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHA_SnowFollow : MonoBehaviour
{
    public Transform target; // The object to follow
    public float yOffset; // Offset on the Y axis

    void Update()
    {
        if (target != null)
        {
            // Follow the target with the Y offset
            transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);
        }
    }
}
