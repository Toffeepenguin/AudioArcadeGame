using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWA_EnemySpown : MonoBehaviour
{
    public SWA_Enemy EnemyPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public int spawnAmout = 1;
    public float spawnDistance = 15f; 
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {   
        // dealing with the spawn point which for enemies.
        for (int i = 0; i < this.spawnAmout; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            SWA_Enemy enemy = Instantiate(this.EnemyPrefab, spawnPoint, rotation);
            enemy.size = Random.Range(enemy.minSize, enemy.maxSize);
            enemy.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
