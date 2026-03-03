using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LTA_Score : MonoBehaviour
{
    public float curTime;
    public bool GameActive = false;
    public TextMeshProUGUI playerScore;

    private void Awake()
    {
        curTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameActive = true;
        //curTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameActive == true)
        {
            
            curTime += Time.deltaTime;
           
            playerScore.text = "Score: " + curTime.ToString("F2");
        }
    }
}
