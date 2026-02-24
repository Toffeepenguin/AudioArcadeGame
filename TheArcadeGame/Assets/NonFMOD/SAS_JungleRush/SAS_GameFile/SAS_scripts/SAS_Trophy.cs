using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SAS_TrophyManager : MonoBehaviour
{
    public static bool hasPlayerWon;

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerWon)
        {
        }
    }

    public void playerWinSet(bool temp)
    {
        hasPlayerWon = temp;
    }
}