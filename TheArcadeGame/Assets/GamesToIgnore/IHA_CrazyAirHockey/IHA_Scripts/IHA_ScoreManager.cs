using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class IHA_ScoreManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private int playerScore;
    [SerializeField] private int enemyScore;
    private int consecutiveWins;
    [Header("UI")]
    [SerializeField] private Text playerScoreText;
    [SerializeField] private Text enemyScoreText;
    [SerializeField] private Text winText;

    [SerializeField] private GameObject MenuScreen;
    [SerializeField] private GameObject LevelOver;

    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private GameObject restartBtn;


    private bool gameFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;  //game starts paused on main menu  
    }

    // Update is called once per frame
    void Update()
    {
        //first player to 7 points wins
        if (playerScore >= 7 && !gameFinished)
        {
            consecutiveWins++;
            FinishGame("You won!");
            

        }
        else if (enemyScore >= 7 && !gameFinished)
        {
            consecutiveWins = 0; //no longer on win streak
            FinishGame("You lose!");
        }
    }

    public void incrementPlayerScore()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
    }

    public void incrementEnemyScore()
    {
        enemyScore++;
        enemyScoreText.text = enemyScore.ToString();
    }

    public void FinishGame(string finishMessage)
    {
        winText.gameObject.SetActive(true);
        winText.text = finishMessage;
        Time.timeScale = 0; //pause game
        LevelOver.SetActive(true);
        eventSystem.SetSelectedGameObject(restartBtn);
        gameFinished = true;
        //if player has won 10 games in a row
        if(consecutiveWins >= 10)
        {
            //pop trophy
            if (PlayerPrefs.GetInt("IHA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("IHA_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }
    //UI Controller

    public void OnPlay()
    {
        MenuScreen.SetActive(false);
        Time.timeScale = 1; //unpause game
    }

    public void OnQuit()
    {
        SceneManager.LoadScene(0);
    }

    public void OnRestart()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        OnPlay();
    }
}
