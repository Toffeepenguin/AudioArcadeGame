using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHA_KillBox : MonoBehaviour
{
    public WHA_RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raceManager.RespawnAtLastCheckpoint(other.gameObject);
        }
    }
}
