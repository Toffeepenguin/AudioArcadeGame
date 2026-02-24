using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;  

public class MRO_UI : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject WinScreen;
    public GameObject pauseMenu;
    public GameObject startMenu;
    private bool isPaused;
    private bool isGameStarted;

    public GameObject StartPlayButton;
    public GameObject StartQuitButton;
    public GameObject PauseResumeButton;
    public GameObject PauseQuitButton;
    public GameObject GameOverQuitButton;
    public GameObject GameOverResetButton;

    
    public TMP_Text powerUpsCollectedText; 

    void Start()
    {
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);
        isPaused = true;
        isGameStarted = false;
        EventSystem.current.SetSelectedGameObject(StartPlayButton);
        Invoke("stopTime", 0.05f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStarted)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (isGameStarted && !isPaused)
        {
            Time.timeScale = 1f;
        }
    }

    void stopTime()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isGameStarted = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);

        isPaused = true;
        isGameStarted = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);

        isPaused = true;
        EventSystem.current.SetSelectedGameObject(PauseResumeButton);
        Invoke("stopTime", 0.1f);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startMenu.SetActive(false);
        pauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isGameStarted = true;
    }

    public void GameOver(int powerUpsCollected)
    {
        
        powerUpsCollectedText.text = "Power-Ups Collected: " + powerUpsCollected.ToString();
        
        GameOverScreen?.SetActive(true);

        isPaused = true;
        isGameStarted = false;
        EventSystem.current.SetSelectedGameObject(GameOverResetButton);
        Invoke("stopTime", 0.1f);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        isPaused = true;
        isGameStarted = false;

        if (PlayerPrefs.GetInt("MRO_Trophie_Int") != 1)
        {
            PlayerPrefs.SetInt("MRO_Trophie_Int", 1);
            PlayerPrefs.Save();
            Debug.Log("Trophy Awarded: 10 Power-Ups Collected!");
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
