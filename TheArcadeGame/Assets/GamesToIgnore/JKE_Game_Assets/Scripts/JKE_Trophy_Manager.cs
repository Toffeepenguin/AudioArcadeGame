using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JKE_Trophy_Manager : MonoBehaviour
{
    public static bool PlayerWon = false;

    void Update()
    {
        if (PlayerWon)
        {
            if (PlayerPrefs.GetInt("JKE_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("JKE_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }

    public void playerWinSet(bool temp)
    {
        PlayerWon = temp;
    }
}