using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CYU_Obstacle : MonoBehaviour
{

    public CYU_GameManager CYU_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly sets the instanced obstacle's scale and rotation
        transform.localScale = Random.Range(1.5f, 3f) * Vector3.one;
        //transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
        //transform.localPosition = new Vector2(18, Random.Range(-9f, 9f));

        // Gets the obstacle's rb, and adds a force with a random speed to move it
        Rigidbody2D CYU_ObstacleRB = GetComponent<Rigidbody2D>();
        Vector2 CYU_ObstacleDir = new Vector2(-1, 0).normalized;
        float CYU_ObstacleSpeed = Random.Range(5f, 11f);
        CYU_ObstacleRB.AddForce(CYU_ObstacleDir * CYU_ObstacleSpeed, ForceMode2D.Impulse);

        // Increment obstacle count so game manager knows there are still obstacles in the scene
        CYU_gameManager.CYU_obstacleCount++;
    }

    void Update()
    {
        // Checks if the obstacle has gone off screen
        if (this.gameObject.transform.position.x < -21f)
        {
            // Destroy object, and decrement obstacle count
            Destroy(this.gameObject);
            CYU_gameManager.CYU_obstacleCount--;
            print("Object destroyed");
        }

        // Updates the rotation of the obstacle
        //transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    // Collision check for player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if instances obstacle has collided with the player
        if (collision.gameObject.name == "Player")
        {
            // Calls the game manager to handle health and provide a game over if necessary
            CYU_gameManager.CYU_ResetLevel();
            // Destroys this instanced obstacle
            Destroy(this.gameObject);
            // Decrement obstacle count
            CYU_gameManager.CYU_obstacleCount--;
        }
    }
}
