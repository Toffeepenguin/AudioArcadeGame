

using UnityEngine;

public class EBA_EnemyMove : MonoBehaviour
{

    float Asteriodspeed;
    Rigidbody2D Erb;
    float bottomBoundary = -10f;
    // Start is called before the first frame update
    void Start()
    {
        Asteriodspeed = Random.Range(2f, 4f);
        Erb = GetComponent<Rigidbody2D>();
        Erb.linearVelocity = new Vector2(0, -Asteriodspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < bottomBoundary)
        {
            Destroy(gameObject);
            Debug.Log("enemy was destroyed");
        }

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "EBA_Bullet(Clone)")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.name == "EBA_Player")
        {
            Destroy(gameObject);
            
        }

        if (other.gameObject.name == "EBA_Protect")
        {
            Destroy(gameObject);
           
            Debug.Log("gameover");
        }
    }
    
}
