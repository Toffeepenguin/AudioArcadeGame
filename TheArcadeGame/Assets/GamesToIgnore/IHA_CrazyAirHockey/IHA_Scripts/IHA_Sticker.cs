using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHA_Sticker : MonoBehaviour
{
    private float creationTime;
    public float lifetime = 0.5f;

    private void Awake()
    {
        creationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - creationTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
