using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLI_BulletLogic : MonoBehaviour
{
    Rigidbody2D MLI_bullet_rb;
    SpriteRenderer MLI_bullet_sr;

    //CircleCollider2D MLI_bullet_col;

    public bool MLI_player_fired = false;

    public Color MLI_colour;

    public int MLI_Damage = 1;
    public float MLI_bullet_speed = 10.0f;
    public float MLI_bullet_size = 0.3f;
    public Vector2 MLI_bullet_dir = new Vector2(0,1);

    private void Awake()
    {
        //MLI_bullet_col = GetComponent<CircleCollider2D>();
        MLI_bullet_rb = GetComponent<Rigidbody2D>();
        MLI_bullet_sr = GetComponent<SpriteRenderer>();
    }

    public void UpdateValues()
    {
        MLI_bullet_sr.color = MLI_colour;
        Vector3 mli_scale = this.transform.lossyScale;
        mli_scale.x = MLI_bullet_size;
        mli_scale.y = MLI_bullet_size;
    }


    // Update is called once per frame
    void Update()
    {
        MLI_bullet_rb.linearVelocity = MLI_bullet_dir * MLI_bullet_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.name != "MLI_Bullet(Clone)")
        {
            if (collision.name == "MLI_Player" && MLI_player_fired == false)
            {
                Destroy(this.gameObject);
                MLI_PlayerStats MLI_playerStats = collision.gameObject.GetComponent<MLI_PlayerStats>();
                if (MLI_playerStats != null)
                {
                    MLI_playerStats.TakeDamage(MLI_Damage);
                } 
            }

            if (collision.name != "MLI_Player" && MLI_player_fired == true && collision.name != "MLI_Bounds")
            {
                Destroy(this.gameObject);
                MLI_EnemyStats MLI_enemyStats = collision.gameObject.GetComponent<MLI_EnemyStats>();
                if (MLI_enemyStats != null)
                {
                    MLI_enemyStats.TakeDamage(MLI_Damage);
                }
            }
            //Destroy(collision.gameObject);
            if (collision.name == "MLI_Bounds" || collision.name == "MLI_Enemy_Bounds") 
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "MLI_Bounds" || collision.name == "MLI_Enemy_Bounds")
        {
            Destroy(this.gameObject);
        }
    }
}
