using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class JRO_GameManager : MonoBehaviour
{

    public string currentLevel;
    public static int Score = 0;
    public static int HighScore = 0;
    [SerializeField] Text ScoreText;

    //[SerializeField] private UnityEvent<int> ScoreUpdatedEvent = new UnityEvent<int>();

    public void LoadNextLevel(string x)
    {
        SceneManager.LoadScene(x);
    }

    private void Awake()
    {
        //PlayerPrefs.SetInt("PlayerScore", 0);

        //if (PlayerPrefs.GetInt("PlayerScore") = null)
        //{
        //    int newScore = 0;
        //} else
        //{

        //}
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + Score;
    }

    //public int Score
    //{
    //    get => this.score;

    //    set
    //    {
    //        this.score = value;
    //        this.ScoreUpdatedEvent?.Invoke(score);
    //    }
    //}

    public string ReturnCurrentLevel()
    {
        return currentLevel;
    }
}
