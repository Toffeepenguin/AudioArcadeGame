using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_BikeController : MonoBehaviour
{
    // Bike parameters
    public float maxSpeed, acceleration, steerStrength;
    [Range(0, 10)] public float breakingFactor;

    // Rigidbody reference
    public Rigidbody SphereRB;

    // Input variables
    private float moveInput, steerInput;

    // Reference to InputSubscription
    [SerializeField] private InputSubscription inputSubscription;

    // Start is called before the first frame update
    void Start()
    {
        SphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values from InputSubscription
        moveInput = inputSubscription.NormalizedMovementInput.y;
        steerInput = inputSubscription.NormalizedMovementInput.x;

        // Update position of the bike to match the SphereRB position
        transform.position = SphereRB.transform.position;
    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
        Brake();
    }

    // Handle the movement based on the input
    void Movement()
    {
        // Use Lerp to smoothly move the bike towards the target speed based on input
        SphereRB.linearVelocity = Vector3.Lerp(SphereRB.linearVelocity, maxSpeed * moveInput * transform.forward, Time.fixedDeltaTime * acceleration);
    }

    // Handle the rotation based on steering input
    void Rotation()
    {
        // Rotate the bike based on the steering input
        transform.Rotate(0, steerInput * moveInput * steerStrength * Time.fixedDeltaTime, 0, Space.World);
    }

    // Handle the braking
    void Brake()
    {
        if (inputSubscription.NormalizedMovementInput.y < 0f)  // Brake if moving backwards
        {
            SphereRB.linearVelocity *= breakingFactor / 10;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Debug.Log("COLLISIONSSSSSS");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("COLLISIONSSSSSS");
        }
    }




}
