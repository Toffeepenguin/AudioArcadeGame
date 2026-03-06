using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 10f; 
    [SerializeField] private int damage = 1; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player"))
        {
           
            MRO_playerHealth playerHealth = collision.GetComponent<MRO_playerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health -= damage; 


                if (playerHealth.health <= 0)
                {
                    playerHealth.ImmortalShooters_UI.GameOver(playerHealth.GetPowerUpsCollected());
                    Destroy(collision.gameObject); 
                }
            }

            Destroy(gameObject);
        }
    }
}
