using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHA_Puck : MonoBehaviour
{
    // Public variables to set in the inspector
    public float maxSpeed = 1000f;       // The maximum speed the puck can reach
    public float friction = 0.8f;     // Friction factor to slow down the puck
    float lastWallHit = 0;
    private Rigidbody2D rb;

    public IHA_ScoreManager scoreManager;

    public float minXBound = -24;
    public float maxXBound = 37;
    public float minYBound = -8;
    public float maxYBound = 19;

    public GameObject particleSystem;

    public AudioClip hitSound;
    public AudioClip goalSound;
    private AudioSource audio;

    public GameObject sticker;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Ensure puck stays in 2D plane (no gravity)
        scoreManager = GameObject.FindAnyObjectByType<IHA_ScoreManager>();
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Limit the puck's speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        // Apply friction
        //rb.velocity *= friction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - lastWallHit > 0.01)
        {
            // Detect collision with paddles or walls
            if (collision.gameObject.name.Contains("Wall"))
            {
                Debug.Log("Wall hit");
                rb.AddForce(1000 * rb.linearVelocity.normalized * -1);
                lastWallHit = Time.time;
                rb.linearVelocity *= friction;

                // Basic reflection behavior on walls
                //ReflectPuck(rb.velocity * -1);
            }
        }
        audio.clip = hitSound;
        audio.PlayOneShot(hitSound);
        Debug.Log("Hit");
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("EnemyGoal"))
        {
            Debug.Log("Player scores goal");
            scoreManager.incrementPlayerScore();
            GameObject.FindAnyObjectByType<IHA_PuckSpawner>().alivePucks--;
            Instantiate(particleSystem, gameObject.transform);

            Instantiate(sticker, new Vector3(-30, 20, -4), Quaternion.identity);

            Destroy(gameObject);
            
            
        }
        if (collision.gameObject.name.Contains("PlayerGoal"))
        {
            Debug.Log("Enemy scores goal");
            scoreManager.incrementEnemyScore();
            GameObject.FindAnyObjectByType<IHA_PuckSpawner>().alivePucks--;
            Instantiate(particleSystem, gameObject.transform);

            Instantiate(sticker, new Vector3(41, 20, -4), Quaternion.identity);
            Destroy(gameObject);
            
        }
        //play goal sound
        audio.clip = goalSound;
        audio.PlayOneShot(goalSound);
        
    }

    private void ReflectPuck(Vector2 normal)
    {
        // Reflect the puck's velocity based on the collision normal
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, normal);
    }

    private void KeepInBounds()
    {
        if (gameObject.transform.position.x >= maxXBound)
        {
            gameObject.transform.position = new Vector3(maxXBound, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x <= minXBound)
        {
            gameObject.transform.position = new Vector3(minXBound, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.y >= maxYBound)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, maxYBound, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.y <= minYBound)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, minYBound, gameObject.transform.position.z);
        }
    }
}
