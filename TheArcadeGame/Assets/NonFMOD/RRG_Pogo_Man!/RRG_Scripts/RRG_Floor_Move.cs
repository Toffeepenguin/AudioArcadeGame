using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRG_Floor_Move : MonoBehaviour
{
    public RRG_Jumping jumping;

    public Transform playerReference;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-jumping.movementSpeed, 0, 0) * Time.deltaTime;
    }
}
