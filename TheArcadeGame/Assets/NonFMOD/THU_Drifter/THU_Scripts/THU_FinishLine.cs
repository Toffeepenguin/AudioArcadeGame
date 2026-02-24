using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THU_FinishLine : MonoBehaviour
{
    public THU_Drifter_UIManager Drifter_UIManager;
    private int playerCrossings = 0;
    private int enemyCrossings = 0;

    private int winThreshold = 3;
    private int loseThreshold = 3;

    private float lastPlayerCrossTime = -1f;
    private float lastEnemyCrossTime = -1f;
    public float finishLineCooldown = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "THU_Player")
        {
            if (Time.time - lastPlayerCrossTime >= finishLineCooldown)
            {
                lastPlayerCrossTime = Time.time;
                playerCrossings++;
                Debug.Log("Player has crossed the finish line " + playerCrossings + " times.");

                Drifter_UIManager.OnLapCompleted();

                if (playerCrossings >= winThreshold)
                {
                    Drifter_UIManager.WinScreen();
                    Debug.Log("Player wins!");

                    if (PlayerPrefs.GetInt("THU_Trophie_Int") != 1)
                    {
                        PlayerPrefs.SetInt("THU_Trophie_Int", 1);
                        PlayerPrefs.Save();
                    }
                }
            }
            else
            {
                Debug.Log("Duplicate player crossing ignored due to cooldown.");
            }
        }
        else if (other.gameObject.name == "THU_Enemy")
        {
            if (Time.time - lastEnemyCrossTime >= finishLineCooldown)
            {
                lastEnemyCrossTime = Time.time;
                enemyCrossings++;
                Debug.Log("Enemy has crossed the finish line " + enemyCrossings + " times.");

                if (enemyCrossings >= loseThreshold)
                {
                    Drifter_UIManager.LoseScreen();
                    Debug.Log("Enemy wins!");
                }
            }
            else
            {
                Debug.Log("Duplicate enemy crossing ignored due to cooldown.");
            }
        }
    }
}
