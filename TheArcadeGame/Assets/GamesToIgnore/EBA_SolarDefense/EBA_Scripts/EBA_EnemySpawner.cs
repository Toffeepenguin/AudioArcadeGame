using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EBA_EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnSpeed;
  
    [SerializeField] private GameObject[] enemy;

    [SerializeField] private Renderer Erb;


    
    void Start()
    {
        Erb = GetComponent<Renderer>();
        StartCoroutine(spawner());
        
    }
    
    IEnumerator spawner ()
    {
        spawnSpeed = Random.Range(3f, 6f);
        WaitForSeconds wait = new WaitForSeconds(spawnSpeed);

        while (true) 
        {
            yield return wait;
            int rand = Random.Range(0, enemy.Length);
            GameObject tospawn = enemy[rand];

            Instantiate(tospawn, transform.position, Quaternion.identity);

        }
    }

 


}
