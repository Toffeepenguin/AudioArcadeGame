using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBD_DistanceCovered : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Text distanceText; // UI Text element to display the distance (optional)
    public GameObject[] stars; // UI elements for stars (assign in the inspector)
    private Vector3 startPosition; // Starting position of the player
    private float distanceCovered; // Total distance covered
    private int starCount = 0; // Tracks the current star rating

    void Start()
    {
        // Set the starting position to the player's initial position
        if (player == null)
        {
            Debug.LogError("Player Transform not assigned!");
            return;
        }

        startPosition = player.position;
        distanceCovered = 0f;

        // Ensure stars are inactive at the start
        foreach (var star in stars)
        {
            if (star != null) star.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculate the distance covered in the Z direction (or adjust for your game axis)
        float currentZ = player.position.z;
        float startZ = startPosition.z;

        distanceCovered = currentZ - startZ;

        // Optional: Update the UI element
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + distanceCovered.ToString("F2") + " meters";
        }

        // Update star rating based on distance
        UpdateStars();
    }

    void UpdateStars()
    {
        if (distanceCovered >= 1000 && starCount < 3)
        {
            SetStars(3);

            if (PlayerPrefs.GetInt("EBD_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("EBD_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }

        if (distanceCovered >= 750 && starCount < 2)
        {
            SetStars(2);
        }

        if (distanceCovered >= 400 && starCount < 1)
        {
            SetStars(1);
        }
    }

    void SetStars(int count)
    {
        starCount = count;

        // Enable stars based on the current rating
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] != null) stars[i].SetActive(i < starCount);
        }
    }

    public float GetDistanceCovered()
    {
        return distanceCovered;
    }

    public int GetStarCount()
    {
        return starCount;
    }
}
