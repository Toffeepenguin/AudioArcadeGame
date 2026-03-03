using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THU_RoadBorder : MonoBehaviour
{
    [SerializeField] private float slowdownFactor = 0.5f;
    [SerializeField] private float slowdownDuration = 2f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        THU_PlayerMovement playerMovement = collision.gameObject.GetComponent<THU_PlayerMovement>();

        if (playerMovement != null)
        {
            playerMovement.ReduceSpeedTemporarily(slowdownFactor, slowdownDuration);
        }
    }
}
