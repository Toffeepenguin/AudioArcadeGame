using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WHA_TrophyManager : MonoBehaviour
{
    public static bool hasPlayerWon = false;

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerWon)
        {
            if (PlayerPrefs.GetInt("WHA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("WHA_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }

    public void playerWinSet(bool temp)
    {
        hasPlayerWon = temp;
    }
}
