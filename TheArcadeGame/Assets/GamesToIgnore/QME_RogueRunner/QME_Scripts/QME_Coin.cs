using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QME_Coin : MonoBehaviour
{
    // Reference to the UI Text element that displays the score
    public Text scoreText;

    // Variable to keep track of the player's score
    private int score = 0;

    // AudioClip to be played when an interactable object is collected
    public AudioClip pickupSound;

    // Method called when another object enters the collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the tag "Interactable"
        if (collision.CompareTag("Interactable"))
        {
            // Play the pickup sound using the sound manager
            QME_SoundManager.instance.PlaySound(pickupSound);

            // Increase the score by 1
            score += 1;

            // Update the score display on the UI
            UpdateScoreUI();

            // Destroy the interactable object after collection
            Destroy(collision.gameObject);
        }
    }

    // Method to update the score text on the UI
    private void UpdateScoreUI()
    {
        // Update the text to display the current score
        scoreText.text = "Score: " + score;
    }
}
