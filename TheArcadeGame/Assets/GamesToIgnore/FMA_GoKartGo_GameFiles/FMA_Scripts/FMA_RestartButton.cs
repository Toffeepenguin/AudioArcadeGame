
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FMA_RestartButton : MonoBehaviour
{
    float timer = 0.0f;

    void Update()
    {
        if (timer < 3.0f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Debug.Log("?");
            Scene CurrentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(CurrentScene.name);
            timer = 0.0f;
        }
    }
}
    
