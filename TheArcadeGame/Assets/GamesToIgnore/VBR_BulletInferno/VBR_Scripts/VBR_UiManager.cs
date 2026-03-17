using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Windows;

public class VBR_UiManager : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private VBR_Player player;

    public string totalTime;
    public int score = 0;

    [Header ("Textbox parameters")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;

    [Header ("Health bar parameters")]
    [SerializeField] private Image healthBar;
    [SerializeField] public float healthAmount = 100f;

    private float elapsedTime;
    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score: " + score.ToString();
        player = GameObject.Find("Player").GetComponent<VBR_Player>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        updateTimeUI();
    }

    public void addScore(int Score)
    {
        score += Score;
        scoreText.text = "Score: " + score.ToString();
    }
    private void updateTimeUI()
    {
        int hours = Mathf.FloorToInt(elapsedTime / 36000f);
        int minutes = Mathf.FloorToInt((elapsedTime - hours * 36000f) / 60f);
        int seconds = Mathf.FloorToInt((elapsedTime - hours * 36000f) - (minutes * 60f));

        string clockString = string.Format("{0:00}:{1:00}:{2:00}", hours,minutes,seconds);
        timeText.text = "Time: " + clockString;
        totalTime = clockString;
    }

    public void healthUpdate(float number)
    {
        healthAmount = number;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }
}
