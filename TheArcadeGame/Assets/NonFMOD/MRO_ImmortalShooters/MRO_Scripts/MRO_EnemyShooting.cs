using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MRO_EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    public float firerate = 1f;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    

    
    void Update()
    {
        if (player != null)
        {
            timer -= Time.deltaTime;

            float distance = Vector2.Distance(transform.position, player.transform.position);
            
                if (timer <= 0)
                {
                    timer = firerate;
                    shoot();
                }
            
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
