using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWA_BossBullets : MonoBehaviour
{
    public float speed = 1.0f;
    public float maxLifeTime = 1.0f;
    public bool _playerGetHit = false;
    public SWA_Boss boss;

    private Vector2 dir;
    private Rigidbody2D _rigidbody;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerGetHit = false;
    }

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            boss.currentHp++;
            Destroy(gameObject);
        }
    }
}
