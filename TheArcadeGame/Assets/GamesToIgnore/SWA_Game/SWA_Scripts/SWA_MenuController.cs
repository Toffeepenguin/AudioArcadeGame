using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SWA_MenuController : MonoBehaviour
{
    [SerializeField] SWA_Enemy enemy;

    InputSubscription cantInput;
    public GameObject Menu;
    public GameObject GameOverUI;
    public SWA_Player player;
    public SWA_Boss boss;
    public SWA_GameObjManager gameObjManager;
    public Text GameOverText;
    public bool isGameOver = false;

    private bool isMenuOpen = false;
    

    private void Start()
    {
        cantInput = GetComponent<InputSubscription>();
        isMenuOpen = false;
        isGameOver = false;
        Time.timeScale = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (cantInput.MenuInput && !isMenuOpen)
        {
            ToggleMenu();
        }
        else if (cantInput.MenuInput && isMenuOpen)
        {
            ContinueGame();
        }

        if (gameObjManager.isBossSpawned)
        {
            if (boss.currentHp == 0)
            {
                Debug.Log("This works");
                WinGame();
            }
        }
        

        // check isGameOver true
        if (isGameOver)
        {
            GameOverMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = true;
        Menu.SetActive(true);

        Time.timeScale = 0.0f;
    }

    public void ContinueGame()
    {
        isMenuOpen = false;
        Menu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    void GameOverMenu()
    {
        GameOverUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    void WinGame()
    {
        GameOverText.text = "You escape here!";
        GameOverUI.SetActive(true);

        if (PlayerPrefs.GetInt("SWA_Trophie_Int") != 1)
        {
            PlayerPrefs.SetInt("SWA_Trophie_Int", 1);
            PlayerPrefs.Save();
        }

        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        GameOverUI.SetActive(false);
        player.isAlive = true;
        Time.timeScale = 1f;

        SceneManager.LoadScene("CantEscapeOut");
    }
}
