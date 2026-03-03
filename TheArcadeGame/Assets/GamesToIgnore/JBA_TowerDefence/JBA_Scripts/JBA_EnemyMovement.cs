using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBA_EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D JBA_rb; //Assigning the rigidbody component to a variable

    [Header("Attributes")]
    [SerializeField] private float JBA_moveSpeed = 2f; //Defining the move speed of the enemy

    private Transform JBA_target; //The target 'point' that the enemy will go towards
    private int JBA_pathIndex = 0;  //Which point it is currently moving towards
    public GameObject JBA_gameOverScreen;

    private void Start()
    {
        JBA_target = JBA_LevelHandler.JBA_main.JBA_path[JBA_pathIndex]; //Assigns the first target to the index
        JBA_LevelHandler.JBA_GameOver = false;
    }

    private void Update()
    {
        if (JBA_LevelHandler.JBA_main.JBA_PlayerHealth == 0)
        {
            JBA_LevelHandler.JBA_GameOver = true;
            Destroy(gameObject);
        }
        if (Vector2.Distance(JBA_target.position, transform.position) <= 0.1f) //Checks if it has reached the target each time update is called
        {
            JBA_pathIndex++;    //If so it increases the indec so it can reassign the target

            if (JBA_pathIndex == JBA_LevelHandler.JBA_main.JBA_path.Length) //If it has reached the last point in the array
            {
                JBA_LevelSpawner.JBA_onEnemyDestroy.Invoke();
                Destroy(gameObject);    //Destroys the object if true as it has reached the end
                JBA_LevelHandler.JBA_main.JBA_PlayerHealth--;
                Debug.Log(JBA_LevelHandler.JBA_main.JBA_PlayerHealth);
                return;
            } else
            {
                JBA_target = JBA_LevelHandler.JBA_main.JBA_path[JBA_pathIndex]; //Updates the target
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (JBA_target.position - transform.position).normalized; //Moves the enemy towards the target

        JBA_rb.linearVelocity = direction * JBA_moveSpeed; //Assigns the speed of the enemy 
    }
}
