using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QME_SplashScreen : MonoBehaviour
{
    // Reference to the InputSubscription component to monitor player inputs
    private InputSubscription _input;

    // Start is called before the first frame update
    void Start()
    {
        // Find the GameObject named "InputManager" in the scene
        // and get its InputSubscription component to handle input logic
        _input = GameObject.Find("InputManager").GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "Q" key input is detected via the InputSubscription component
        if (_input.QInput)
        {
            // Log a debug message to the console
            Debug.Log("Q key pressed. Transitioning to the next scene.");

            // Load the scene named "QME_RogueRunner"
            SceneManager.LoadScene("QME_RogueRunner");
        }

        if (_input.MenuInput)
        {
            Debug.Log("Shift key pressed. Transitioning to the next scene.");

            SceneManager.LoadScene(0);
        }
    }
}
