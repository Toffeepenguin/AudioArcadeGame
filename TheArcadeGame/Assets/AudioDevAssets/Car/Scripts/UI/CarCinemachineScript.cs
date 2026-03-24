using UnityEngine;
using Unity.Cinemachine;

// This attribute allows the script to live ON the Virtual Camera
[SaveDuringPlay]
[AddComponentMenu("")]
public class CarCinemachineObject : CinemachineExtension
{
    [Header("Targets")]
    [SerializeField] private Transform target;
    [SerializeField] private PhysicsCarObject car;

    [Header("Positioning")]
    public Vector3 offset;
    public Vector3 rotation_offset;
    public float position_smooth_time_long;
    public float position_smooth_time_lat;
    public float rotation_smooth_speed;

    [Header("FOV")]
    public float min_fov;
    public float fov_multiplier;
    public float look_sensitivity;

    private Vector3 current_lat_velocity;
    private Vector3 current_long_velocity;
    private float current_yaw;
    private Rigidbody target_rigidbody;
    public InputObject local_input;

    protected override void Awake()
    {
        base.Awake();
        if (target) target_rigidbody = target.GetComponent<Rigidbody>();
    }

    // This is the magic method that Cinemachine calls to position the camera
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!target || !target_rigidbody || stage != CinemachineCore.Stage.Body) return;

        // 1. Handle Looking logic
        if (local_input.look.sqrMagnitude > 0.1f)
        {
            float target_yaw = Mathf.Atan2(local_input.look.x, local_input.look.y) * Mathf.Rad2Deg;
            current_yaw = Mathf.LerpAngle(current_yaw, target_yaw, look_sensitivity * deltaTime);
        }
        else
        {
            current_yaw = Mathf.LerpAngle(current_yaw, 0, look_sensitivity * deltaTime);
        }

        // 2. Position Logic
        Vector3 rotated_offset = Quaternion.Euler(0, current_yaw, 0) * offset;
        Vector3 local_cam_pos = target.InverseTransformPoint(state.RawPosition);

        float smooth_x = Mathf.SmoothDamp(local_cam_pos.x, rotated_offset.x, ref current_lat_velocity.x, position_smooth_time_lat);
        float smooth_y = Mathf.SmoothDamp(local_cam_pos.y, rotated_offset.y, ref current_lat_velocity.y, position_smooth_time_lat);
        float smooth_z = Mathf.SmoothDamp(local_cam_pos.z, rotated_offset.z, ref current_long_velocity.z, position_smooth_time_long);

        state.RawPosition = target.TransformPoint(new Vector3(smooth_x, smooth_y, smooth_z));

        // 3. Rotation Logic
        Vector3 direction = target.position + target.up - state.RawPosition;
        if (direction != Vector3.zero)
        {
            Quaternion final_target_rot = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(rotation_offset);
            state.RawOrientation = Quaternion.Slerp(state.RawOrientation, final_target_rot, rotation_smooth_speed * deltaTime);
        }

        // 4. FOV Logic
        state.Lens.FieldOfView = Mathf.Lerp(state.Lens.FieldOfView, min_fov + (target_rigidbody.linearVelocity.magnitude * fov_multiplier), fov_multiplier * deltaTime);
    }
}