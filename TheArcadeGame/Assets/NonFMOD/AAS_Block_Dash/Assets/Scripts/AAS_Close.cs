using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_Close : MonoBehaviour
{

    //Update is called once per frame
    void Update()
    {
        void QuitGame()
        {
            SceneManager.LoadScene(0);
            Debug.Log("Game is exiting");
            //Just to make sure its working
        }
    }
}
