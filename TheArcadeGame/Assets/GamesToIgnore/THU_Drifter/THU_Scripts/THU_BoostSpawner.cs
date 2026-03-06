using System.Collections;
using UnityEngine;

public class THU_BoostSpawner : MonoBehaviour
{
    public GameObject boostPickupPrefab;
    public float spawnInterval = 15f;
    public float spawnRange = 0.2f;
    private float spawnTimer;
    public float autoDeleteTime = 10f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnBoostPickup();
            spawnTimer = 0f;
        }
    }

    private void SpawnBoostPickup()
    {
        float randomX = Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange);
        float randomY = Random.Range(transform.position.y - spawnRange, transform.position.y + spawnRange);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        GameObject boostPickup = Instantiate(boostPickupPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(AutoDeleteIfNotPickedUp(boostPickup));
    }

    private IEnumerator AutoDeleteIfNotPickedUp(GameObject boostPickup)
    {
        yield return new WaitForSeconds(autoDeleteTime);

        if (boostPickup != null && boostPickup.activeInHierarchy)
        {
            Destroy(boostPickup);
        }
    }
}
