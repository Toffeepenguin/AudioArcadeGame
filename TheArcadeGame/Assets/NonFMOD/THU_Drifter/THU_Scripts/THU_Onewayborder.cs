using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THU_Onewayborder : MonoBehaviour
{
    public Vector2 correctDirection = Vector2.up;
    public bool isOneWay = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
        {
            Vector2 velocity = other.attachedRigidbody.linearVelocity;
            float dotProduct = Vector2.Dot(velocity.normalized, correctDirection.normalized);

            if (isOneWay && dotProduct < 0)
            {
                if (velocity.y < 0)
                {
                    velocity.y = 0;
                    other.attachedRigidbody.linearVelocity = velocity;

                    Vector2 newPosition = other.transform.position;
                    newPosition.y = Mathf.Max(newPosition.y, transform.position.y);
                    other.transform.position = newPosition;
                }
            }
        }
    }
}
