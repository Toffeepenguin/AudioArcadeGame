using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAN_TrophyManager : MonoBehaviour
{
    public static bool hasPlayerWon;
    // Update is called once per frame

    void Update()
    {
        if (hasPlayerWon)
        {
            if (PlayerPrefs.GetInt("SAN_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("SAN_Trophie_Int", 1);
                PlayerPrefs.Save();
            }

            Debug.Log("Trophy Won");
        }
    }

    public void playerWinSet(bool temp)
    {
        hasPlayerWon = temp;
    }
}

