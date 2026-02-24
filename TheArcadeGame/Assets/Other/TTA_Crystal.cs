using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTA_Crystal : MonoBehaviour
{
    public TTA_WinCondition winScript;
    public TTA_TrophyManager trophyManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //
            // Win 
            //

            winScript.winMenu.SetActive(true);
            winScript.buttonsMenu.SetActive(true);
            trophyManager.SetPlayerWin(true);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            winScript.loseMenu.SetActive(true);
            winScript.buttonsMenu.SetActive(true);
        }
    }
}
