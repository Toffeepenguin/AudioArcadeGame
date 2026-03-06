using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TTA_WinCondition : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject buttonsMenu;

    public TTA_Movement move;
    public TTA_TrophyManager trophyManager;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        winMenu.SetActive(false);

        loseMenu.SetActive(false);

        buttonsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (winMenu.activeInHierarchy == true || loseMenu.activeInHierarchy == true)
        {
            Time.timeScale = 0;
        }

        if (move.hasPlayerDied == true)
        {
            loseMenu.SetActive(true);
            buttonsMenu.SetActive(true);
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("TTA_Scene");
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene(0);
    }
}
