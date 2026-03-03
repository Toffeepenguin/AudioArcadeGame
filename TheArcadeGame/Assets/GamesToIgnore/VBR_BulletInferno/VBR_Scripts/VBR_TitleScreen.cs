using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VBR_TitleScreen : MonoBehaviour
{
    private GameObject Ui;
    public void GoToGame()
    {
        SceneManager.LoadScene("VBR_NewGameScene");
    }
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
