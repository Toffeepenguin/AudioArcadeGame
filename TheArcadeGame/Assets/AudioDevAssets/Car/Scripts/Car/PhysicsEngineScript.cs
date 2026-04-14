using UnityEngine;

public class PhysicsEngineObject : MonoBehaviour
{
    private Rigidbody car_rigidbody;
    public PhysicsCarObject car;

    [Header("Engine")]
    public float engine_rpm;
    public float idle_rpm;
    public float peak_rpm;
    public AnimationCurve horsepower_curve;
    public float max_horsepower;
    public float max_engine_braking_torque;

    [Header("Transmission")]
    public bool automatic;
    public float[] gears;
    public int gear;
    public float final_drive;
    public float clutch_bite;
    public float transmission_loss;
    public float shift_delay;
    private float last_shift_time;
    [Range(0f, 1f)] public float upshift_threshold;
    [Range(0f, 1f)] public float downshift_threshold;

    [Header("State")]
    public float given_wheel_rpm;
    public float wheel_rpm;
    public bool last_shift_up;
    public bool last_shift_down;

    [Header("Vehicle Speed")]
    public float mph;

    public InputObject local_input;

    private void Awake()
    {
        car_rigidbody = GetComponent<Rigidbody>();
        engine_rpm = idle_rpm;
    }

    private void FixedUpdate()
    {
        mph = car_rigidbody.linearVelocity.magnitude * 2.23694f;
        UpdateRPM();
        HandleShifting();
    }

    private void UpdateRPM()
    {
        if (local_input.clutch > clutch_bite) engine_rpm = Mathf.Lerp(engine_rpm, Mathf.Lerp(idle_rpm, peak_rpm, local_input.throttle), Time.fixedDeltaTime * 5f);
        else engine_rpm = Mathf.Lerp(engine_rpm, Mathf.Abs(given_wheel_rpm * gears[gear] * final_drive), Time.fixedDeltaTime * 10f);
        engine_rpm = Mathf.Clamp(engine_rpm, idle_rpm, peak_rpm);
    }

    private void HandleShifting()
    {
        if (!automatic)
        {
            bool up_pressed = local_input.shift_up && !last_shift_up;
            bool down_pressed = local_input.shift_down && !last_shift_down;
            if (up_pressed && gear < gears.Length - 1)
            {
                PerformShift(gear + 1);
            }
            if (down_pressed && gear > 0)
            {
                PerformShift(gear - 1);
            }
        }
        else
        {
            if (Time.time - last_shift_time < shift_delay) return;
            float rpm_range = peak_rpm - idle_rpm;
            if (engine_rpm > idle_rpm + (rpm_range * upshift_threshold) && gear < gears.Length - 1) PerformShift(gear + 1);
            else if (engine_rpm < idle_rpm + (rpm_range * downshift_threshold) && gear > 1) PerformShift(gear - 1);
        }
        last_shift_up = local_input.shift_up;
        last_shift_down = local_input.shift_down;
    }

    private void PerformShift(int new_gear)
    {
        gear = new_gear;
        last_shift_time = Time.time;
    }

    public float GetDriveTorque()
    {
        float engine_horsepower = horsepower_curve.Evaluate(engine_rpm / peak_rpm) * max_horsepower;
        float engine_torque = (engine_horsepower * 5252f / Mathf.Max(engine_rpm, idle_rpm)) * 1.356f;
        float transmission_torque = engine_torque * gears[gear] * final_drive * transmission_loss;
        return transmission_torque * local_input.throttle * local_input.clutch;
    }

    public float GetEngineBrakingTorque()
    {
        if (1f - local_input.clutch < clutch_bite) return 0f;
        return (engine_rpm / peak_rpm) * (engine_rpm / peak_rpm) * max_engine_braking_torque;
    }
}
