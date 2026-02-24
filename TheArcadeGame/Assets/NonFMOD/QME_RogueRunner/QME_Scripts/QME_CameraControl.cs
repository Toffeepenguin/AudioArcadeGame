using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QME_CameraControl : MonoBehaviour
{
    // Speed of the camera's smooth movement (used for smoothing transitions).
    [SerializeField] private float speed;

    // The target X-position of the camera, used when transitioning to new areas.
    private float currentPosX;

    // Velocity used for smooth movement calculations.
    private Vector3 velocity = Vector3.zero;

    // Reference to the character the camera should follow.
    [SerializeField] private Transform Character;

    // Distance ahead of the character that the camera should look.
    [SerializeField] private float aheadDistance;

    // Speed at which the camera adjusts its "look ahead" position.
    [SerializeField] private float cameraSpeed;

    // The current "look ahead" offset based on the character's direction.
    private float lookAhead;

    private void Update()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        // Update the camera's position to follow the character directly on the X-axis,
        // while keeping its Y and Z positions unchanged.
        transform.position = new Vector3(Character.position.x, transform.position.y, transform.position.z);

        // Smoothly adjust the lookAhead value based on the character's direction and movement.
        // This creates a smoother transition when the character changes direction or speed.
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * Character.localScale.x), Time.deltaTime * cameraSpeed);

    }

    // Method to move the camera to a new room or area.
    // This sets the target X position of the camera to match the position of the new room.
    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;
    }
}
