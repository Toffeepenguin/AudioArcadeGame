using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JHO_ButtonHander : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("JHO_GardenGuardian");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
