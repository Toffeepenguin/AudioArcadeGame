using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HLL_StayinyourhalfAI : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0.5f, 8.19f),
            Mathf.Clamp(transform.position.y, -4.22f, 4.22f), transform.position.z);
        
    }
}
