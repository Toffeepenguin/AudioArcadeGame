using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KCY_Player : MonoBehaviour
{
    float velX = 0;
    float velY = 0;
    float timer = 0.0f;
    bool timeStart = false;
    bool onGround = false;

    Rigidbody2D rb;
    private Animator animator;
    public GameObject topColl;
    public GameObject bottomColl;
    public GameObject lasso;
    public GameObject lassoAnim;
    private GameObject lassoInst;
    private GameObject lassoInstAnim;
    public SpriteRenderer spriteRenderer;
    public KCY_ScoreManager scoreManager;
    public GameObject gameManager;
    BoxCollider2D topCollBox;
    BoxCollider2D botCollBox;

    public AudioSource jumpSource;
    public AudioSource lassoSource;
    public AudioSource gallopSource;
    //Sprite sprites = "Sheriff_Player_Duck";

    [SerializeField] InputSubscription getInput;

    bool galloping = false;
    bool canPlayGallop = true;
    bool grounded = false;
    bool jumped = false;
    bool ducked = false;
    bool thrown = false;
    bool spawnedlasso = false;
    bool trackCaught = false;
    bool canJump = true;
    bool gameOver = false;

    public int SCORE = 0;

    Vector2 upDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        topCollBox = topColl.GetComponent<BoxCollider2D>();
        botCollBox = bottomColl.GetComponent<BoxCollider2D>();
        //upDown = new Vector2(getInput.NormalizedMovementInput.x, getInput.NormalizedMovementInput.y);
    }

    // Update is called once per frame
    void Update()
    {
        //gets the vector of the movement from input
        upDown = new Vector2(getInput.NormalizedMovementInput.x, getInput.NormalizedMovementInput.y);

        if (rb.linearVelocity.y < 0.5)
        {
            animator.SetBool("DoJump", false);
        }
        //when the player is on the ground:

        if (grounded)
        {
            animator.SetBool("DoJump", false);

            //if arrow key up:
            if (upDown.y > 0 && !jumped && canJump)
            {
                jumped = true;
                jumpSource.Play();
                animator.SetBool("DoJump", true);
                grounded = false;
                rb.AddForce(transform.up * 300);
            }
            else if (upDown.y == 0 && jumped)
            {
                jumped = false;
            }


            if (getInput.SpaceInput && !thrown)
            {
                thrown = true;
                lassoSource.Play();
                timeStart = true;
                lassoInstAnim = Instantiate(lassoAnim, new Vector3(transform.position.x + 2, transform.position.y), Quaternion.identity);
                canJump = false;
            }
            else if (!getInput.SpaceInput && thrown)
            {
                thrown = false;
            }


            if (timeStart == true)
            {
                if (timer < 0.7f)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    Destroy(lassoInstAnim);
                    lassoInst = Instantiate(lasso, new Vector3(transform.position.x + 3.5f, transform.position.y), Quaternion.identity);
                    //print(lassoInst.GetComponent<S_Lasso>().caught);
                    if (lassoInst.GetComponent<KCY_S_Lasso>().caught)
                    {
                        print("caught");
                        SCORE += 1;
                    }
                    canJump = true;
                    timer = 0.0f;
                    timeStart = false;

                }
            }


            if (upDown.y < 0 && !ducked)
            {
                //transform.localScale = new Vector3(1.25f, 0.5f, 0);
                ducked = true;
                animator.SetTrigger("DoDuck");
                //boxCollider.size = new Vector3(0.88f, 0.4f, 0);
                //boxCollider.offset = new Vector3(0, -0.22f);
                //boxCollider.enabled = false;
                botCollBox.enabled = false;
            }
            else if (upDown.y == 0 && ducked)
            {
                ducked = false;
                botCollBox.enabled = true;
                //boxCollider.enabled = true;
                //boxCollider.offset = new Vector3(0, 0);
                //boxCollider.size = new Vector3(0.88f, 0.88f, 0);

            }

        }
        else
        {
            gallopSource.Stop();
        }

        if (gameOver)
        {
            gallopSource.Stop();
        }

        //not working because lasso isnt made yet
        //print(lassoInst.GetComponent<S_Lasso>().caught);


        transform.position += new Vector3(velX, velY, 0);

        if (getInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Obstacle(Clone)" || collision.name == "Bullet(Clone)")
        {
            gameOver = true;
            gameManager.GetComponent<KCY_GameOver>().EndGame();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SheriffGame/Death SFX"); // Play SFX through FMOD once


            //SceneManager.LoadScene("GameOver");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            grounded = true;
            if (!ducked)
            {
                gallopSource.Play();
            }
        }
    }

    public void UpdateScore(int i)
    {
        SCORE += i;
        scoreManager.GetComponent<KCY_ScoreManager>().SetScore(SCORE);
    }

}
