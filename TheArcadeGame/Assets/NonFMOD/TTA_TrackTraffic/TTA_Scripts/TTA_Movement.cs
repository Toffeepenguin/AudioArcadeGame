using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTA_Movement : MonoBehaviour
{
    public bool hasPlayerDied;
    InputSubscription _Inputs;

    private Rigidbody2D rb;


    [SerializeField] float movespeed = 5.0f;
    Vector2 PlayerMovement;

    Vector2 MOVEMENT;

    Transform GemObject;
    void Start()
    {
        _Inputs = GetComponent<InputSubscription>();

        hasPlayerDied = false;
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement = _Inputs.NormalizedMovementInput;
        rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * movespeed;

        if (_Inputs.SpaceInput && GemObject != null)
        {
            GemObject.SetParent(transform);
        }

        if (_Inputs.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 10 - obstacle
        //11 - Interactable
        if (collision.gameObject.layer == 11)
        {
            GemObject = collision.GetComponent<Transform>();
           
            
        }

        if (collision.gameObject.layer == 10)
        {
            hasPlayerDied = true;
            Destroy(gameObject);
        } 
        if (collision.gameObject.layer == 9)
        {
            
        }

        

    }

    //if the green walls turn into obstacles, then the mouse will react if player touches them.
    //The walls should be obstacle(10) and the object that will be dragged out will be intractive(aka11

}
