using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QME_YouLose : MonoBehaviour
{
    public GameObject loseScreen; // Reference to the GameObject that represents the "Lose Screen"
    private InputSubscription _inputSubscription; // Reference to the InputSubscription component

    private void Start()
    {
        // Find the InputSubscription component in the scene and assign it to _inputSubscription
        _inputSubscription = FindObjectOfType<InputSubscription>();
    }

    private void Update()
    {
        // Continuously check if the _inputSubscription is available and if the EInput flag is active
        // If true, trigger the game restart process
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

    public void ShowLoseScreen()
    {
        // Display the "Lose Screen" by enabling the loseScreen GameObject
        loseScreen.SetActive(true);

        // Pause the game's time scale to stop all in-game actions
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Resume the game's time scale to continue normal gameplay
        Time.timeScale = 1f;

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
