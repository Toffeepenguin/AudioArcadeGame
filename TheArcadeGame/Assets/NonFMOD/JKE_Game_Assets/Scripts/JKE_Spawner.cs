using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class JKE_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    private float obstacleSpawnTime = 1.1f;
    private float obstacleSpeed = 8f;
    private float timeUntilObstacleSpawn;
    public float obsSpawnModi = 1.1f;


    private void Update()
    {
        if (JKE_GameManager.Instance.isPlaying)
        {
            SpawnLoop();
        }
    }



    private void SpawnLoop()
    {   
        timeUntilObstacleSpawn += Time.deltaTime;
        if (timeUntilObstacleSpawn >= obsSpawnModi)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
            if (obstacleSpawnTime > 0.62f)
            {
                obstacleSpawnTime -= Random.Range(0.0001f, 0.015f);

            }
            else if (obstacleSpawnTime > 0.48f)
            {
                obstacleSpawnTime -= Random.Range(0.0001f, 0.006f);
            }
            else
            {
                obstacleSpawnTime -= 0.0001f;
            }
            if (obstacleSpeed < 20f)
            {
                obstacleSpeed += Random.Range(0, 0.22f);
            }
            else if (obstacleSpeed < 24f)
            {
                obstacleSpeed += Random.Range(0, 0.05f);
            }
            else
            {
                obstacleSpeed += 0.012f;
            }
            obsSpawnModi = obstacleSpawnTime + Random.Range(-(obstacleSpawnTime/12), obstacleSpawnTime/11);
            if (obsSpawnModi <= 0.05f){
                obsSpawnModi = 0.05f;
            }
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.linearVelocity = Vector2.left * obstacleSpeed;
    }
}
