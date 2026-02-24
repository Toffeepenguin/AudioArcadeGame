using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWA_GameObjManager : MonoBehaviour
{
    public Transform spawnPoint; // point of spawn
    public GameObject bossObj;

    public bool isBossSpawned = false;


    private void Awake()
    {
        isBossSpawned = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        bossObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfSpawn();
    }

    private void CheckIfSpawn()
    {
        if (SWA_Player.scores >= 50 && !isBossSpawned)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        bossObj.SetActive(true);
        isBossSpawned = true;
        Debug.Log("Boss has spawned");
    }
}
