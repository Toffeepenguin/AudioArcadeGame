using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_RandomSpawn : MonoBehaviour
{
    public GameObject carPrefab;
    public ATA_GenerateLevel generateLevel;
    public float spawnInterval = 1f;
    public float nextSpawntime;


    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawntime)
        {
            nextSpawntime = Time.time + spawnInterval;
            float xPos = Random.Range(-8, 8); // Random x-position within range
            float zPos = generateLevel.zPos + Random.Range(-50f, 50f); // Random z-position based on level position

            // Cast a ray down from a high y-position to find the exact ground level
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(xPos, 0, zPos), Vector3.down, out hit))
            {
                // Use hit.point.y to get the ground level, adding a slight offset to prevent clipping
                Vector3 spawnPosition = new Vector3(xPos, hit.point.y + 0.5f, zPos); // Adjust 0.5f based on car height
                GameObject newCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);

                // Reset the Rigidbody's velocity to avoid unintended forces
                Rigidbody rb = newCar.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
            // float xPos = Random.Range(-8, 3);

            //   float zPos = generateLevel.zPos + Random.Range(-50f, 50f);

            // float groundYPos = 0; // Set this based on your ground level
            //Vector3 randomSpawnPosition = new Vector3(xPos, groundYPos, zPos);
            // Instantiate(carPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
