using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRO_Enemy : MonoBehaviour
{
    [SerializeField] bool Boss = false;
    [SerializeField] bool Enemy = false;

    [SerializeField] private int health = 300;
    private int maxHealth;
    public MRO_HealthbarBehaviour Healthbar;
    public MRO_UI MRO_UI;
    public GameObject EnemyLeft;
    public GameObject EnemyLeftBottom;
    public GameObject EnemyRight;
    public GameObject EnemyRightBottom;

    private MRO_playerHealth playerHealth;

    [Header("Boss Movement Settings")]
    [SerializeField] private Transform pointA;  
    [SerializeField] private Transform pointB;  
    [SerializeField] private float moveSpeed = 2f; 
    private bool isMoving = false;  
    private float currentSpeed = 0f; 
    [SerializeField] private float speedIncreaseRate = 1f; 

    [Header("Boss Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private int bulletsPerWave = 10; 
    [SerializeField] private float bulletSpeed = 5f; 
    [SerializeField] private float fireRate = 1f; 
    [SerializeField] private float spreadAngle = 90f; 
    private bool isShooting = false; 

    void Start()
    {
        maxHealth = health;
        if (Healthbar != null)
        {
            Healthbar.SetHealth(health, maxHealth);
        }

        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<MRO_playerHealth>();
        }

        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Bullet Prefab or Fire Point is not set.");
            return;
        }

      //  StartCoroutine(ShootBulletsForDuration(30f));

        EnemyLeft.SetActive(false);
        EnemyLeftBottom.SetActive(false);
        EnemyRight.SetActive(false);
        EnemyRightBottom.SetActive(false);
    }

    void Update()
    {
        fireRate -= Time.deltaTime;
        if (fireRate < 0f)
        {
            ShootWave();
            fireRate = 2f;

        }
        
        if (health <= 220 && !isMoving && Boss)
        {
            isMoving = true;
            StartCoroutine(SmoothStartMovement());
            EnemyLeft.SetActive(true);
            EnemyLeftBottom.SetActive(true);
            EnemyRight.SetActive(true);
            EnemyRightBottom.SetActive(true);
        }

        if (isMoving && Boss)
        {
            MoveBetweenPoints();
        }


    }

    private void MoveBetweenPoints()
    {
        float t = Mathf.PingPong(Time.time * currentSpeed, 1f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }

    private IEnumerator SmoothStartMovement()
    {
        while (currentSpeed < moveSpeed)
        {
            currentSpeed += speedIncreaseRate * Time.deltaTime;
            yield return null;
        }
        currentSpeed = moveSpeed;
    }

    private void ShootWave()
    {
        if (health <= 260 && bulletPrefab != null)
        {


            Debug.Log("Shooting a wave of bullets...");
            for (int i = 0; i < bulletsPerWave; i++)
            {
                
                
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                

                float baseAngle = 270f;
                float angleOffset = spreadAngle * ((float)i / (bulletsPerWave - 1) - 0.5f);
                float angle = baseAngle + angleOffset;

                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = direction * bulletSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Boss)
        {
            if (collision.gameObject.layer == 8) 
            {
                Destroy(collision.gameObject);
                health--;

                if (Healthbar != null)
                {
                    Healthbar.SetHealth(health, maxHealth);
                }

                if (health <= 0)
                {
                    MRO_UI.GameOver(playerHealth.GetPowerUpsCollected());
                    Destroy(this.gameObject);

                    if (PlayerPrefs.GetInt("MRO_Trophie_Int") != 1)
                    {
                        PlayerPrefs.SetInt("MRO_Trophie_Int", 1);
                        PlayerPrefs.Save();
                    }

                    Debug.Log("Win");
                }
            }
        }

        if (Enemy)
        {
            if (collision.gameObject.layer == 8)
            {
                Destroy(collision.gameObject);
                health--;

                if (Healthbar != null)
                {
                    Healthbar.SetHealth(health, maxHealth);
                }

                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
