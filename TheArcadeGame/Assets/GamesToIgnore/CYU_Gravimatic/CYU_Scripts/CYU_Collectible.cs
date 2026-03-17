using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CYU_Collectible : MonoBehaviour
{

    public CYU_GameManager CYU_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the collectible's rb, and adds a force with a random speed to move it
        Rigidbody2D CYU_collectibleRB = GetComponent<Rigidbody2D>();
        Vector2 CYU_collectibleDir = new Vector2(-1, 0).normalized;
        float CYU_collectibleSpeed = Random.Range(5f, 11f);

        CYU_collectibleRB.AddForce(CYU_collectibleDir * CYU_collectibleSpeed, ForceMode2D.Impulse);

        // Increment collectible count
        CYU_gameManager.CYU_collectibleCount++;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the obstacle has gone off screen
        if (this.gameObject.transform.position.x < -21f)
        {
            // Destroy object, and decrement obstacle count
            Destroy(this.gameObject);
            CYU_gameManager.CYU_collectibleCount--;
        }
    }

    // Collision check with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks if instanced collectible is colliding with the player
        if (collision.gameObject.name == "Player")
        {
            // Call the gamemanager's function to handle incrementing the score, remove instanced game object, and decrement variable
            CYU_gameManager.CYU_CollectibleScore();
            Destroy(this.gameObject);
            CYU_gameManager.CYU_collectibleCount--;
        }
    }
}
