using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class THU_PowerUpSpawner : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public GameObject mysteryBoxPrefab;
    public Sprite mysteryBoxImage;
    public Sprite[] powerUpImages;
    public Image powerUpUIImage;
    public float spawnInterval = 5f;
    public float spawnRangeX = 0f;
    public float spawnRangeY = 0f;

    private void Start()
    {
        StartCoroutine(SpawnPowerUpsPeriodically());
    }

    private IEnumerator SpawnPowerUpsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnMysteryBox();
        }
    }

    private void SpawnMysteryBox()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(transform.position.x - spawnRangeX, transform.position.x + spawnRangeX),
            Random.Range(transform.position.y - spawnRangeY, transform.position.y + spawnRangeY)
        );

        Instantiate(mysteryBoxPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Mystery Box spawned!");
    }
}
