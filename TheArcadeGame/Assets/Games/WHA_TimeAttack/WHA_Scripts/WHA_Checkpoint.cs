using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to check racers are going through checkpoints

public class WHA_Checkpoint : MonoBehaviour
{
    public WHA_RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceManager.CheckpointTriggered(other.gameObject, transform);
        }
    }
}
