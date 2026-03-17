using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLI_EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform spawnLocation; // The base spawn location

    [Header("Random Spawn Settings")]
    public Vector2 spawnAreaSize = new Vector2(5f, 5f); // Area around the spawn location
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns

    private float spawnTimer; // Timer for random spawning

    void Start()
    {
        // Initialize the timer with a random interval
        spawnTimer = GetRandomInterval();
    }

    void Update()
    {
        // Decrease the timer based on the elapsed time
        spawnTimer -= Time.deltaTime;

        // If the timer reaches zero, spawn an enemy and reset the timer
        if (spawnTimer <= 0)
        {
            SpawnEnemyInArea();
            spawnTimer = GetRandomInterval();
        }
    }

    /// <summary>
    /// Spawns the enemy prefab at the specified location.
    /// </summary>
    public void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnLocation != null)
        {
            Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab or spawn location is not set!");
        }
    }

    /// <summary>
    /// Spawns the enemy prefab at a random position within the defined spawn area.
    /// </summary>
    public void SpawnEnemyInArea()
    {
        if (enemyPrefab != null && spawnLocation != null)
        {
            // Calculate a random position within the spawn area
            Vector3 randomPosition = spawnLocation.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                0f // Assuming 2D
            );

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Enemy prefab or spawn location is not set!");
        }
    }

    /// <summary>
    /// Returns a random interval between the minimum and maximum spawn interval.
    /// </summary>
    private float GetRandomInterval()
    {
        return Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}