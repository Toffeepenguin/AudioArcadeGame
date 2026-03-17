using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class JHO_PlayerController : MonoBehaviour
{
    [SerializeField] InputSubscription JHO_input;

    Rigidbody2D JHO_rb;

    public Animator JHO_anima;

    Vector2 JHO_PlayerMovement; 

    float JHO_speed = 10f;

    bool facingRace = true;

    private void Awake()
    {
        JHO_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        JHO_anima.SetFloat("JHO_Speed", JHO_PlayerMovement.magnitude);

        JHO_PlayerMovement = new Vector2(JHO_input.NormalizedMovementInput.x, JHO_input.NormalizedMovementInput.y);
        JHO_rb.linearVelocity = new Vector2(JHO_PlayerMovement.x, 0) * JHO_speed;

        if (JHO_PlayerMovement.x < 0 && !facingRace) // Moving right
        {
            flip();
        }
        else if (JHO_PlayerMovement.x > 0 && facingRace) // Moving left
        {
            flip();
        }
    }

    void flip() // a new method that will flip the character
    {
        facingRace = !facingRace; // make facing race is equal to not facing race
        transform.Rotate(0, 180, 0); // rotates the character in the y axis by 180 degrees
    }
}
