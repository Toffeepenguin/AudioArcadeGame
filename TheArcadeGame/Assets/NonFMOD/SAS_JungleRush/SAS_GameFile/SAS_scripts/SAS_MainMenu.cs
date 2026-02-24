using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SAS_MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SAS_JungleRush");
    }

    /// /////////////Added by Izzy/////////////////////

    public void QuitGame()
    {
       SceneManager.LoadScene(0);
    }

    /// ////////////////////////////////////////////
}
