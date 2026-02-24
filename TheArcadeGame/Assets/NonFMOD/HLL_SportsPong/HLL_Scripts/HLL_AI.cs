using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HLL_AI : MonoBehaviour
{
    // Start is called before the first frame update

    private float DirectY;
    private float DirectX;
    private float movespeed;
    private Rigidbody2D rb;

    public GameObject beach_ball;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        DirectX = 0f;
        DirectY = 0f;
        movespeed = 4f;
    }

    private void FixedUpdate()
    {
        if (beach_ball != null)
        {

            if (beach_ball.transform.position.x > transform.position.x)
            {

                if (beach_ball.transform.position.x > 0)
                {

                    DirectX = 2f;
                }

                else
                {
                    DirectX = 0f;
                }
            }
            else if (beach_ball.transform.position.x < transform.position.x)
            {

                if (beach_ball.transform.position.x < 0.1)
                {

                    DirectX = -2f;
                }

                else
                {
                    DirectX = 0f;
                }
            }

        }
        

        if (beach_ball.transform.position.y > transform.position.y + 0.1f)
        {
            DirectY = 2f;
        }
        else if (beach_ball.transform.position.y < transform.position.y - 0.1f)
        {
            DirectY = -2f;
        }
        else
        {
            DirectY = 0f;
        }


        rb.linearVelocity = new Vector2(DirectX * movespeed, DirectY * movespeed);
    }
    




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HLL_goalbounds>())
        {
            DirectY *= -1f;
        }
    }

}

    

