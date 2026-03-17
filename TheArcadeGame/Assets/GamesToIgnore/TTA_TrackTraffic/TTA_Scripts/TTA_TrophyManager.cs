using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTA_TrophyManager : MonoBehaviour
{
    public static bool hasPlayerWon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPlayerWon)
        {
            if (PlayerPrefs.GetInt("TTA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("TTA_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }

    public void SetPlayerWin(bool temp)
    {
        hasPlayerWon = temp;
    }
}
