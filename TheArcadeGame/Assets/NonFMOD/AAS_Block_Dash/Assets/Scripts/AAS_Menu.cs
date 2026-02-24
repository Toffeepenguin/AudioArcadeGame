using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_Menu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(66);
    }

    public void GoCustomization()
    {
        SceneManager.LoadScene("CustomScreen");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
