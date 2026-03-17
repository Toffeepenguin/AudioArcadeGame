using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATA_TrophyManager : MonoBehaviour
{
    public static bool hasPlayerWon; // Global flag to track trophy status

    void Start()
    {
        // Initialize the trophy status
        hasPlayerWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerWon)
        {
            // Log or trigger other actions when the player wins
            Debug.Log("Trophy Achieved! Player exceeded 300 distance.");
        }
    }

    // Method to update the win state
    public void playerWinSet(bool temp)
    {
        hasPlayerWon = temp;
    }
}
