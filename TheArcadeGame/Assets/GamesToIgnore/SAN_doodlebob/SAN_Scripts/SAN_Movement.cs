using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SAN_Movement : MonoBehaviour
{
    //UI STUFF
    public Image hearts;
    public TextMeshProUGUI scoretext;

    [SerializeField]
    private GameObject Player;
    
    
    private InputSubscription PlayerControls;
    public Rigidbody2D rb;
    public float moveSpeed = 10f;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public float health = 1;
    public AudioClip Whoosh;
    public AudioSource AudioSource;



    public bool PLeft;
    public bool Pright;
    public bool PunchHitbox = false;
    bool IsPunching = false;

    public GameObject RR;
    public GameObject LR;

    Vector2 moveDirection = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerControls = GameObject.Find("GameManager").GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        ////////////Added by Izzy///////////////
        if (PlayerControls.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
        ///////////////////////////////////////
        hearts.fillAmount = health;

        Punch();

        if (health < 0f)
        {
            Destroy(Player);
            SceneManager.LoadScene("SAN_MainMenu");
        }
    }

    void Punch()
    {

        if (PlayerControls.ActionInput1 && !IsPunching)
        {
            PLeft = true;
            Pright = false;
            spriteRenderer.flipX = true;
            StartCoroutine(PunchAnimation());
            AudioSource.PlayOneShot(Whoosh);

        }

        if (PlayerControls.ActionInput2 && !IsPunching)
        {
            PLeft = false;
            Pright = true;
            spriteRenderer.flipX = false;
            StartCoroutine(PunchAnimation());
            AudioSource.PlayOneShot(Whoosh);
        }


    }

    IEnumerator PunchAnimation()
    {

        PunchHitbox = true;
        IsPunching = true;
        anim.SetBool("Punch", true);
        //anim.Play("Punch");
        yield return new WaitForSeconds(0.6f);
        anim.SetBool("Punch", false);
        PunchHitbox = false;
        IsPunching = false;

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 0.34f;
            Destroy(collision.gameObject);
        }
    }   
}
