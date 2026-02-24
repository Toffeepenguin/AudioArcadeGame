using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMA_ObstacleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float MaxDistance;

    private Vector3 leftPoint, rightPoint;

    private Vector3 currentTarget;
    private int moveDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        leftPoint = transform.position - new Vector3(MaxDistance, 0, 0);
        rightPoint = transform.position + new Vector3(MaxDistance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveDirection == 1)
        {
            currentTarget = rightPoint;
        }
        else if (moveDirection == -1)
        {
            currentTarget = leftPoint;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed = Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget) <= 0.01f)
        {
            moveDirection *= -1;
        }
    }
}
