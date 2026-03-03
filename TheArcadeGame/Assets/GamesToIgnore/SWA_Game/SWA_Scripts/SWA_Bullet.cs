using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SWA_Bullet : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxLifeTime = 1.0f;
    

    private Vector2 dir;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.linearVelocity = transform.up * speed;
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.layer == 6)
        {
            SWA_Player.scores++;
            Destroy(this.gameObject);
        }

       if (collision.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
    }
}