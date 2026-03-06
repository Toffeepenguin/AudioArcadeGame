using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WHA_SnowboyObstacle : MonoBehaviour
{
    [Header("Spawning Settings")]
    public MeshRenderer meshRender;
    public BoxCollider boxCol;
    private ParticleSystem snowParticle;

    [Header("Waypoint Settings")]
    public WHA_CarAIPath path; // Reference to the path
    public float speed = 10f; // Movement speed
    public float rotationSpeed = 5f; // Speed of turning towards the target node
    public float waypointTolerance = 1f; // Distance to consider reaching a waypoint

    private List<Transform> waypoints; // List of waypoints
    private int currentWaypointIndex = 0; // Index of the current target waypoint

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        snowParticle = GetComponent<ParticleSystem>();
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0);

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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RespawnAtRandomWaypoint());
        }
    }

    private IEnumerator RespawnAtRandomWaypoint()
    {
        snowParticle.Play();
        if (meshRender) meshRender.enabled = false;
        if (boxCol) boxCol.enabled = false;

        // Wait for a few seconds
        yield return new WaitForSeconds(3f); // Adjust the delay as needed

        // Choose a random waypoint to respawn at
        int randomWaypointIndex = Random.Range(0, waypoints.Count);
        Transform respawnWaypoint = waypoints[randomWaypointIndex];

        // Reset position and re-enable components
        transform.position = respawnWaypoint.position;
        transform.rotation = Quaternion.identity; // Reset rotation if needed
        currentWaypointIndex = randomWaypointIndex; // Update the current waypoint index

        if (meshRender) meshRender.enabled = true;
        if (boxCol) boxCol.enabled = true;
        snowParticle.Stop();
    }
}
