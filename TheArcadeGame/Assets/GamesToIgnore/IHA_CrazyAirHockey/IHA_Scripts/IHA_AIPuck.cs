using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHA_AIPuck : MonoBehaviour
{
    [SerializeField] private GameObject puck;   //puck AI is aiming for
    [SerializeField] private GameObject goal;   //enemy's goal
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 100f; // Controls how fast the paddle accelerates
    [SerializeField] private float secondsInFuture = 2;

    [SerializeField] private float attackThreshold = 40f; // distance to switch to attack mode

    private IHA_Puck[] pucks;  // Array to hold all enemy objects

    private Vector2 targetPosition;

    public float minXBound;
    public float maxXBound;
    public float minYBound;
    public float maxYBound;


    [SerializeField] private float choseDelay = 5.0f;
    private float lastTimePuckChosen;
    float minDistance = 100; //initaly high value
    IHA_Puck targetPuck = null;

    private Rigidbody2D rb;
    public AudioClip hitSound;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        lastTimePuckChosen = -choseDelay; //make sure puck is intially picked
        
    }

    // Update is called once per frame
    void Update()
    {
        pucks = GameObject.FindObjectsOfType<IHA_Puck>(); //find all pucks currently on table

        //check if no pucks on table
        if (pucks.Length == 0)
        {
            return;
        }

        //delay between rechosin gpuck to target
        if (Time.time - lastTimePuckChosen >= choseDelay)
        {
            FindNearestPuck();

            // Determine AI behavior: Defensive or Offensive
            float distanceToPuck = Vector2.Distance(transform.position, targetPuck.gameObject.transform.position);
            Debug.Log(distanceToPuck);
            // AI defends goal when puck is far; attacks when puck is close or random 50% chance
            if (distanceToPuck < attackThreshold || Random.RandomRange(0, 10) < 5)
            {
                // Attack mode: Move towards the puck
                //get current position of puck and then find where it will be in 2 seconds
                targetPosition = (Vector2)targetPuck.gameObject.transform.position + (targetPuck.gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized * secondsInFuture) ;
            }
            else
            {
                // Defensive mode: Move to goal's position to block the puck
                targetPosition = new Vector2(goal.gameObject.transform.position.x - 4, targetPuck.transform.position.y);
            }
        }
        //FindNearestPuck();



        // Move the AI paddle towards the target position
        MoveTowardsTarget(targetPosition);


        KeepInBounds();
        
    }

    void FindNearestPuck()
    {
        
        minDistance = 100; //initaly high value

        //iterate through all pucks in 
        foreach (IHA_Puck puck in pucks)
        {
            //find distance to puck
            float distance = Vector2.Distance(transform.position, puck.transform.position);
            Debug.Log(distance);
            //check if this is the closest puck
            if (distance < minDistance)
            {
                minDistance = distance; //set this as current min distance to check other pucks against
                targetPuck = puck; //set this as the puck for AI to target
            }
        }
    }
    void MoveTowardsTarget(Vector2 target)
    {

        // Calculate the direction to the target and smooth acceleration
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        Vector2 force = direction * acceleration;

        // Apply the force to the Rigidbody2D
        rb.AddForce(force * rb.mass);

        // Limit the maximum speed for smooth control
        if (rb.linearVelocity.magnitude > speed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect collision with the puck and add force to simulate a hit
        if (collision.gameObject.name.Contains("Puck"))
        {
            Vector2 hitDirection = (puck.transform.position - transform.position).normalized;
            collision.rigidbody.AddForce(hitDirection * 1000, ForceMode2D.Impulse);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(1000 * movementInput);
        }

        audio.PlayOneShot(hitSound);
    }
    private void KeepInBounds()
    {
        if (gameObject.transform.position.x >= maxXBound)
        {
            gameObject.transform.position = new Vector3(maxXBound, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.x <= minXBound)
        {
            gameObject.transform.position = new Vector3(minXBound, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.y >= maxYBound)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, maxYBound, gameObject.transform.position.z);
        }
        else if (gameObject.transform.position.y <= minYBound)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, minYBound, gameObject.transform.position.z);
        }
    }



}
