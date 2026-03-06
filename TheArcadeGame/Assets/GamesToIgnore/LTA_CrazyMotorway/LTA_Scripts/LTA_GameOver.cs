using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LTA_GameOver : MonoBehaviour
{
    public AudioSource LTA_Background;
    public AudioSource LTA_carSound;
    public AudioSource LTA_gameOver;
    public AudioSource LTA_gameWin;

    public GameObject CanvasObject;

    public TextMeshProUGUI TextGameOver;
    public TextMeshProUGUI TextGameWin;

    public LTA_Score Score;//Allows access to LTA_Score script

    public bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        CanvasObject.SetActive(false);
        hasWon = false;
        TextGameOver.enabled = false;
        TextGameWin.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Score.curTime >= 50 && hasWon == false)
        {
            YouWin();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Score.GameActive = false;
            CanvasObject.SetActive(true);
            TextGameOver.enabled = true;
            TextGameWin.enabled = false;
            Time.timeScale = 0;
            LTA_Background.Stop();
            LTA_carSound.Stop();
            LTA_gameOver.Play();
        }
    }

    public void YouWin()
    {
        hasWon = true;//This makes YouWin only run once
        Score.GameActive = false;
        CanvasObject.SetActive(true);
        TextGameOver.enabled = false;
        TextGameWin.enabled = true;
        Time.timeScale = 0;
        LTA_Background.Stop();
        LTA_carSound.Stop();
        LTA_gameWin.Play();

        //code for the trophy
        if(PlayerPrefs.GetInt("LTA_Trophie_int") != 1)
        {
            PlayerPrefs.SetInt("LTA_Trophie_int", 1);
            PlayerPrefs.Save();
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("LTA_CrazyMotorway");
        Score.curTime = 0;
        Time.timeScale = 1;

    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
