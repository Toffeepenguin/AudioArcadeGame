using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class JRO_Exit : MonoBehaviour
{
    JRO_GameManager gM;
    [SerializeField] string nextLevel;
    //[SerializeField] private UnityEvent SnakeExitEvent = new UnityEvent();
    [SerializeField] GameObject winner;
    string currentLevel;

    [SerializeField] float TimeDelay;

    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<JRO_GameManager>();
        winner.SetActive(false);
        currentLevel = gM.ReturnCurrentLevel();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (SceneManager.GetActiveScene().name == "JRO_level4")
        {
            winner.SetActive(true);

            if (PlayerPrefs.GetInt("JRO_Trophie_Int") != 1) {
                PlayerPrefs.SetInt("JRO_Trophie_Int", 1); PlayerPrefs.Save();
            }
            Invoke("BackToMenu", 2);
        }
        if (col.gameObject.name == "JRO_PlayerSprite")
        {
            //gM.LoadNextLevel(nextLevel);
            //SnakeExitEvent?.Invoke();
            //
            //currentLevel = nextLevel;

            Invoke("LoadNextScene", TimeDelay);
        }
        
    }

    private void LoadNextScene()
    {
        gM.LoadNextLevel(nextLevel);
        //SnakeExitEvent?.Invoke();

        currentLevel = nextLevel;
    }


    private void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
