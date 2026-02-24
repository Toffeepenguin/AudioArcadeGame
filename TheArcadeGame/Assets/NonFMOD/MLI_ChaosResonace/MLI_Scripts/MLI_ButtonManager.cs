using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MLI_ButtonManager : MonoBehaviour
{
    public void GameOverTryAgain()
    {
        Destroy(GameObject.Find("MLI_Player"));
        Time.timeScale = 1f;
        SceneManager.LoadScene("MLI_ChaosResonance");
    }

    public void Quit()
    {
        Destroy(GameObject.Find("MLI_Player"));
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
