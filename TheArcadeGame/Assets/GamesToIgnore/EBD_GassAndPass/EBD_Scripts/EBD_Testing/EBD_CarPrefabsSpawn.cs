using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_CarPrefabsSpawn : MonoBehaviour
{
        public Transform carPrefab; // Reference to the car prefab
        public Transform carSpawnPoint; // Reference to the empty GameObject in front of the player
        public float spawnInterval = 2.0f; // Time between spawns
        public int numLanes = 3; // Number of lanes
        public float laneWidth = 2.0f; // Width of each lane

        private float timer;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnCar();
                timer = 0;
            }
        }

        void SpawnCar()
        {
            // Randomly select a lane
            float laneOffset = Random.Range(-numLanes / 2, numLanes / 2) * laneWidth;

            // Calculate spawn position
            Vector3 spawnPosition = carSpawnPoint.position + carSpawnPoint.right * laneOffset;

            // Spawn the car
            Transform car = Instantiate(carPrefab, spawnPosition, carSpawnPoint.rotation);

            // Align the car to the road (optional if the spawnPoint is already aligned)
            car.rotation = Quaternion.LookRotation(carSpawnPoint.forward);
        }
 }


