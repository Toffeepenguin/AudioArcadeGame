using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HKN_Play : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("HKN_Main");
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene("HKN_Menu");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("VAG_VirtualArcadeScene");
    }

    
}
