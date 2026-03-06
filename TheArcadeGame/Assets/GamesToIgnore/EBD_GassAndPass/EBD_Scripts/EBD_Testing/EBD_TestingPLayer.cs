using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_TestingPLayer : MonoBehaviour
{
    private float moveInput, steerInput;
    private Rigidbody rb;
    private float speed = 50f;

    public GameObject thePlayer;
    public GameObject levelControl;

    private Vector2 movement;
    [SerializeField] private InputSubscription inputSubscription;

    [Header("Game Over Manager Reference")]
    [SerializeField] private EBD_GameOverManager gameOverManager; // Reference to the Game Over Manager

    private bool isGameOver = false; // Track if the game is over

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Find the Game Over Manager in the scene if not assigned in Inspector
        if (gameOverManager == null)
        {
            gameOverManager = FindObjectOfType<EBD_GameOverManager>();
        }
    }

    void Update()
    {
        // If the game is not over, allow player movement
        if (!isGameOver)
        {
            movement = new Vector2(inputSubscription.NormalizedMovementInput.x, inputSubscription.NormalizedMovementInput.y);
            rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.y) * speed;
        }
        else
        {
            // Stop the player if the game is over
            rb.linearVelocity = Vector3.zero; // Stop all movement
        }
    }

    void OnCollisionEnter(Collision collision) // For 3D
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reset the player's position or stop movement
            transform.position = new Vector3(0, transform.position.y, 0); // Example reset
            rb.linearVelocity = Vector3.zero; // Stop movement
        }
        if (collision.gameObject.layer == 10)
        {
            TriggerGameOver(); // Trigger the game over sequence
        }
    }

    void OnTriggerEnter(Collider other) // For 3D trigger colliders
    {
        if (other.CompareTag("Bush"))
        {
            // Custom logic for triggers
        }
        if (other.gameObject.layer == 10)
        {
            TriggerGameOver(); // Trigger the game over sequence
        }
    }

    private void TriggerGameOver()
    {
        // Set the game over flag to true
        isGameOver = true;

        // Optionally stop any further movement or reset other aspects
        rb.linearVelocity = Vector3.zero; // Stop Rigidbody movement

        // Trigger the Game Over Manager
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogWarning("Game Over Manager is not assigned or found!");
        }
    }
}
