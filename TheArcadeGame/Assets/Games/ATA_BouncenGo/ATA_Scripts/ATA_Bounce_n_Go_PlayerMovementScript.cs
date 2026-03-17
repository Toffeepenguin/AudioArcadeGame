using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ATA_Bounce_n_Go_PlayerMovementScript : MonoBehaviour
{
    public float forwardSpeed = 15f;          // Starting forward movement speed
    public float sideMoveSpeed = 10f;         // Speed for left/right movement
    public float jumpForce = 1f;              // Jump force

    public float groundCheckDistance = 0.1f;  // Distance to check below the player for ground
    public bool isGrounded = false;
    public LayerMask Ground;                  // Layer assigned to ground objects

    [SerializeField] InputSubscription ATA_Input;
    [SerializeField] ATA_LvlBoundary levelBoundary;  // Reference to the boundary script

    private Rigidbody rb;
    private float maxForwardSpeed = 55f;      // Maximum forward speed

    private void Awake()
    {
        ATA_Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents player rotation due to physics
    }

    private void Update()
    {
        // Increase forward speed over time
        forwardSpeed += Time.deltaTime * 0.5f; // Adjust the multiplier to control acceleration

        // Clamp forward speed to the maximum value
        forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxForwardSpeed);  // Clamp to max speed (55)

        // Get the current velocity
        Vector3 velocity = rb.linearVelocity;

        // Constant forward movement (use velocity instead of MovePosition)
        velocity.z = forwardSpeed;  // Set forward movement on the Z-axis

        // Horizontal Movement with A/D or Left/Right Arrow keys
        velocity.x = ATA_Input.NormalizedMovementInput.x * sideMoveSpeed; // Update only horizontal velocity

        rb.linearVelocity = velocity; // Apply the updated velocity to the Rigidbody

        // Clamping the player's position to stay within boundaries
        float clampedX = Mathf.Clamp(transform.position.x, levelBoundary.internalLeft, levelBoundary.internalRight);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Jumping
        if (ATA_Input.SpaceInput && isGrounded)
        {
            // Apply jump force only when grounded
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevents double jumping until grounded
        }

        if (ATA_Input.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Called when the player's collider enters a collision
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;  // Set grounded to true when we touch the ground
        }
    }

    // Called when the player's collider exits a collision
    private void OnCollisionExit(Collision collision)
    {
        // Check if the collision is with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false; // Set grounded to false when we leave the ground
        }
    }
}
