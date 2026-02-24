using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

public class WHA_CarAI : MonoBehaviour
{
    [Header("Setup Stuff")]
    private GameObject gameMan;
    private WHA_RaceManager raceMan;

    [Header("Waypoint Settings")]
    public WHA_CarAIPath path; // Reference to the path
    public float speed = 10f; // Movement speed
    public float speedUpSpeed = 10f;
    private float defaultSpeed;
    public float rotationSpeed = 5f; // Speed of turning towards the target node
    public float waypointTolerance = 1f; // Distance to consider reaching a waypoint

    [Header("Wheel Settings")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public float wheelTurnAngle = 30f; // Max steering angle for the front wheels
    public float wheelSpinSpeed = 360f; // Speed for wheel spinning effect

    [Header("Sound Settings")]
    private AudioSource soundMaker;
    public AudioClip carRunningSound;

    private List<Transform> waypoints; // List of waypoints
    private int currentWaypointIndex = 0; // Index of the current target waypoint

    private Rigidbody rb;

    private void Start()
    {
        gameMan = GameObject.FindGameObjectWithTag("GameController");
        raceMan = gameMan.GetComponent<WHA_RaceManager>();
        
        soundMaker = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

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

        defaultSpeed = speed;
    }

    private void Update()
    {
        if (raceMan.isPlayerInFirst)
        {
            StartCoroutine(SpeedUp());
        }
        else
        {
            speed = defaultSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (!raceMan.raceStarted)
            return;

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

    private void MoveTowardsWaypoint(Transform targetWaypoint)
    {
        // Calculate direction to the waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Rotate towards the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

        // Move forward
        Vector3 forwardMovement = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        if (rb.linearVelocity.magnitude > 0)
        {
            if (!soundMaker.isPlaying)
            {
                soundMaker.PlayOneShot(carRunningSound);
            }
        }

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
        float spinAmount = speed * Time.fixedDeltaTime * wheelSpinSpeed / 60f;
        frontLeftWheel.Rotate(spinAmount, 0, 0, Space.Self);
        frontRightWheel.Rotate(spinAmount, 0, 0, Space.Self);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(SlowCarDown());
        }
    }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(3);

        speed = speedUpSpeed;
    }

    IEnumerator SlowCarDown()
    {
        speed -= 2;

        yield return new WaitForSeconds(2);

        speed += 2;
    }
}
