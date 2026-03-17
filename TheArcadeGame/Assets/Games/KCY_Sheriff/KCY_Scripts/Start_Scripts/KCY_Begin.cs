using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KCY_Begin : MonoBehaviour
{
    public TMP_Text startText;
    public AudioSource startSound;
    [SerializeField] InputSubscription getInput;


    float timer = 0f;
    bool timeStarted = false;
    bool canInput = true;
    // Start is called before the first frame update
    void Start()
    {
        
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (getInput.SpaceInput && canInput)
        {
            canInput = false;
            //startSound.Play();
            timeStarted = true;
        }

        if (timeStarted)
        {
            StartTimer();
            
        }

        if (getInput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }


    void StartTimer()
    {
        
        if (timer < 1.0f)
        {
            print("Time running...");
            timer += Time.deltaTime;
        }
        else
        {
            print("Time ran out");

            SceneManager.LoadScene("KCY_GameScene");
            timer = 0f;
        }
    }
}
