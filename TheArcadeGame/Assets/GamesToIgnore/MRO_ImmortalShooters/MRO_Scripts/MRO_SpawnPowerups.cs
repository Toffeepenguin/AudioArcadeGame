using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;
    public float spawnInterval = 5f; 
    public float xRange = 8f; 

    void Start()
    {
        InvokeRepeating(nameof(SpawnPowerup), 2f, spawnInterval);
    }

    void SpawnPowerup()
    {
        Camera mainCamera = Camera.main;
        float screenLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenTop = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float randomX = Random.Range(screenLeft + 1f, screenRight - 1f); 
        Vector3 spawnPosition = new Vector3(randomX, screenTop + 1f, 0f); 
        Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
    }
}
