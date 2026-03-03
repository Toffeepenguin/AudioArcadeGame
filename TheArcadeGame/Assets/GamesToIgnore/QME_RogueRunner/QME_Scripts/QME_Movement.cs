using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class QME_Movement : MonoBehaviour
{
    // Reference to the InputSubscription script for handling input
    InputSubscription _Input;

    // Speed at which the character moves
    public float moveSpeed = 5f;

    // Force applied to the character when jumping
    public float jumpForce = 5f;

    // Rigidbody2D component for physics-based movement
    private Rigidbody2D rb2D;

    // Animator component for handling animations
    public Animator animator;

    // Boolean to check if the character is grounded
    private bool grounded;

    // Reference to the InputSubscription component
    private InputSubscription inputSubscriptions;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component attached to the GameObject
        rb2D = GetComponent<Rigidbody2D>();

        // Get the InputSubscription component attached to the GameObject
        inputSubscriptions = GetComponent<InputSubscription>();

        // Error check for missing InputSubscription script
        if (inputSubscriptions == null)
        {
            Debug.LogError("QME_InputSubscription script is missing!");
        }

        // Error check for missing Animator component
        if (animator == null)
        {
            Debug.LogError("Animator component is missing");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle character movement if inputSubscriptions is present
        if (inputSubscriptions != null)
        {
            MoveCharacter();
        }

        // Handle jumping if the space input is pressed and the character is grounded
        if (inputSubscriptions.SpaceInput && grounded)
        {
            Jump();
        }

        // Update animation parameters
        animator.SetBool("move", inputSubscriptions);
        animator.SetBool("ground", grounded);
    }

    // Method to handle character movement
    private void MoveCharacter()
    {
        // Get the normalized movement input from the InputSubscription
        Vector2 moveInput = inputSubscriptions.NormalizedMovementInput;

        // Calculate movement vector and apply horizontal velocity
        Vector2 movement = moveInput * moveSpeed;
        rb2D.linearVelocity = new Vector2(movement.x, rb2D.linearVelocity.y); // Maintain vertical velocity during movement
    }

    // Method to handle character jumping
    private void Jump()
    {
        // Ensure the character is on the ground before allowing a jump
        if (Mathf.Abs(rb2D.linearVelocity.y) < 0.01f)
        {
            // Apply a vertical impulse to make the character jump
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Set grounded to false and trigger the jump animation
            grounded = false;
            animator.SetTrigger("jump");
        }
    }

    // Detect collisions with other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the character collides with an object tagged "Ground", set grounded to true
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
