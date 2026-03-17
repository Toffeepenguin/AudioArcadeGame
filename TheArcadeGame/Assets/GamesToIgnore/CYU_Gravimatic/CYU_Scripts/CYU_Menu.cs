using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CYU_Menu : MonoBehaviour
{
    public bool CYU_gravimaticTrophy = false;
    public Text CYU_finalScoreText;
    public Text CYU_trophyText;

    [SerializeField] AudioSource CYU_menuMusic;

    void Start()
    {
        // Checks if the active scene is the Game Over Screen
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        CYU_menuMusic.loop = true;
        CYU_menuMusic.volume = 0.1f;
        CYU_menuMusic.Play();

        if (sceneName == "CYU_GameOver")
        {
            CYU_trophyText.enabled = false;

            // If it is the Game Over screen, find the gamemanager object and get the score from it
            GameObject gameManagerObject = GameObject.Find("GameManager");
            CYU_GameManager gameManager = gameManagerObject.GetComponent<CYU_GameManager>();
            CYU_finalScoreText.text = gameManager.CYU_score.ToString();

            if (gameManager.CYU_score >= 2000)
            {
                if (PlayerPrefs.GetInt("CYU_Trophie_Int") != 1)
                {
                    PlayerPrefs.SetInt("CYU_Trophie_Int", 1);
                    PlayerPrefs.Save();
                }
                CYU_trophyText.enabled = true;
            }
        }
    }

    // Button to take the player to the main game
    public void CYU_PlayGame()
    {
        // Destroy the gamemanager object to free up memory (or else there will be multiple Gamemanagers from the DontDestroyOnLoad function!!)
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("CYU_Gravimatic");
    }

    // Button to take the player back to the virtual arcade
    public void CYU_ExitGame()
    {
        // Destroy the gamemanager object to free up memory (or else there will be multiple Gamemanagers from the DontDestroyOnLoad function!!)
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene(0);
    }

    // Button to take the player to the main menu
    public void CYU_MainMenu()
    {
        // Destroy the gamemanager object to free up memory (or else there will be multiple Gamemanagers from the DontDestroyOnLoad function!!)
        Destroy(GameObject.Find("GameManager"));
        SceneManager.LoadScene("CYU_MainMenu");
    }
}
