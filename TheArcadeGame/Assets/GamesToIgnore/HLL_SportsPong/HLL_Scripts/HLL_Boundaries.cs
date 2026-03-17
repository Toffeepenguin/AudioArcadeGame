using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

public class HLL_Boundaries : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -400f, 350f),
            Mathf.Clamp(transform.position.y, -362f, 362f), transform.position.z);
    }
}
