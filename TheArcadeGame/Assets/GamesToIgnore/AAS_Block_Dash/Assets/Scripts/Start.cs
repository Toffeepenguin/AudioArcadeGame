using UnityEngine.SceneManagement;
using UnityEngine;

public class Start : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level01");
    }
}
