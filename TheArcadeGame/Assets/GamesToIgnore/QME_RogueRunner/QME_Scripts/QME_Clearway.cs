using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QME_Clearway : MonoBehaviour
{
    // Reference to the Transform of the previous room in the scene
    [SerializeField] private Transform previousRoom;

    // Reference to the Transform of the next room in the scene
    [SerializeField] private Transform nextRoom;

    // Reference to the camera control script used for moving the camera
    [SerializeField] private QME_CameraControl cam;

    // This method is called when another collider enters the trigger attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the player
        if (collision.tag == "Player")
        {
            // Determine which direction the player came from based on their position
            if (collision.transform.position.x < transform.position.x)
                // If the player is to the left of the trigger, move the camera to the next room
                cam.MoveToNewRoom(nextRoom);
            // Otherwise, move the camera to the previous room
            else cam.MoveToNewRoom(previousRoom);
        }
    }
}
