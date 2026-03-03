using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAS_checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject[] checkpoint;
    private int currentcheckpointindex = 0;
    [SerializeField] private float speed = 2f;

    
    void Update()
    {
        if (Vector2.Distance(checkpoint[currentcheckpointindex].transform.position, transform.position) < .1f)
        {
            currentcheckpointindex++;
            if (currentcheckpointindex >= checkpoint.Length)
            {
                currentcheckpointindex = 0;

            }
        }
        transform.position = Vector2.MoveTowards(transform.position, checkpoint[currentcheckpointindex].transform.position, Time.deltaTime * speed);
    }
}
