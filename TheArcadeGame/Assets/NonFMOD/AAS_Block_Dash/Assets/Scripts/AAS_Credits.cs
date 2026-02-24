using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_Credits : MonoBehaviour
{

    public void Quit()
    {
        SceneManager.LoadScene(0);
        Debug.Log("QUIT");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

}
