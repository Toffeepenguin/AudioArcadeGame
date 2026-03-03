using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLI_HealthDisplay : MonoBehaviour
{
    public GameObject healthIconPrefab; // Prefab of the health icon
    public Transform healthIconParent; // Parent object for the health icons

    private MLI_PlayerStats playerStats; // Reference to the player's stats
    private List<GameObject> healthIcons = new List<GameObject>(); // List to manage health icons

    void Start()
    {
        // Find the player by name and get its stats component
        GameObject player = GameObject.Find("MLI_Player");
        if (player != null)
        {
            playerStats = player.GetComponent<MLI_PlayerStats>();
        }

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on MLI_Player GameObject!");
            return;
        }

        UpdateHealthDisplay();
    }

    void Update()
    {
        // Continuously update health display (can be optimized if needed)
        UpdateHealthDisplay();
    }

    void UpdateHealthDisplay()
    {
        if (playerStats == null) return;

        // Clear the existing health icons
        foreach (GameObject icon in healthIcons)
        {
            Destroy(icon);
        }
        healthIcons.Clear();

        // Set the starting position and spacing
        Vector3 startPosition = Vector3.zero; // Start at (0, 0)
        float spacing = 1.5f; // Horizontal spacing between icons

        // Add icons based on the player's current health
        for (int i = 0; i < playerStats.MLI_PLAYER_HEALTH; i++)
        {
            GameObject newIcon = Instantiate(healthIconPrefab, healthIconParent);
            newIcon.GetComponent<SpriteRenderer>().enabled = true;

            // Adjust the position incrementally
            newIcon.transform.localPosition = startPosition + new Vector3(i * spacing, 0, 0);

            healthIcons.Add(newIcon);
        }
    }
}
