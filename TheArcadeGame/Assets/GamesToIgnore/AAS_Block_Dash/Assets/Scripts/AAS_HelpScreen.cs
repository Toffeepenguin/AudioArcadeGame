using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_HelpScreen : MonoBehaviour
{
     GameObject HelpScreenGo; 
    public void GoHelpScreen()
    {
        SceneManager.LoadScene("HelpScreen");
    }
}
