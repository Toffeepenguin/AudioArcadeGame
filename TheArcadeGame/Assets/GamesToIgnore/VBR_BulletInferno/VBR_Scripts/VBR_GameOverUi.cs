using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VBR_GameOverUi: MonoBehaviour
{
    private GameObject gameOverUi;
    private GameObject gamePlayUi;
    private InputSubscription _Input;
    private VBR_UiManager _UiManager;
    private VBR_TrophyCheck _TrophyCheck;

    [Header("Event system parameters")]
    [SerializeField] private EventSystem _EventSystem;
    [SerializeField] private GameObject StartingButton;

    [Header("Textbox parameters")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text trophyGainedText;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip trophyGetSound;
    [SerializeField] private AudioClip trophyMissedSound;
    private AudioSource audioSource;

    private bool MenuPressed;

    private void Start()
    {
        gameOverUi = GameObject.Find("GameOverUi");
        gamePlayUi = GameObject.Find("GameplayUi");
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        _UiManager = GameObject.Find("Ui").GetComponent<VBR_UiManager>();
        _TrophyCheck = GameObject.Find("GameManager").GetComponent<VBR_TrophyCheck>();
        audioSource = GetComponent<AudioSource>();
        gameOverUi.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (_UiManager.healthAmount <=0f)
        {
            GameOverState();
        }
    }
    private void GameOverState()
    {
        gamePlayUi.SetActive(false);
        gameOverUi.SetActive(true);
        updateScore();
        updateTime();
        updateTrophyText();
        SetFirstSelected(StartingButton);
        Time.timeScale = 0f;
    }
    private void SetFirstSelected(GameObject button)
    {
        _EventSystem.SetSelectedGameObject(null);
        _EventSystem.firstSelectedGameObject = button;
        _EventSystem.SetSelectedGameObject(button);
    }
    private void updateScore()
    {
        scoreText.text = "Score: " + _UiManager.score.ToString();
    }
    private void updateTime()
    {
        timeText.text = "Time: " + _UiManager.totalTime;
    }
    private void updateTrophyText()
    {
        //Need to add a condition to this
        if (_UiManager.score >= 1200)
        {
            _TrophyCheck.VBR_TrophyGetSet(true);
            Time.timeScale = 1;
            audioSource.PlayOneShot(trophyGetSound, 0.15f);
            Time.timeScale = 0;
            trophyGainedText.text = "Congratulations! you have gained a trophy!";

            if (PlayerPrefs.GetInt("VBR_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("VBR_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
            

        }
        else
        {
            _TrophyCheck.VBR_TrophyGetSet(false);
            audioSource.PlayOneShot(trophyMissedSound, 0.15f);
            trophyGainedText.text = "Unlucky :( you need 1200 score to gain a trophy.";
        }
    }
}
