using System;
using TreeEditor;
using UnityEngine;
using static GameUtilitiesObject;

public class PhysicsCarObject : MonoBehaviour
{
    [Header("Car Settings")]
    private PhysicsWheelObject[] wheels;
    public LayerMask surface_layer;
    [HideInInspector] public Rigidbody rigid_body;
    public bool locked;
    private Vector3 default_position;
    private Quaternion default_rotation;

    private void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<PhysicsWheelObject>();
        default_position = transform.position;
        default_rotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        if (locked)
        {
            if (!rigid_body.isKinematic)
            {
                rigid_body.linearVelocity = Vector3.zero;
                rigid_body.angularVelocity = Vector3.zero;
                rigid_body.isKinematic = true;
            }
            return;
        }
        if (rigid_body.isKinematic) rigid_body.isKinematic = false;
    }

    public void ResetCar(Vector3? pos = null, Quaternion? rot = null)
    {
        Debug.Log("Resetting car");
        if (pos == null) locked = true;
        if (locked) rigid_body.isKinematic = true;
        Vector3 finalPos = pos ?? default_position;
        Quaternion finalRot = rot ?? default_rotation;
        if (Physics.Raycast(finalPos + (Vector3.up * 5f), Vector3.down, out RaycastHit hit, 30f, surface_layer))
        {
            finalPos = hit.point + (Vector3.up * .7f);
            finalRot = Quaternion.FromToRotation(Vector3.up, hit.normal) * finalRot;
        }
        rigid_body.linearVelocity = Vector3.zero;
        rigid_body.angularVelocity = Vector3.zero;
        foreach (PhysicsWheelObject wheel in wheels) wheel.ResetWheel();
        transform.SetPositionAndRotation(finalPos, finalRot);
    }
}
