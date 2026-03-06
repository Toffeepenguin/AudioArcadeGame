using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLI_EnemyBear : MonoBehaviour
{
    float MLI_Timer;
    float MLI_FireAngle;
    Vector2 mli_dir;

    Rigidbody2D MLI_rb;
    MLI_EnemyStats MLI_stats;
    MLI_BulletSpawner MLI_weapon;

    float movementChangeTimer; // Timer for changing direction
    Vector2 currentDirection;  // Current movement direction

    // Start is called before the first frame update
    void Start()
    {
        MLI_stats = GetComponent<MLI_EnemyStats>();
        MLI_weapon = GetComponent<MLI_BulletSpawner>();
        MLI_rb = GetComponent<Rigidbody2D>();

        MLI_FireAngle = 45;

        ChooseNewDirection(); // Choose an initial direction
    }

    // Update is called once per frame
    void Update()
    {
        HandleFiring();
        HandleMovement();
    }

    void HandleFiring()
    {
        // Increment the timer based on the time since the last frame
        MLI_Timer += Time.deltaTime;

        // Check if it's time to fire
        if (MLI_Timer >= MLI_stats.MLI_FIRERATE)
        {
            // Fire bullets in 4 directions
            FireBullets();

            // Reset the timer
            MLI_Timer = 0;
        }
    }

    void FireBullets()
    {
        // Fire in the current angle and at 90-degree increments
        for (int i = 0; i < 4; i++)
        {
            mli_dir = MLI_weapon.AngleToVector2(MLI_FireAngle + (i * 90));
            MLI_weapon.SpawnBullet(false, MLI_stats.MLI_BULLET_SPEED, MLI_stats.MLI_BULLET_SIZE, MLI_stats.MLI_BULLET_DAMAGE, mli_dir, Color.red);
        }

        // Increment the firing angle for the next set of bullets
        MLI_FireAngle += 5;
    }

    void HandleMovement()
    {
        // Decrease the timer for direction change
        movementChangeTimer -= Time.deltaTime;

        // If it's time to change direction
        if (movementChangeTimer <= 0)
        {
            ChooseNewDirection();
        }

        // Apply movement in the current direction
        MLI_rb.linearVelocity = currentDirection * MLI_stats.MLI_SPEED;
    }

    void ChooseNewDirection()
    {
        // Randomly choose a direction: left, right, or down
        int randomChoice = Random.Range(0, 3); // 0, 1, or 2

        switch (randomChoice)
        {
            case 0:
                currentDirection = Vector2.left; // Move left
                break;
            case 1:
                currentDirection = Vector2.right; // Move right
                break;
            case 2:
                currentDirection = Vector2.down; // Move down
                break;
        }

        // Set a new timer for direction change
        movementChangeTimer = Random.Range(1f, 3f); // Change direction every 1-3 seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.name == "MLI_Enemy_Bounds")
        {
            Destroy(this.gameObject);
        }
    }
}
