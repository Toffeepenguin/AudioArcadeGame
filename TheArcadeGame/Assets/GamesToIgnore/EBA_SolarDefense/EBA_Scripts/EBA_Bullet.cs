using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBA_Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 12f;

     Rigidbody2D Erb;

    float topBoundary = 12f;

    void Awake()
    {
        Erb = GetComponent<Rigidbody2D>();
       
    }

    void Start()
    {
        Erb.linearVelocity = transform.up * bulletSpeed;
    }


    void Update()
    {
        if (transform.position.y > topBoundary)
        {
            Destroy(gameObject);
           // Debug.Log("Bullet was destroyed");
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "EBA_Enemy(Clone)")
        {
            Destroy(gameObject);
            EBA_GameManager.score += 5;
        }
    }
}
