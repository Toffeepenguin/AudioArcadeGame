using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCY_Enemy : MonoBehaviour
{
    private Animator animator;
    public GameObject bullet;
    GameObject bulletInst;
    Rigidbody2D rb;
    Transform ray;
    RaycastHit2D hit;
    Vector2 ePos;
    public AudioSource shootSource;

    float timer = 0.0f;
    float sTimer = 0.0f;
    float interval;
    bool canShoot = false;
    bool gameOver = false;
    Vector3 vel = new Vector3(-1, 0);

    // Start is called before the first frame update
    void Start()
    {
        //bulletInst = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        interval = Random.Range(2, 4);
        ray = this.gameObject.transform.GetChild(0);
        hit = Physics2D.Raycast(transform.position, Vector2.right);
        ePos = new Vector2(transform.position.x, transform.position.y + 1);
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody2D>();
        RaycastHit2D raycast = Physics2D.Raycast(ePos, -Vector2.right, 1);

        if (raycast)
        {
            canShoot = false;
        } else
        {
            canShoot = true;
        }

        //enemy should only shoot when an obstacle on the screen is TO THE LEFT of the enemy.
        //should also vary speed based
        if (rb.linearVelocity == new Vector2(rb.linearVelocity.x, 0) && !gameOver)
        {
            transform.position += vel * Time.deltaTime;

            if (timer < interval)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if (canShoot)
                {
                    Shoot();
                }
                
                //animator.SetTrigger("DoShoot");
                //bulletInst = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
                //bulletInst.transform.Rotate(new Vector3(0, 0, 90));
                //interval = Random.Range(2, 5);
            }
        } else
        {
            timer = 0.0f;
        }
        
        
    }

    void jump(Rigidbody2D rigid)
    {
        //print("rb is " + rb);
        rigid.AddForce(transform.up * 300);
    }

    void Shoot()
    {
        animator.SetTrigger("DoShoot");

        if (sTimer < 0.5f)
        {
            sTimer += Time.deltaTime;
        } else
        {
            bulletInst = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            //bulletInst.transform.Rotate(new Vector3(0, 0, 90));
            shootSource.Play();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SheriffGame/ShootingSFX");
            interval = Random.Range(2, 5);
            timer = 0.0f;
        }
    }

    public void StopGame()
    {
        //gameOver = true;
        vel = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Obstacle(Clone)")
        {
            jump(rb);
        }
    }
}
