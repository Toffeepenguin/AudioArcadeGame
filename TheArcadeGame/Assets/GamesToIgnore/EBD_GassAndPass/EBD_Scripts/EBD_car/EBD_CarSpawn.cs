using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EBD_CarSpawner : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField] private Transform player;

    [Header("Car Settings")]
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private float[] lanes = { -3f, 0f, 3f }; // Lane positions
    [SerializeField] private float minSpawnDistance = 10f;
    [SerializeField] private float maxSpawnDistance = 30f;
    [SerializeField] private float spawnDistanceGrowthRate = 0.1f;

    [Header("Difficulty Settings")]
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnIntervalReductionRate = 0.05f;
    [SerializeField] private float minSpawnInterval = 0.5f;
    [SerializeField] private float carBaseSpeed = 5f;
    [SerializeField] private float carSpeedGrowthRate = 0.1f;

    private float elapsedTime = 0f;
    private float currentSpawnDistance;

    void Start()
    {
        currentSpawnDistance = minSpawnDistance;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnInterval)
        {
            SpawnChallengingCar();
            elapsedTime = 0f;

            // Gradually reduce spawn interval for difficulty
            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalReductionRate, minSpawnInterval);
        }
    }

    private void UpdateSpawnDistance()
    {
        currentSpawnDistance = Mathf.Clamp(
            minSpawnDistance + (player.position.z * spawnDistanceGrowthRate),
            minSpawnDistance,
            maxSpawnDistance
        );
    }

    private void SpawnChallengingCar()
    {
        UpdateSpawnDistance();

        // Select a random lane
        int randomLane = Random.Range(0, lanes.Length);
        float lanePositionX = lanes[randomLane];

        // Calculate spawn position based on player's position and dynamic spawn range
        Vector3 spawnPosition = new Vector3(lanePositionX, 0, player.position.z + currentSpawnDistance);

        // Instantiate car
        GameObject car = Instantiate(carPrefab, spawnPosition, Quaternion.identity);

        // Set car speed based on player's progress
        float speed = carBaseSpeed + (player.position.z * carSpeedGrowthRate);
        car.GetComponent<EBD_CarController>().SetSpeed(speed);
    }
}
