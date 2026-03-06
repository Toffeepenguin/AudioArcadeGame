using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class QME_YouWin : MonoBehaviour
{
    // Reference to the win screen Canvas. Set this in the Unity Inspector.
    public GameObject winScreenCanvas;

    // Reference to the InputSubscription component to handle player inputs.
    private InputSubscription _inputSubscription;

    private QME_Gamemanager QME_GM;

    private void Start()
    {

        // Locate the InputSubscription component in the scene at the start of the game.
        _inputSubscription = FindObjectOfType<InputSubscription>();
        QME_GM = GetComponent<QME_Gamemanager>();
    }

    private void Update()
    {
        // Listen for the EInput action from the InputSubscription to restart the game.
        if (_inputSubscription != null && _inputSubscription.EInput)
        {
            RestartGame();
        }

        if (_inputSubscription.MenuInput)
        {
            Debug.Log("Shift key pressed. Transitioning to the next scene.");

            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object colliding has the "Finish" tag.
        // If so, display the win screen.
        if (collision.gameObject.CompareTag("Finish"))
        {
            ShowWinScreen();
            QME_GM.WonTrophy = true;


            if (PlayerPrefs.GetInt("QME_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("QME_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }

    private void ShowWinScreen()
    {
        // Show the win screen Canvas.
        winScreenCanvas.SetActive(true);

        // Pause the game by setting the time scale to 0.
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Resume normal game speed in case it was paused.
        Time.timeScale = 1f;

        // Reload the current scene to restart the game.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
