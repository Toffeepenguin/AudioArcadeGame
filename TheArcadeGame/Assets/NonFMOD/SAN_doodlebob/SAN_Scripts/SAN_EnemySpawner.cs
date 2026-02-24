using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAN_EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject SAN_Enemy;
    [SerializeField]
    private GameObject SAN_EnemyMirrored;


    [SerializeField]
    private float SAN_EnemyInterval = 3f;
    [SerializeField]
    private float SAN_EnemyIntervalMirrored = 3f;



    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(spawnEnemy(SAN_EnemyInterval, SAN_Enemy));
        StartCoroutine(spawnEnemy1(SAN_EnemyIntervalMirrored, SAN_EnemyMirrored));

    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {

        yield return new WaitForSeconds(Random.Range(2f, 6f));
        GameObject newEnemy = Instantiate(enemy, new Vector3(10f, -2.75f, 0f), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));

    }
    private IEnumerator spawnEnemy1(float interval1, GameObject enemy1)
    {

        yield return new WaitForSeconds(Random.Range(2f, 6f));
        GameObject newEnemy = Instantiate(enemy1, new Vector3(-10f, -2.75f, 0f), Quaternion.identity);
        StartCoroutine(spawnEnemy1(interval1, enemy1));

    }
}
