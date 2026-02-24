using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBA_MoveSpawner : MonoBehaviour
{

    private float speed;
    private float rightBoundary = 6f;
    private float LeftBoundary = -6f;
    private Rigidbody2D Erb;

    void Start()
    {
        speed = Random.Range(1f, 1.5f);
        Erb = GetComponent<Rigidbody2D>();
       Erb.linearVelocity = new Vector2(speed, 0);
    }

   
    void Update()
    {
        if (transform.position.x > rightBoundary) //move spawner right 
        {
            Erb.linearVelocity = new Vector2(-speed, 0);
        }

        if (transform.position.x < LeftBoundary) //move spawner left
        {
            Erb.linearVelocity = new Vector2(speed, 0);
        }
    }
}
