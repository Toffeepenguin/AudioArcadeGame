using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WHA_CarPlayer : MonoBehaviour
{
    [Header("Setup Stuff")]
    private GameObject gameMan;
    private InputSubscription _input;
    private WHA_RaceManager _raceManager;

    [Header("Movement Settings")]
    public float moveSpeed = 50f;
    public float maxSpeed = 15f;
    public float steerAngle = 10f;
    public float dragStrength = 0.98f;

    private Rigidbody carRB;

    [Header("Ground Check Settings")]
    public float groundCheckDistance = 2f; // Adjusted distance
    public LayerMask groundLayer;
    public LayerMask iceLayer;
    private bool isGrounded;
    private bool isOnIce;

    [Header("Wheel Settings")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public float maxWheelSteerAngle = 30f;
    public float steerSmoothSpeed = 5f; // Controls how smooth the wheel turning is

    private float currentSteerAngle; // Tracks the current steering angle for smooth transitions

    [Header("Sound Settings")]
    public AudioSource soundMaker;
    public AudioSource soundMakerTwo;
    public AudioClip carRunningSound;
    public AudioClip tireScreechSound;
    private float soundCooldownTimer = 0f;
    private float soundCooldownDuration = 0.5f; // 1-second cooldown

    [Header("Follow Path Once Won Settings")]
    public WHA_CarAIPath path; // Reference to the path
    public float rotationSpeed = 5f; // Speed of turning towards the target node
    public float waypointTolerance = 1f; // Distance to consider reaching a waypoint
    public float wheelTurnAngle = 30f; // Max steering angle for the front wheels
    public float wheelSpinSpeed = 360f; // Speed for wheel spinning effect
    private List<Transform> waypoints; // List of waypoints
    private int currentWaypointIndex = 0; // Index of the current target waypoint

    FMOD.Studio.EventInstance CarEngnie;    

    private void Start()
    {
        gameMan = GameObject.FindGameObjectWithTag("GameController");
        _input = gameMan.GetComponent<InputSubscription>();
        _raceManager = gameMan.GetComponent<WHA_RaceManager>();
        carRB = GetComponent<Rigidbody>();

        CarEngnie = FMODUnity.RuntimeManager.CreateInstance("event:/RacingGame/Player Car");
        CarEngnie.start();

        // Adjust center of mass to avoid flipping
        carRB.centerOfMass = new Vector3(0, -0.5f, 0);

        // Initialize waypoints from the path
        if (path != null)
        {
            waypoints = path.GetComponentsInChildren<Transform>().ToList();
            waypoints.Remove(path.transform); // Remove the parent path object itself
        }
        else
        {
            Debug.LogError("No path assigned to AI!");
        }
    }

    private void FixedUpdate()
    {
        if (!_raceManager.raceStarted)
            return;

        if (_raceManager.hasPlayerFinished == false)
        {
            // Apply movement, traction, and gravity
            HandleMovement();
            ApplyTraction();
        }
        else
        {
            if (waypoints == null || waypoints.Count == 0) return;

            // Get the current waypoint
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            // Move towards the waypoint
            MoveTowardsWaypoint(targetWaypoint);

            // Check if close enough to the waypoint to consider it reached
            float distanceToWaypoint = Vector3.Distance(transform.position, targetWaypoint.position);
            if (distanceToWaypoint < waypointTolerance)
            {
                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; // Loop back to start
            }
        }

        // Perform the ground check
        GroundCheck();

        // Apply gravity (normal gravity applied at all times)
        ApplyCustomGravity();

        // Lock the car's rotation on the Z-axis to prevent unwanted flipping
        carRB.rotation = Quaternion.Euler(carRB.rotation.eulerAngles.x, carRB.rotation.eulerAngles.y, 0);

        // Stop the car from sliding when it's grounded and still
        if (isGrounded && carRB.linearVelocity.magnitude < 0.1f)
        {
            // Set velocity to zero to stop sliding
            carRB.linearVelocity = Vector3.zero;
            carRB.angularVelocity = Vector3.zero; // Stop rotating if there's any residual torque
        }

        if (_input.MenuInput)
        {
            SceneManager.LoadScene("WHA_MainMenu");
        }
    }

    private void GroundCheck()
    {
        // Raycast downwards to check if the car is grounded
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, iceLayer))
        {
            isOnIce = true;
        }
        else
        {
            isOnIce = false;
        }
    }

    // Adjust the sound cooldown timer and play sound only after cooldown
    private void HandleMovement()
    {
        // Forward and backward movement
        if (isGrounded || isOnIce) // Ensure movement only happens when grounded
        {
            Vector3 moveDirection = transform.forward * moveSpeed * _input.NormalizedMovementInput.y;
            carRB.AddForce(moveDirection, ForceMode.Acceleration);

            // Check if the car is moving forward
            if (_input.NormalizedMovementInput.y > 0)
            {
<<<<<<< Updated upstream
                FMODUnity.RuntimeManager.PlayOneShot("event:/RacersRidgeGame/Car Engine"); // Play SFX through FMOD once
=======
                
                
>>>>>>> Stashed changes
                // Play engine sound immediately if not already playing
                //if (!soundMaker.isPlaying)
                //{
                //    soundMaker.PlayOneShot(carRunningSound);
                //}
            }

            // Steering
            float steerInput = _input.NormalizedMovementInput.x;
            if (_input.NormalizedMovementInput.y != 0)
            {
                Vector3 steerTorque = Vector3.up * steerInput * steerAngle;
                steerTorque.z = 0;  // Explicitly set Z-axis torque to zero
                carRB.AddTorque(steerTorque, ForceMode.VelocityChange);

                // Play tire screech sound when turning sharply
                if (Mathf.Abs(steerInput) > 0.5f) // Only trigger if the turn is significant
                {
                    if (soundCooldownTimer <= 0f) // Check if cooldown has expired
                    {
                        soundCooldownTimer = soundCooldownDuration; // Reset cooldown timer
                        StartCoroutine(PlayTireScreechSoundOnce());
                    }
                }
            }

            // Rotate the front wheels smoothly
            RotateFrontWheelsSmoothly(steerInput);

            // Limit max speed
            Vector3 velocity = carRB.linearVelocity;
            if (velocity.magnitude > maxSpeed)
            {
                carRB.linearVelocity = velocity.normalized * maxSpeed;
            } 
            CarEngnie.setParameterByName("RPM", velocity / 50f);
        }

        // Decrease cooldown timer each frame
        if (soundCooldownTimer > 0f)
        {
            soundCooldownTimer -= Time.deltaTime;
        }
    }


    #region CarSounds

    // Improved Tire screech sound with cooldown
    IEnumerator PlayTireScreechSoundOnce()
    {
        // Play tire screech sound
        soundMakerTwo.PlayOneShot(tireScreechSound);

        // Wait for the cooldown duration
        yield return new WaitForSeconds(soundCooldownDuration);
    }


    #endregion

    #region TurnIntoAIWhenRaceOver

    private void MoveTowardsWaypoint(Transform targetWaypoint)
    {
        // Calculate direction to the waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Rotate towards the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // Move forward
        Vector3 forwardMovement = transform.forward * maxSpeed * Time.fixedDeltaTime;
        carRB.MovePosition(carRB.position + forwardMovement);

        // Rotate front wheels
        RotateFrontWheels(direction);
    }

    private void RotateFrontWheels(Vector3 direction)
    {
        // Calculate steering angle for the front wheels
        float steerAngle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        steerAngle = Mathf.Clamp(steerAngle, -wheelTurnAngle, wheelTurnAngle);

        // Apply steering to the front wheels
        Quaternion frontWheelRotation = Quaternion.Euler(0, steerAngle, 0);
        frontLeftWheel.localRotation = Quaternion.Lerp(frontLeftWheel.localRotation, frontWheelRotation, Time.fixedDeltaTime * rotationSpeed);
        frontRightWheel.localRotation = Quaternion.Lerp(frontRightWheel.localRotation, frontWheelRotation, Time.fixedDeltaTime * rotationSpeed);

        // Spin the front wheels to simulate movement
        float spinAmount = maxSpeed * Time.fixedDeltaTime * wheelSpinSpeed / 60f;
        frontLeftWheel.Rotate(spinAmount, 0, 0, Space.Self);
        frontRightWheel.Rotate(spinAmount, 0, 0, Space.Self);
    }

    #endregion

    private void RotateFrontWheelsSmoothly(float steerInput)
    {
        // Calculate the target angle based on input
        float targetSteerAngle = steerInput * maxWheelSteerAngle;

        // Smoothly interpolate the current angle to the target angle
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, steerSmoothSpeed * Time.fixedDeltaTime);

        // Apply the rotation to the front wheels
        Quaternion leftWheelRotation = Quaternion.Euler(0, currentSteerAngle, 0);
        Quaternion rightWheelRotation = Quaternion.Euler(0, currentSteerAngle, 0);

        frontLeftWheel.localRotation = leftWheelRotation;
        frontRightWheel.localRotation = rightWheelRotation;
    }

    private void ApplyTraction()
    {
        if (isOnIce)
        {
            dragStrength = 0f;
        }

        if (isGrounded)
        {
            dragStrength = 0.98f;

            // Apply stronger drag when grounded and moving slowly
            Vector3 localVelocity = transform.InverseTransformDirection(carRB.linearVelocity);
            localVelocity.x *= dragStrength;

            // Apply more drag to slow down the car completely
            if (carRB.linearVelocity.magnitude < 0.1f) // Stronger drag when near stop
            {
                localVelocity.x *= 1.5f; // Stronger drag multiplier when near stop
            }

            carRB.linearVelocity = transform.TransformDirection(localVelocity);
        }
    }

    private void ApplyCustomGravity()
    {
        // Apply normal gravity at all times
        carRB.AddForce(Vector3.down * Physics.gravity.magnitude, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(SlowCarDown());
        }

        // This triggers the car collision sound effect
        if (other.gameObject.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/RacersRidgeGame/Car Collision");
        }
    }

    IEnumerator SlowCarDown()
    {
        maxSpeed -= 2;

        yield return new WaitForSeconds(2);

        maxSpeed += 2;
    }
}