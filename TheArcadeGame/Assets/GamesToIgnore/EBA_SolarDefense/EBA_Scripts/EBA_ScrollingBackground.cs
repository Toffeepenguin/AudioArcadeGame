using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class EBA_ScrollingBackground : MonoBehaviour
{
    private float maxHeightY = 11.95f;
    public float speed;
    private float currentpoisitonY = 0;

    

    private void Update()
    {
        currentpoisitonY += speed;
        this.gameObject.transform.position = new Vector3(0, currentpoisitonY, 0);
        if (currentpoisitonY > maxHeightY)
        {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            currentpoisitonY = 0;
        }
       
    }
}
