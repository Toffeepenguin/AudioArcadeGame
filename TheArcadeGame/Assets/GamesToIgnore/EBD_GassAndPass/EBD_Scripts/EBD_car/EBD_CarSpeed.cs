using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_CarSpeed : MonoBehaviour
{
    //script on every car
    float thisCarSpeed;
    float defaultSpeed;
    float randomValue;
    void Start()
    {
        thisCarSpeed = Random.Range(defaultSpeed - randomValue, defaultSpeed + randomValue);
    }

    void Update()
    {
        transform.position += Vector3.forward * thisCarSpeed;
    }
}
