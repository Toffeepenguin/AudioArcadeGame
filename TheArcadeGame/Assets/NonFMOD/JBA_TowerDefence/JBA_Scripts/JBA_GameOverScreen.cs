using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JBA_GameOverScreen : MonoBehaviour
{
    public TMP_Text pointsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "Score : " + score.ToString();
    }

    public void restartButton()
    {
        SceneManager.LoadScene("JBA_TowerDefence");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("VAG_VirtualArcadeScene");
    }
}
