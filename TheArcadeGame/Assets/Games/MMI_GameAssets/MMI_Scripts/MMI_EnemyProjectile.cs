using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMI_EnemyProjectile : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Type of Projectile")]
    [SerializeField] bool DestroySelfOnPlayer = false;
    [SerializeField] bool SelfRotating = false;
    [SerializeField] bool ClockWiseRotation = false;
    float RotationSpeed;

    [SerializeField] bool BasicProjectile = false;
    [SerializeField] bool AimedAtPlayer = false;
    [SerializeField] bool SpawnPausedProjectile = false;

    [SerializeField] float ProjectileRange = 5f;
    [SerializeField] float ProjectileSpeed = 1f;
    [SerializeField] int DamageValue = 50;
    public int CurrentDamageValue;

    Transform Player;
    Transform ChildSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("MMI_Player").GetComponent<Transform>();

        if (SelfRotating)
        {
            ChildSprite = GetComponentInChildren<Transform>();
        }
    }


    private void Start()
    {
        transform.SetParent(GameObject.Find("GameMaterial").transform);

        CurrentDamageValue = DamageValue;
        Destroy(gameObject, ProjectileRange);



        if (BasicProjectile) 
        {
            rb.linearVelocity = transform.up * ProjectileSpeed;
        }


        if (AimedAtPlayer)
        {
            rb.linearVelocity = transform.right * ProjectileSpeed;

        }

        if (SpawnPausedProjectile)
        {

            Invoke("SwordHandler", 2f);
        }
    }



    private void Update()
    {
        //Debug.Log("Projectile Damage Value" + CurrentDamageValue);



        if (SelfRotating)
        {
            if (ClockWiseRotation)
            {
                RotationSpeed += 360f * Time.deltaTime;
            }
            else
            {
                RotationSpeed -= 360f * Time.deltaTime;
            }


            ChildSprite.transform.rotation = Quaternion.Euler(0,0,RotationSpeed);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DestroySelfOnPlayer)
        {
            if (collision.gameObject.layer == 3)
            {
                Destroy(gameObject);
            }
        }


        if (collision.gameObject.layer == 9)
        {

            Destroy(gameObject);
        }



    }



    void SwordHandler()
    {
        rb.linearVelocity = transform.up * ProjectileSpeed;
    }






}
