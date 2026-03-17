using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HLL_Startscreen : MonoBehaviour
{

    private InputSubscription _input;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tosplashscreen()); //start the splash screen
        _input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
    }

    IEnumerator Tosplashscreen()
    {
        yield return new WaitForSeconds(1); //The splash screen will be shown for 1 second 

    }

    void Update()
    {
        if (_input.SpaceInput)
        {
            SceneManager.LoadScene("HLL_Scene");
        }

        if (_input.EInput)
        {

            SceneManager.LoadScene(0);

        }

    }
}
