using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KCY_S_Lasso : MonoBehaviour
{
    int score = 0;
    public bool caught = false;
    Rigidbody2D rb;
    float timer = 0;
    public KCY_Player player;
    public KCY_ScoreManager scoreManager;
    public GameObject pScore;
    int upForce;
    int rightForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player") .GetComponent<KCY_Player>();
        
        //rb.AddForce(transform.up * 350);
        //rb.AddForce(transform.right * 200);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer < 0.8f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Floor")
        {
            Destroy(gameObject);
        }
        if (collision.name == "Enemy(Clone)")
        {
            
            caught = true;
            Destroy(collision.gameObject);
            player.UpdateScore(50);
        }
    }

    
}
