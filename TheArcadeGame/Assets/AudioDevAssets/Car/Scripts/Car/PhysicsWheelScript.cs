using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PhysicsWheelObject : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody car_rigidbody;
    private PhysicsEngineObject engine;
    private PhysicsCarObject car;

    public Transform wheel_pivot;
    [SerializeField] private float wheel_radius;
    [SerializeField] private LayerMask surface_mask;

    public bool grounded;
    [SerializeField] private RaycastHit hit;

    [Header("Suspension")]
    [SerializeField] private float susp_rest_distance;
    [SerializeField] private float susp_stiffness;
    [SerializeField] private float damp_stiffness;
    [SerializeField] private Vector3 suspension_force;

    public float susp_offset;
    private float last_hit_distance;
    [SerializeField] private float spring_force;

    [Header("Steering")]
    [SerializeField] private bool steerable;
    [SerializeField] private float max_steering_angle;
    [SerializeField] private AnimationCurve tyre_grip_curve;
    [SerializeField] private float wheel_mass;

    [Header("Driving")]
    [SerializeField] private bool driveable;
    [SerializeField] private float tyre_friction;
    [SerializeField] private float lateral_force_coefficient;
    [SerializeField] AnimationCurve longitudinal_slip_curve;
    [SerializeField] private float grip_level;

    [Header("Braking")]
    [SerializeField] private float braking_force;
    [SerializeField] private float handbrake_force;

    [Header("Visuals")]
    [SerializeField] private Transform wheel_mesh;
    [SerializeField] private float wheel_rotation;

    [Header("Tyre")]
    [SerializeField] private float rolling_resistance;
    [SerializeField] private float lateral_rolling_resistance_coefficient;
    [SerializeField] private float angular_velocity;
    [SerializeField] private Vector3 tyre_force;
    [SerializeField] private Vector3 point_velocity;
    [SerializeField] private float slip_ratio;
    [SerializeField] private float curve;

    [Header("Speed Sensitivity")]
    [SerializeField] private float high_speed_threshold;
    [SerializeField] private float high_speed_steer_factor;

    [SerializeField] private InputObject local_input;

    private void Awake()
    {
        if (local_input == null)
        {
            Debug.LogError($"[Wheel {gameObject.name}] Could not find InputObject on parent! Steering will not work.", this);
        }
        else
        {
            Debug.Log($"[Wheel {gameObject.name}] Successfully linked to InputObject on {local_input.gameObject.name}", this);
        }
        car_rigidbody = transform.parent.GetComponent<Rigidbody>();
        engine = car_rigidbody.GetComponent<PhysicsEngineObject>();
        car = car_rigidbody.GetComponent<PhysicsCarObject>();  
        angular_velocity = 0f;
    }

    private void FixedUpdate()
    {
        grounded = DoRaycast();
        UpdateSteering();
        point_velocity = car_rigidbody.GetPointVelocity(transform.position);
        Vector3 total_force = Vector3.zero;
        if (driveable) engine.given_wheel_rpm = car_rigidbody.linearVelocity.magnitude / (2 * Mathf.PI * wheel_radius) * 60f;
        angular_velocity = car_rigidbody.linearVelocity.magnitude / (2 * Mathf.PI * wheel_radius);
        if (grounded)
        {
            suspension_force = GetSuspensionForce();
            tyre_force = GetTireForce();
            total_force = suspension_force + tyre_force;
        }
        else
        {
            susp_offset = 0f;
            spring_force = 0f;
            suspension_force = Vector3.zero;
            tyre_force = Vector3.zero;
        }
        car_rigidbody.AddForceAtPosition(total_force, transform.position);
    }

    private void LateUpdate()
    {
        UpdateWheelMesh();
    }

    public Vector3 GetSuspensionForce()
    {
        susp_offset = Mathf.Max(0f, susp_rest_distance - (hit.distance - wheel_radius));
        spring_force = (susp_offset * susp_stiffness) - (Vector3.Dot(hit.normal, point_velocity) * damp_stiffness);
        return car_rigidbody.transform.up * spring_force;
    }

    public Vector3 GetTireForce()
    {
        float forward_velocity = Vector3.Dot(point_velocity, wheel_pivot.forward);
        float lateral_velocity = Vector3.Dot(point_velocity, wheel_pivot.right);
        float spring_load = Mathf.Max(0f, spring_force);
        float max_grip = spring_load * tyre_friction * grip_level;
        float lateral_torque = -lateral_velocity * lateral_force_coefficient * spring_load;
        float driving_torque = driveable ? (engine.GetDriveTorque() / wheel_radius) : 0f;
        float rolling_torque = Mathf.Lerp(rolling_resistance * Mathf.Sign(forward_velocity), forward_velocity * rolling_resistance, forward_velocity);
        float braking_torque = (local_input.brake * braking_force + (local_input.handbrake ? handbrake_force : 0f)) * -Mathf.Sign(forward_velocity);
        float forward_torque = driving_torque + braking_torque - rolling_torque;

        Vector3 total_force = (wheel_pivot.forward * forward_torque) + (wheel_pivot.right * lateral_torque);
        if (total_force.magnitude > max_grip) total_force = total_force.normalized * max_grip;

        return Vector3.ProjectOnPlane(total_force, hit.normal);
    }

    private bool DoRaycast()
    {
        float rayLength = susp_rest_distance + wheel_radius;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, rayLength, surface_mask))
        {
            grip_level = GameUtilitiesObject.GripSurface(hit.transform.gameObject.layer);
            return true;
        }
        return false;
    }

    private void UpdateSteering()
    {
        if (steerable)
        {
            float current_max_angle = Mathf.Lerp(max_steering_angle, max_steering_angle * high_speed_steer_factor, 
                Mathf.Clamp01(car_rigidbody.linearVelocity.magnitude / high_speed_threshold));
                // CURRENT MAX ANGLE IS 0
            Quaternion targetRotation = Quaternion.Euler(0f, local_input.steering.x * current_max_angle, 0f);
            if (float.IsNaN(targetRotation.x) || float.IsNaN(targetRotation.y)) {
                Debug.LogError($"Steering calculation failed! Check your input vectors: {local_input.steering}: {car_rigidbody.linearVelocity.magnitude}");
                return;
            }
            Debug.Log($"{current_max_angle} : {targetRotation} : {local_input.steering}");
            wheel_pivot.localRotation = targetRotation;
        }
    }

    private void UpdateWheelMesh()
    {
        if (!wheel_mesh) return;
        Vector3 local_pos = wheel_mesh.localPosition;
        local_pos.y = -(susp_rest_distance - susp_offset);
        wheel_rotation += angular_velocity * Mathf.Rad2Deg * Time.fixedDeltaTime;
        wheel_mesh.SetLocalPositionAndRotation(local_pos, Quaternion.Euler(wheel_rotation, 0f, 0f));
    }

    public void ResetWheel()
    {
        angular_velocity = 0f;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 origin = transform.position;

        Vector3 current_velocity = car_rigidbody.GetPointVelocity(origin);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(origin, origin + current_velocity * 0.1f); // Scaled by 0.1 for visibility

        if (!grounded) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + suspension_force * 0.0005f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + tyre_force * 0.0005f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + (suspension_force + tyre_force) * 0.0005f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(car_rigidbody.worldCenterOfMass, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(origin, origin + transform.up * 0.5f);
    }
}
