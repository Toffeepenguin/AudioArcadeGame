using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHO_Prefab : MonoBehaviour
{
    public JHO_GameManager JHO_Spawner;

    [SerializeField] public GameObject JHO_G_Items;

    [SerializeField] public GameObject JHO_B_Items;

    public Animator JHO_anima;

    public bool JHO_IsGoodItem;

    private float JHO_fallSpeed = 2f; // Default fall speed

    public void SetFallSpeed(float minSpeed, float maxSpeed)
    {
        JHO_fallSpeed = Random.Range(minSpeed, maxSpeed); // Assign a random speed
    }

    void Update()
    {
        // Move the item down based on fall speed
        transform.position += Vector3.down * JHO_fallSpeed * Time.deltaTime;

        //Debug.Log("Falling, speed: " + JHO_fallSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" || collision.gameObject.name == "ItemCatcher")
        {
            Destroy(this.gameObject);
            JHO_Spawner.DecrementItemCount(JHO_IsGoodItem);
            //Debug.Log("Destroyed");
        }

        if (collision.gameObject.name == "Player")
        {
            if (JHO_IsGoodItem)
            {
                Destroy(this.gameObject);
                JHO_ScoreManager.Instance.AddScore();
                JHO_Spawner.DecrementItemCount(true);
            }
            else
            {
                Destroy(this.gameObject);
                JHO_ScoreManager.Instance.LoseScore(); // Correctly decrement the score for bad items
                JHO_Spawner.DecrementItemCount(false);
            }

            Debug.Log("Item Collected");
        }
    }
}

