using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_GoBack : MonoBehaviour
{
    GameObject MainMenuGo;
    public void GoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
