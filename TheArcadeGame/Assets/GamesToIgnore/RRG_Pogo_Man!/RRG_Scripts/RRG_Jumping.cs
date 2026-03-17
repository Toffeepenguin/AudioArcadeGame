using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.ProBuilder.MeshOperations;
public class RRG_Jumping : MonoBehaviour
{
    InputSubscription RRG_Input;
    [SerializeField] RRG_Game_Over gameOverReference;
    [SerializeField] RRG_Car_Move carMove;

    public Rigidbody body;

    [SerializeField] TMP_Text hitIndicatorMiss;
    [SerializeField] TMP_Text hitIndicatorHit;
    [SerializeField] TMP_Text scoreText;

    //How high the player jumps, made public for easy access through inspector.
    public float playerJumpAcceleration = 0;

    private float ymovegrass;

    //Movement speed value; higher it is the faster the sprite moves.
    public float movementSpeed;

    private float multipled;

    private float playerYTransform;

    public float scoreCount;

    private float delay = 1f;
    private float delayCar = 4f;

    private bool canJump;
    private bool canDoubleJump;
    private bool pressedSpace;
    private bool delayCarSpawn;
    private bool gameIsPaused;
    

    private bool countDown;

    public GameObject player;
    public GameObject roadTrigger;
    public GameObject car;


    //Timing mechanic
    float jumpingTiming = 1.15f;

    bool movementSlow;
    bool pressSpace;

    public AudioSource bounceSFX;
    public AudioSource doubleJumpSFX;

    // Start is called before the first frame update
    void Start()
    {
        scoreCount = 1;

        canJump = false;
        countDown = false;
        movementSlow = false;
        pressSpace = false;
        pressedSpace = false;
        canDoubleJump = false;
        delayCarSpawn = false;  
        

        hitIndicatorMiss.enabled = false;
        hitIndicatorHit.enabled = false;
        hitIndicatorMiss.text = "";
        hitIndicatorHit.text = "";

        RRG_Input = GetComponent<InputSubscription>();

        multipled = 1f;
    }

    private void FixedUpdate()
    {
        if (RRG_Input.SpaceInput && !canJump && countDown && !pressedSpace)
        {
            pressedSpace = true;
            if (playerYTransform >= jumpingTiming && playerYTransform < 2)
            {
                movementSlow = true;

                if (movementSlow)                              
                {
                    hitIndicatorMiss.enabled = true;
                    hitIndicatorMiss.text = "Miss";
                    hitIndicatorHit.enabled = false;
                    scoreCount += 0;
                    if (movementSpeed > 3)
                    {
                        movementSpeed -= multipled;
                    }
                }

                countDown = false;
            }
            if (playerYTransform <= jumpingTiming)
            {
                movementSlow = false;

                if (!movementSlow)
                {
                    hitIndicatorHit.enabled = true;
                    hitIndicatorHit.text = "Hit";
                    hitIndicatorMiss.enabled = false;
                    
                    if (movementSpeed <= 6)
                    {
                        multipled += 0.06f;
                        scoreCount += multipled * 0.216f;
                    }
                    if (movementSpeed >= 6)
                    {
                        multipled += 0;
                        scoreCount += scoreCount * 0.216f;
                    }
                    if (movementSpeed >= 10)
                    {
                        movementSpeed = 10;
                    }

                    movementSpeed *= multipled;
                    
                }

                countDown = false;
            }
        }
        if (RRG_Input.SpaceInput && !canJump && playerYTransform >= jumpingTiming && playerYTransform < 2 && !pressedSpace)
        {
            pressedSpace = true;
            hitIndicatorMiss.enabled = true;
            hitIndicatorMiss.text = "Miss";
            hitIndicatorHit.enabled = false;
            if (movementSpeed >= 1)
            {
                movementSpeed /= multipled;
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        playerYTransform = player.transform.position.y;

        scoreText.text = "Score " + scoreCount.ToString("f2");


        if (canJump)
        {
            Jump(playerJumpAcceleration);
            canJump = false;   
        }
        if (RRG_Input.SpaceInput && canDoubleJump && 2 <= player.transform.position.y)
        {
            Jump(300);
            canDoubleJump = false;
            doubleJumpSFX.Play();
        }
        if (pressedSpace)
        {
            movementSlow = true;
            delay -= Time.deltaTime;
            if (delay < 0)
            {
                pressedSpace = false;
            }
        }
        if (!pressedSpace)
        {
            delay = 1f;
        }
        if (delayCarSpawn)
        {
            delayCar -= Time.deltaTime;
            if (delayCar < 0)
            {
                print(delayCar);
                Instantiate(car, new Vector3(100, 0.5f, 0), Quaternion.Euler(-90, 0, 0));
                delayCar = 4f;
                delayCarSpawn = false;
            }

        }
        //Trophy earned
        if (scoreCount >= 100)
        {
            print("Trophy Earned");
            if (PlayerPrefs.GetInt("RRG_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("RRG_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }

        if (RRG_Input.MenuInput)
        {
            gameIsPaused = !gameIsPaused;
            gameOverReference.pauseScreen(true);
        }
        if (!gameIsPaused)
        {
            gameOverReference.pauseScreen(false);
        }
    }

    void Jump(float jumpAcceleration)
    {
        //Accessing the "AddForce" method of RigidBody2D to move the player up times the playerJumpAcceleration (a declared variable).
        body.AddForce(Vector2.up * jumpAcceleration);

        countDown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("onground");
        canJump = true;
        canDoubleJump = true;
        bounceSFX.Play();

        if (countDown)
        {
            pressSpace = true;
        }
            

        if (!RRG_Input.SpaceInput && pressSpace)
        {
            hitIndicatorMiss.enabled = true;
            hitIndicatorMiss.text = "Miss";
            hitIndicatorHit.enabled = false;
            pressSpace = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            gameOverReference.GameOverScreen(true);
            body.freezeRotation = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameObject roadClone = Instantiate(roadTrigger, new Vector3(82, 0.5f, 0), Quaternion.Euler(-90, 0, 0));
            Destroy(roadClone, 120.0f);
            Instantiate(car, new Vector3(100, 0.5f, 0), Quaternion.Euler(-90, 0, 0));

            delayCarSpawn = true;
        }
    }
}
