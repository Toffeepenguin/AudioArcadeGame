using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QME_Health : MonoBehaviour
{
    // Reference to the UI element (Image) that represents the health bar's fill amount
    public Image healthFill;

    // Maximum health value for the player or entity
    public float maxHealth = 100f;

    // Current health value, private to ensure it's only modified through defined methods
    private float currentHealth;

    // Called when the script instance is being loaded
    void Start()
    {
        // Initialize current health to the maximum health value
        currentHealth = maxHealth;

        // Update the health bar UI to reflect the initial health value
        UpdateHealthBar();
    }

    // Method to apply damage to the entity
    public void TakeDamage(float damage)
    {
        // Subtract the damage amount from the current health
        currentHealth -= damage;

        // Ensure current health remains within valid bounds (0 to maxHealth)
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar UI to reflect the new health value
        UpdateHealthBar();
    }

    // Private method to update the health bar's visual representation
    void UpdateHealthBar()
    {
        // Adjust the health bar fill amount based on the percentage of health remaining
        healthFill.fillAmount = currentHealth / maxHealth;
    }
}
