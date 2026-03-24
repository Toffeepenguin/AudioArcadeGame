using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class InputObject : MonoBehaviour
{
    private CarInputActions controls;
    public string current_map;

    [Header("Throttle")]
    public float throttle;
    public float throttle_min;
    public float throttle_max;

    [Header("Brake")]
    public float brake;
    public float brake_min;
    public float brake_max;

    [Header("Sticks")]
    public Vector2 steering;
    public Vector2 look;

    [Header("Steering")]
    public float steering_rate;
    public float steering_min;
    public float steering_max;
    public float steering_linearity;

    [Header("Clutch")]
    public float clutch;
    public float clutch_min;
    public float clutch_max;

    [Header("Bools")]
    public bool handbrake;
    public bool shift_up;
    public bool shift_down;

    [Header("Gameplay")]
    public bool pause;
    public bool resume;

    private void Awake()
    {
        controls = new CarInputActions();
    }

    private void OnEnable() => EnableGameplay();
    private void OnDisable() => controls.Disable();

    public void EnableGameplay()
    {
        controls.UI.Disable();
        controls.Gameplay.Enable();
        current_map = "Gameplay";
    }

    public void EnableUI()
    {
        controls.Gameplay.Disable();
        controls.UI.Enable();
        current_map = "UI";
    }

    private void Update()
    {
        if (current_map == "Gameplay") UpdateGameplayInputs();
    }

    private void UpdateGameplayInputs()
    {
        throttle = SetTrigger(controls.Gameplay.Throttle.ReadValue<float>(), throttle_min, throttle_max);
        brake = SetTrigger(controls.Gameplay.Brake.ReadValue<float>(), brake_min, brake_max);
        steering = SetSteering();
        clutch = SetTrigger(controls.Gameplay.Clutch.ReadValue<float>(), clutch_min, clutch_max);
        handbrake = controls.Gameplay.Handbrake.ReadValue<float>() > .5f;
        shift_up = controls.Gameplay.GearUp.ReadValue<float>() > .5f;
        shift_down = controls.Gameplay.GearDown.ReadValue<float>() > .5f;
        look = controls.Gameplay.Look.ReadValue<Vector2>();
    }

    private Vector2 SetSteering()
    {
        Vector2 raw_input = controls.Gameplay.Steering.ReadValue<Vector2>();
        steering.x = Mathf.MoveTowards(
            steering.x,
            Mathf.Pow(Mathf.Abs(raw_input.x), steering_linearity) * Mathf.Sign(raw_input.x),
            steering_rate * Time.deltaTime
        );
        return new Vector2(steering.x, raw_input.y);
    }


    private float SetTrigger(float input, float min, float max)
    {
        return Mathf.InverseLerp(min, max, input);
    }
}
