using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SAN_EnemyControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Movespeed;

    public bool OnTheRight;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnTheRight)
        {
            rb.linearVelocity = new Vector2(Movespeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-Movespeed, 0);
        }
    }
}

   
