using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HLL_Stayinyourhalfplayer : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.5f, -0.5f), //clamps boundaries on the x axis at the values set, so player can only move between these two positions controlled by a vector.
            Mathf.Clamp(transform.position.y, -4.22f, 4.22f), transform.position.z); // Clamps boundaries on the y axis at the values set, so player can only move between two positions controlled by a vector.
    }
}
