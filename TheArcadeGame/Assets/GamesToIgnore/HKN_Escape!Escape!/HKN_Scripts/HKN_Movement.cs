using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class HKN_Movement : MonoBehaviour
{
    [SerializeField] InputSubscription GetInput;
    Rigidbody2D rb;

    public Rigidbody2D Fireball;
    public Transform LaunchOffset;

    public Vector2 PlayerMovement;
    

    float moveSpeed = 5.0f;

    float LastThrowTime = -1.5f;

    //public AudioSource Footstep;
    //public AudioSource Shoot;

    bool fireTimer()
    {
        if (Time.time - LastThrowTime > 1.5f)
        {
            LastThrowTime = Time.time;
            return true;
        }
        return false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //////////////added by Izzy////////////////////////
        if (GetInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
        PlayerMovement = new Vector2(GetInput.NormalizedMovementInput.x, GetInput.NormalizedMovementInput.y);
        rb.linearVelocity = new Vector2(PlayerMovement.x, PlayerMovement.y) * moveSpeed;


        if (GetInput.NormalizedMovementInput.x > 0) // Right Movement ->
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = rotation;
            //Footstep.Play();


        }

        if (GetInput.NormalizedMovementInput.x < 0) // Left Movement ->
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, -180, 0);
            transform.rotation = rotation;
            //Footstep.Play();

        }

        if (GetInput.NormalizedMovementInput.y > 0) // Up Movement 
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = rotation;
            //Footstep.Play();

        }

        if (GetInput.NormalizedMovementInput.y < 0) 
        {
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = rotation;
            //Footstep.Play();

        }

        if (GetInput.MenuInput) 
        {
            
        }

        if (GetInput.SpaceInput && fireTimer())
        { 
            Rigidbody2D Fireballinstance = Instantiate(Fireball, this.transform.position,this.transform.rotation);
            Fireballinstance.AddForce(Fireballinstance.transform.right * 500);
            //Shoot.Play();
        }

    }



}


