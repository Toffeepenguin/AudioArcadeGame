using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SAN_ScoreManager : MonoBehaviour
{

    public TMP_Text ScoreText;
    public int Score = 0;
    public static bool WinCondition = false;
    private SAN_TrophyManager TrophyManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreUI();
        TrophyManager = GetComponent<SAN_TrophyManager>();
    }

    public void AddScore(int points)
    {
        Score += points;
        UpdateScoreUI();
    }

    public void Update()
    {
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        /*Score++;*/
        ScoreText.text = "Score: " + Score.ToString();
        Debug.Log("Current Score: " + Score);

        if (Score > 500000)
        {
            WinCondition = true;
            Debug.Log("Win Condition: " + WinCondition);

            TrophyManager.playerWinSet(true);
        }
    }
}
