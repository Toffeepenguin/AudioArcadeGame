using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

//Struct to hold direction
[System.Serializable]
public struct StartingDirection
{
    public bool LEFT, RIGHT, UP, DOWN;
}

public class basicplayercontroller : MonoBehaviour
{
    private Rigidbody2D rb;
    private TrailRenderer tr;
    private JRO_GameManager gM;

    [SerializeField] InputSubscription _inputs;
    [SerializeField] public static float movespeed = 3;
    [SerializeField] float timeDelay = 0f;
    [SerializeField] StartingDirection sDirection;
    [SerializeField] Transform Target;

    //Is moving?s
    bool moving;
    bool delay;
    public int PlayerHealth = 2;
    private bool dead = false;

    //For initial direction control
    public void DirectionHandler()
    {
        //DO once
        if (!moving)
        {
            if (sDirection.LEFT == true)
            {
                rb.linearVelocity = Vector2.left * movespeed;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                moving = true;
            }
            if (sDirection.RIGHT == true)
            {
                rb.linearVelocity = Vector2.right * movespeed;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                moving = true;
            }
            if (sDirection.UP == true)
            {
                rb.linearVelocity = Vector2.up * movespeed;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                moving = true;
            }
            if (sDirection.DOWN == true)
            {
                rb.linearVelocity = Vector2.down * movespeed;
                transform.rotation = Quaternion.Euler(0, 0, -90);
                moving = true;
            }
        }
    }

    //Player movement
    public void moveUp()
    {
        if (delay == false && _inputs.NormalizedMovementInput.y > 0)
        {
            rb.linearVelocity = Vector2.up * movespeed;
            transform.rotation = Quaternion.Euler(0, 0, 90);
            delay = true;
            Invoke("inputLock", timeDelay);
        }
    }
    //public void testInput()
    //{
    //    if (_inputs.MoveInput.x < 0)
    //    {
    //        rb.velocity = Vector2.left;
    //    }
    //}
    public void moveDown()
    {
        if (delay == false && _inputs.NormalizedMovementInput.y < 0)
        {
            rb.linearVelocity = Vector2.down * movespeed;
            transform.rotation = Quaternion.Euler(0, 0, -90);
            delay = true;
            Invoke("inputLock", timeDelay);
        }
    }
    public void moveRight()
    {
        if (delay == false && _inputs.NormalizedMovementInput.x > 0)
        {
            rb.linearVelocity = Vector2.right * movespeed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            delay = true;
            Invoke("inputLock", timeDelay);
        }
    }
    public void moveLeft()
    {
        
        if (delay == false && _inputs.NormalizedMovementInput.x < 0)
        {
            rb.linearVelocity = Vector2.left * movespeed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            delay = true;
            Invoke("inputLock", timeDelay);
        }
    }
    public void quit()
    {
        if (_inputs.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }
    //Movement delay
    void inputLock()
    {
        delay = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        gM = FindObjectOfType<JRO_GameManager>();


        //Start initial direction
        DirectionHandler();
        //Spawn at spawner
        transform.position = Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveUp();
        moveDown();
        moveLeft();
        moveRight();
        quit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("MazeWall") || collision.gameObject.name == "JRO_PlayerSprite")
        {
            rb.linearVelocity = Vector3.zero;
            PlayerHealth--;

            death();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Portal"))
        {
            delay = false;
            moveRight();
            tr.Clear();
        }
    }

    public void death()
    {
        if (PlayerHealth <= 0)
        {
            JRO_GameManager.Score = 0;
            movespeed = 3;
            
            gM.LoadNextLevel("JRO_Level1");//GetActiveScene().buildIndex, LoadSceneMode.Single);
            dead = true;
        }
    }
}

