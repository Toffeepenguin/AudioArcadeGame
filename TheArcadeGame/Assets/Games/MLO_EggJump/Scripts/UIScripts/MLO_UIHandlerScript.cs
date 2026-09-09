using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MLO_UIHandlerScript : MonoBehaviour
{
    CanvasRenderer rndr;
    public GameObject transition_UI;
    MLO_TransitionScript transition_UI_script;

    void Start()
    {
        rndr = GetComponent<CanvasRenderer>();
        transition_UI_script = transition_UI.GetComponent<MLO_TransitionScript>();
    }

    void StartGame()
    {
        // fade out logo
        // fade in score and level

        //CrossFadeColor from Image?
    }

    void EndGame()
    {
        // fade in logo
        // fade out score and level
    }
}
