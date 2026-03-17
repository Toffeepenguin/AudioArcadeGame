using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMI_REMinion : MonoBehaviour
{
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject ArrowProjectile;
    
    Transform Player;

    Animator Anim;

    float FireRate;

    int HP = 5;


    private void Start()
    {
        Player = GameObject.Find("MMI_Player").GetComponent<Transform>();
        Anim = GetComponent<Animator>();
        transform.SetParent(GameObject.Find("GameMaterial").transform);
    }

    private void Update()
    {
        if (FireRate > 0)
        {
            FireRate -= Time.deltaTime;
        }

        if (FireRate <= 0)
        {
            Vector2 direction = (Player.position - FirePoint.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            FirePoint.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


            Instantiate(ArrowProjectile, FirePoint.position, FirePoint.rotation);



            FireRate = 0.4f;
        }





    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            MMI_ActionListerner.OnWallProjCol();
            

            HP--;

            Anim.SetTrigger("IsDamaged");

            if (HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }




}
