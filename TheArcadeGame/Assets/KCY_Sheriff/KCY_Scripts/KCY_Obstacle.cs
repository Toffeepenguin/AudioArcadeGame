using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KCY_Obstacle : MonoBehaviour
{
    public SpriteRenderer renderer;
    bool gameOver = false;
    Vector3 velocity = new Vector3(-5, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
        transform.position += velocity * Time.deltaTime;
        
    }

    public void StopGame()
    {
        velocity = new Vector3(0, 0, 0);
        //gameOver = true;
    }
}
