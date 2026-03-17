using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public GameObject completeLevelUI;

    public float restartDelay = 1f;

    public void CompleteLevel ()
    {
        completeLevelUI.SetActive(true);

    }

   public void EndGame ()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }

        
    }

    private void Update()
    {
      
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
