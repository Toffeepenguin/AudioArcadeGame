using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHA_PuckSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 1;
    private float lastSpawnTime = 0;

    public int alivePucks;
    public int maxPucks;
    public GameObject puck;
    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alivePucks < maxPucks && Time.time - lastSpawnTime > spawnDelay)
        {
            GameObject newPuck = Instantiate(puck, gameObject.transform);
            Vector2 initialDirection = new Vector2(Random.RandomRange(-1f, 1f), Random.RandomRange(-1f, 1f));
            newPuck.GetComponent<Rigidbody2D>().linearVelocity = initialDirection * speed;
            lastSpawnTime = Time.time;
        }
    }
}
