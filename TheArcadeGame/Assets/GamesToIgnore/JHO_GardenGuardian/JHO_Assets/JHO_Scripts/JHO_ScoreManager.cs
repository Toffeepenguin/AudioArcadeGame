using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JHO_ScoreManager : MonoBehaviour
{

    public static JHO_ScoreManager Instance;

    public Text JHO_ScoreText;
    public Text JHO_HighscoreText;

    public Animator JHO_ani;

    int JHO_currentScore = 0;
    float JHO_Highscore = 0;
    [SerializeField] public Timer timer;

    bool JHO_GardenGuardianTrophy = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

        JHO_Highscore = PlayerPrefs.GetFloat("HighScore", 0);
        UpdateHighScoreText();
        JHO_ScoreText.text = "Score: " + JHO_currentScore.ToString();
        //JHO_HighscoreText.text = "HighScore: " + FormatTime(JHO_Highscore);
    }

    public void AddScore()
    {
        JHO_currentScore += 2;
        JHO_ScoreText.text = "Score: " + JHO_currentScore.ToString();
        Debug.Log("AddScore called. Current score: " + JHO_currentScore);
        JHO_ani.SetInteger("JHO_Score", JHO_currentScore);

        if (JHO_Highscore < JHO_currentScore)
        {
            PlayerPrefs.SetInt("HighScore", JHO_currentScore);
        }

        if (JHO_ani == null)
        {
            Debug.LogError("Animator not assigned! Please assign it in the Inspector.");
            return;
        }

        if (JHO_currentScore >= 60)
        {
           float remainingTime = timer.JHO_timeValue;

                // Save the remaining time as the high score if it's higher
            if (remainingTime > JHO_Highscore)
            {
                JHO_Highscore = remainingTime;
                PlayerPrefs.SetFloat("HighScore", JHO_Highscore);
                PlayerPrefs.Save(); // Ensure the data is written to disk
            }

            JHO_GardenGuardianTrophy = true;
            SceneManager.LoadScene("JHO_VictroyScene");

            if (PlayerPrefs.GetInt("JHO_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("JHO_Trophie_Int", 1);
                PlayerPrefs.Save();
                Debug.Log("JHO Trophy Win");
            }
        }
    }

    public void LoseScore()
    {
        JHO_currentScore -= 2;
        JHO_ScoreText.text = "Score: " + JHO_currentScore.ToString();
        JHO_ani.SetInteger("JHO_Score", JHO_currentScore);
    }

    public int GetScore()
    {
        return JHO_currentScore;
    }

    private void UpdateHighScoreText()
    {
        // Format the high score as mm:ss
        int minutes = Mathf.FloorToInt(JHO_Highscore / 60);
        int seconds = Mathf.FloorToInt(JHO_Highscore % 60);

        JHO_HighscoreText.text = $"HighScore: {minutes:00}:{seconds:00}";
    }
}
