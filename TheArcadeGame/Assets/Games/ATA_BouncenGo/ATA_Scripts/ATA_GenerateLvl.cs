using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATA_GenerateLevel : MonoBehaviour
{
    public GameObject[] section;          // Array of different section prefabs
    public float zPos = 50f;              // Starting position for the first section
    public bool sectionCreation = false;  // Flag to control section generation
    public int secNum;                    // Random section index
    public GameObject player;             // Reference to the player
    public List<GameObject> spawnedSections = new List<GameObject>();  // List to keep track of spawned sections
    public float destroyDistance = 50f;   // Distance behind the player where sections should be destroyed

    private float playerSpeed;            // Player's speed (on the Z axis)

    public float minSpawnRate = 1f;       // Minimum spawn rate (in seconds) 
    public float maxSpawnRate = 2f;       // Maximum spawn rate (in seconds)

    void Update()
    {
        // Get the player's forward speed (velocity on the Z-axis)
        playerSpeed = player.GetComponent<Rigidbody>().linearVelocity.z;

        // Spawn sections based on player's speed, but we now introduce a reduced spawn rate
        if (!sectionCreation)
        {
            sectionCreation = true;
            StartCoroutine(GenerateSection());
        }

        // Destroy sections that are behind the player
        DestroyPassedSections();
    }

    IEnumerator GenerateSection()
    {
        // Choose a random section from the array
        secNum = Random.Range(0, section.Length);
        GameObject newSection = Instantiate(section[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50f;  // Increase the Z position for the next section

        // Add the new section to the list of spawned sections
        spawnedSections.Add(newSection);

        // Adjust the spawn rate based on player's speed, but ensure it's within a minimum and maximum range
        float spawnRate = Mathf.Clamp(1f / Mathf.Abs(playerSpeed), minSpawnRate, maxSpawnRate);

        // Wait for a short duration before generating the next section
        yield return new WaitForSeconds(spawnRate); // Adjust spawn rate based on the player's speed, with limits
        sectionCreation = false;
    }

    void DestroyPassedSections()
    {
        // Loop through all the spawned sections to check if they are behind the player
        for (int i = 0; i < spawnedSections.Count; i++)
        {
            GameObject section = spawnedSections[i];

            // Only remove sections that are instantiated (clones)
            if (section != null && section.name.Contains("(Clone)"))
            {
                // If the section has passed the player by more than destroyDistance
                if (section.transform.position.z < player.transform.position.z - destroyDistance)
                {
                    Destroy(section);  // Destroy the section
                    spawnedSections.RemoveAt(i);  // Remove from the list
                    i--;  // Adjust the index since we removed an element
                }
            }
        }
    }
}
