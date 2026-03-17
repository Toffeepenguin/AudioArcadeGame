using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KCY_ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public KCY_Player player;

    // Start is called before the first frame update
    void Start()
    {
        //SetScore(2);
        scoreText.text = "Score: " + 0;
        
    }

    // Update is called once per frame
    void Update()
    {

        //track last score. if current score is different than the last score, then update the ui

        //scoreText.text = "Score: " + player.GetComponent<Player>().SCORE;
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("chaning score");
            score++;
            //scoreText.text = "Score: " + score;
            newText.text = "Score: " + score;
        }
        */
    }

    public void SetScore(int newScore)
    {
        //scoreText.text = "Score: " + player.GetComponent<Player>().SCORE;
        scoreText.text = "Score: " + newScore;

        if (newScore == 100)
        {
            print("Trophy won!");

            if (PlayerPrefs.GetInt("KCY_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("KCY_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }
}
