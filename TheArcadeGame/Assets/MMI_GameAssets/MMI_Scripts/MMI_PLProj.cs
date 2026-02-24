using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMI_PLProj : MonoBehaviour
{
    Rigidbody2D rb;
    public bool PiercingType = false;

    float ProjectileSpeed = 15f;
    int MinDamage = 500;
    int MaxDamage = 5000 ;
    public int CurrentDamageValue = 0;

    float DPSLossTimer;
    bool ReachedMinimum = false;

    // check
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        Invoke("DestroySelf", 2f);
        CurrentDamageValue = MaxDamage;
        rb.linearVelocity = transform.up * ProjectileSpeed;
    }



    private void Update()
    {
       

        if (DPSLossTimer > 0f)
        {
            DPSLossTimer -= Time.deltaTime;
        }

        if (!ReachedMinimum && CurrentDamageValue > MinDamage && DPSLossTimer <= 0f)
        {
            CurrentDamageValue -= 200;
            DPSLossTimer = 0.1f;
        }
        
        if (CurrentDamageValue <= MinDamage && !ReachedMinimum)
        {
            ReachedMinimum = true;
            CurrentDamageValue = MinDamage;
        }

        //Debug.Log("Projectile Damage Value" + CurrentDamageValue);
    }


    void DestroySelf()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!PiercingType)
        {
            if (collision.gameObject.layer ==6)
            {
                Destroy(gameObject);
            }
        }

       
        if (collision.CompareTag("Obstacle"))
        {
            
            
            //temp
            MMI_ActionListerner.OnWallProjCol();
            Destroy(gameObject);
        }
        
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 6)
        {
            MMI_ActionListerner.OnEnemiesHit();
        }

    }


}
