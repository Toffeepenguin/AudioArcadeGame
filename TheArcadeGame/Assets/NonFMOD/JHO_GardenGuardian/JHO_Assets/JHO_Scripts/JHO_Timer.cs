using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float JHO_timeValue = 90; // setting the time value to 90 seconds
    public Text JHO_timeText; // allows the text to be show

    // Update is called once per frame
    void Update()
    {
        if (JHO_timeValue > 0) // if the time value is greater than 0 
        {
            JHO_timeValue -= Time.deltaTime; // then the time value will be taken away each second
        }
        else
        {
            JHO_timeValue = 0; // else the time value is equal to 0  
            SceneManager.LoadScene("JHO_GameOver");

        }

        DisplayTime(JHO_timeValue); // display the time to the UI
    }

    void DisplayTime(float timeToDisplay) // a method that shows the time on screen
    {
        if (timeToDisplay < 0) // if the time to display is less than 0
        {
            timeToDisplay = 0; // then the time to display will be equal to 0 
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  // calculates the minutes
        float seconds = Mathf.FloorToInt(timeToDisplay % 60); // calculates the seconds

        JHO_timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);// shows the minutes and seconds in the format 0:00
    }

    public void StopTimer()
    {
        enabled = false;
    }
}
