using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QME_Trap : MonoBehaviour
{
    // Public variable for damage dealt to the player
    public float damage = 20f;

    // Audio clip to play when damage is dealt
    public AudioClip damageSound;

    // Triggered when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Play the damage sound using the sound manager
        QME_SoundManager.instance.PlaySound(damageSound);

        // Try to get the QME_Health component from the colliding object
        QME_Health playerHealth = collision.GetComponent<QME_Health>();
        if (playerHealth != null) // Check if the player has a health component
        {
            // Apply damage to the player's health
            playerHealth.TakeDamage(damage);

            // Check if the player's health is depleted (healthFill indicates health level)
            if (playerHealth.healthFill.fillAmount <= 0)
            {
                // Find the 'You Lose' screen manager in the scene
                QME_YouLose loseScreen = FindObjectOfType<QME_YouLose>();
                if (loseScreen != null) // If the lose screen manager exists
                {
                    // Show the 'You Lose' screen
                    loseScreen.ShowLoseScreen();
                }
            }
        }
    }
}
