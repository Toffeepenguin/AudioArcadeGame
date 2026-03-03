using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RRG_Game_Over : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject pause;

    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject _gameOverButton;
    [SerializeField] private GameObject _pauseButton;
    private bool clickButton;
    public bool clickResume;

    // Start is called before the first frame update
    void Start()
    {
        clickButton = false;

        gameOver.SetActive(false);
        pause.SetActive(false);

        
    }
    private void Update()
    {
        if (clickButton)
        {
            SceneManager.LoadScene("RRG_Pogo_Man_Scene");
        }
    }

    public void GameOverScreen(bool gameOverBool)
    {
        if (gameOverBool)
        {
            SetFIrstSelected(_gameOverButton);
            gameOver.SetActive(true);
        }
    }
    public void pauseScreen(bool pauseScreenBool)
    {
        if (pauseScreenBool)
        {
            
            SetFIrstSelected(_pauseButton);
            pause.SetActive(true);
            Time.timeScale = 0;
        }
        else 
        {
           
            pause.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void RestartButton()
    {
        clickButton = true;
    }
    //calls when player clicks button, quits game
    public void QuitButton()
    {
        SceneManager.LoadScene("VAG_VirtualArcadeScene");
    }
    private void  SetFIrstSelected(GameObject button)
    {
        _eventSystem.SetSelectedGameObject(null);
        _eventSystem.firstSelectedGameObject = button;
        _eventSystem.SetSelectedGameObject(button);
    }
}
