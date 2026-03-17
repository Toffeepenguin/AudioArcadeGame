using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATA_LvlBoundary : MonoBehaviour
{
    public static float leftSide = -7f;
    public static float rightSide = 7f;
    // see in the menue
    public float internalLeft;
    public float internalRight;

    // Update is called once per frame
    void Update()
    {
        internalLeft = leftSide;
        internalRight = rightSide;
    }
}