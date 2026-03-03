using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class HLL_RestartButton : MonoBehaviour
{

    private InputSubscription _input;
    void Start()
    {
        StartCoroutine(ToHLL_Scene()); //Reloading game scene
    }

    IEnumerator ToHLL_Scene()
    {
        yield return new WaitForSeconds(2); // once game has been won or lost, there will be a delay for 2 seconds

        SceneManager.LoadScene("HLL_Splashscreen"); // before the game restarts back to the game scene
       
    }
}
