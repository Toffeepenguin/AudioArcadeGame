using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SWA_Player : MonoBehaviour
{
    InputSubscription cantInput;
    [SerializeField] SWA_MenuController menu;

    public GameObject gameUI;
    public bool isAlive = true;
    public SWA_Bullet bulletPrefab;
    public SWA_Enemy enemy;


    public float ColdDowmTime = 0.1f;
    public float MoveForward = 12f;
    public float bulletSpeed = 10f;


    public int maxHP = 4;
    public int currentHP;
    public static int scores;

    public Text playerHPText;
    public Text playerScores;

    private Rigidbody2D _rigidbody;
    private bool _thrusting;
    private bool counterStart;
    private float Angle;
    private float fireRateCounter;

    private void Awake()
    {
        cantInput = GetComponent<InputSubscription>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentHP = maxHP;
        playerHPText.text = "HP: " + currentHP;
        isAlive = true;
        scores = 0;
        playerScores.text = "Scores: " + scores;
    }

    private void Update()
    {
        playerScores.text = "Scores:" + scores;


        _thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        HandleProjectileShot();

        // set the movement functions in a new way.
        _rigidbody.linearVelocity = transform.up * cantInput.NormalizedMovementInput.y * MoveForward * Time.deltaTime;

        if (cantInput.NormalizedMovementInput.x > 0)
        {
            Angle -= 0.4f;
        }

        if (cantInput.NormalizedMovementInput.x < 0)
        {
            Angle += 0.4f;
        }

        transform.rotation = Quaternion.Euler(0, 0, Angle);

        if (currentHP <= 0)
        {
            // player dead and show Game Over Page.
            Destroy(gameObject);
            isAlive = false;
            Debug.Log(isAlive);
            gameUI.SetActive(false);
            menu.isGameOver = true;
            Time.timeScale = 0.0f;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        _rigidbody.linearVelocity = transform.up * bulletSpeed;
    }

    void HandleProjectileShot()
    {
        fireRateCounter -= Time.deltaTime;
        if (cantInput.SpaceInput && fireRateCounter <= 0f)
        {
            Shoot();
            fireRateCounter = ColdDowmTime;
            //AddScores();

            //add audio
            //AudioClip clip = ShootingSound[Random.Range(0, shootingLength)];


        }
    }

    // trigger with enemy and boss bullets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            TakeDamager();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            TakeDamager();
            Destroy(collision.gameObject);
        }
    }

    private void CollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 6)
        //{
        //    TakeDamager();
        //    Destroy(collision.gameObject);
        //}

        //if (collision.gameObject.layer == 7)
        //{
        //    TakeDamager();
        //    Destroy(collision.gameObject);
        //}
    }


    // create a function take part in damage
    void TakeDamager()
    {
        currentHP--;
        playerHPText.text = "HP: " + currentHP;

        if (currentHP <= 0 && isAlive == true)
        {
            // player dead and show Game Over Page.
            //Destroy(gameObject);
            isAlive = false;
            gameUI.SetActive(false);
            menu.isGameOver = true;
            Time.timeScale = 0.0f;
        }
    }
}