using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void restartbutton()
    {
        SceneManager.LoadScene("SAS_MainMenu");
        
    }
    public void Quitbutton()
    {
        SceneManager.LoadScene(0);
    }


}
