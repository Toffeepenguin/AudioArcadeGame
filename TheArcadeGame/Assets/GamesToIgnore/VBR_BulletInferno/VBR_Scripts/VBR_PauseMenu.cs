using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VBR_PauseMenu : MonoBehaviour
{
    private GameObject pauseMenu;
    private InputSubscription _Input;

    private bool MenuPressed;
    public bool isPaused;

    [SerializeField] private EventSystem _EventSystem;
    [SerializeField] private GameObject StartingButton;
    private void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        pauseMenu.SetActive(false);
    }
    private void FixedUpdate()
    {
        MenuPressed = _Input.MenuInput;
        if (MenuPressed)
        {
            if (isPaused)
            {
                resumeGame();
            }
            else 
            {
                pauseGame();
            }
        }
    }
    private void pauseGame()
    {
        pauseMenu.SetActive(true);
        SetFirstSelected(StartingButton);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void resumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToTitleCard()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VBR_TitleScreen");
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VBR_NewGameScene");
    }

    private void SetFirstSelected(GameObject button)
    {
        _EventSystem.SetSelectedGameObject(null);
        _EventSystem.firstSelectedGameObject = button;
        _EventSystem.SetSelectedGameObject(button);
    }
}
