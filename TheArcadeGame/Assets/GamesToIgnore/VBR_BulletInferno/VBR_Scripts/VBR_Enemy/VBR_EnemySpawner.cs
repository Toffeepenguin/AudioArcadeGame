using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBR_EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minimumSpawnTime;
    [SerializeField] private float maximumSpawnTime;
    [SerializeField] private float spawnTimeChange = 30f;

    [Header("Spawn-in particle effects parameters")]
    [SerializeField] private GameObject spawnEffectDP;
    [SerializeField] private GameObject spawnEffectG;
    [SerializeField] private GameObject spawnEffectP;
    [SerializeField] private GameObject spawnEffectR;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip spawnSound;
    private AudioSource audioSource;

    private float timeUntilSpawn;
    private float timeSinceStart = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        setTimeUntilSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        
        timeUntilSpawn -= Time.deltaTime;
        
        if (timeUntilSpawn <= 0)
        {
            randomEffectChooser();
            audioSource.PlayOneShot(spawnSound, 0.25f);
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            setTimeUntilSpawn();
        }
        spawnRateHandler();
    }

    private void setTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime,maximumSpawnTime);
    }
    private void randomEffectChooser()
    {
        float temp = Random.Range(1, 4);
        switch (temp) 
        {
            case 1:
                Instantiate(spawnEffectDP, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(spawnEffectG, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(spawnEffectP, transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(spawnEffectR, transform.position, Quaternion.identity);
                break;
        }
    }
    private void spawnRateHandler()
    {
        if (timeSinceStart >= spawnTimeChange)
        {
            if (minimumSpawnTime > 1)
            {
                minimumSpawnTime -= 1f;
                maximumSpawnTime -= 1f;
            }
            timeSinceStart = 0f;
        }
    }
}
