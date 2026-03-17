using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EBA_StartMenu : MonoBehaviour
{
    [SerializeField] GameObject EBA_startMenu;
    [SerializeField] GameObject EBA_trophy;
    InputSubscription Getinput;

    void Awake()
    {
        Getinput = GetComponent<InputSubscription>();

        if (PlayerPrefs.GetInt("trophy Earned", 0) == 1)
        {
            EBA_trophy.SetActive(true);
        }
        else
        {
            EBA_trophy.SetActive(false);
        }
    }

    private void Update()
    {
        Time.timeScale = 0f;
    }
    public void Play()
    {
        Time.timeScale = 1f;
        EBA_startMenu.SetActive(false);
    }
    public void Exit()
    {
       
        SceneManager.LoadScene(0);
        Debug.Log("exit");

        //PlayerPrefs.SetInt("trophy Earned", 0);
        //PlayerPrefs.Save();
    }
}
