using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KCY_ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    public GameObject Enemy;
    public GameObject obstacleInst;
    public GameObject enemyInst;
    float timer = 0.0f;
    float timer2 = 0.0f;
    public float time;
    public bool canCount = true;

    void MyTimer(float stop, GameObject inst, GameObject obj)
    {

        if (timer < stop)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(inst);
            inst = Instantiate(obj, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            timer = 0.0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        obstacleInst = Instantiate(Obstacle, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        enemyInst = Instantiate(Enemy, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;

        if (timer < 5.0f && canCount)
        {
            timer += time;
        }
        else
        {
            
            Destroy(obstacleInst);
            obstacleInst = Instantiate(Obstacle, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            timer = 0.0f;
        }

        if (timer2 < 25.0f && canCount)
        {
            timer2 += time;
        }
        else
        {
            Destroy(enemyInst);
            enemyInst = Instantiate(Enemy, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            timer2 = 0.0f;
        }

        //MyTimer(2.5f, obstacleInst, Obstacle);

        //obstacleInst.transform.position += new Vector3(-0.005f, 0);
    }
}
