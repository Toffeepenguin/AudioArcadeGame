using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRO_MiniEnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Bullet Properties")]
    [SerializeField] private float force = 10f;  
    [SerializeField] private float lifetime = 5f; 

    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<MRO_playerHealth>().health -= 2;
            Destroy(gameObject);
        }
    }
}
