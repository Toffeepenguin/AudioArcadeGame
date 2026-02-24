using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_CarCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");

        // Check if the player collided with the car
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with car!");
            EBD_TimerScript timer = FindObjectOfType<EBD_TimerScript>();
            if (timer != null)
            {
                Debug.Log("Timer found. Deducting time...");
                timer.DeductTime(2f); // Deduct 2 seconds
            }
            else
            {
                Debug.LogError("TimerScript not found in the scene!");
            }
        }
    }
}
