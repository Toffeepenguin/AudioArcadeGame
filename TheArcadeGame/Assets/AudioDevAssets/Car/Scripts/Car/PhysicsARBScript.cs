using UnityEngine;

public class PhysicsARBObject : MonoBehaviour
{
    [Header("Wheels")]
    [SerializeField] private PhysicsWheelObject left;
    [SerializeField] private PhysicsWheelObject right;

    [Header("ARB")]
    [SerializeField] private float arb_stiffness;
    private Rigidbody car_rigidbody;

    void Awake()
    {
        car_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyAntiRoll();
    }

    void ApplyAntiRoll()
    {
        float left_travel = left.grounded ? left.susp_offset : 0f;
        float right_travel = right.grounded ? right.susp_offset : 0f;
        float roll_force = (left_travel - right_travel) * arb_stiffness;
        if (left.grounded) car_rigidbody.AddForceAtPosition(left.wheel_pivot.up * roll_force, left.transform.position);
        if (right.grounded) car_rigidbody.AddForceAtPosition(right.wheel_pivot.up * -roll_force, right.transform.position);
    }
}