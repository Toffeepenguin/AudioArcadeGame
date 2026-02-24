using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class MAL_LevelComplete : MonoBehaviour
{
    [SerializeField] GameObject LevelDone;
    private InputSubscription _Input;
    private bool done = false;
    public bool GAME_WON = false;
    private void Awake()
    {
        _Input = GameObject.Find("GameManager").GetComponent<InputSubscription>();
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Interactable").Length == 0 && !done)
        {
            Instantiate(LevelDone);
            done = true;
            if (SceneManager.GetActiveScene().name == "MAL_Level7")
            {
                if (PlayerPrefs.GetInt("MAL_Trophie_Int") != 1)
                {
                    PlayerPrefs.SetInt("MAL_Trophie_Int", 1);
                    PlayerPrefs.Save();
                }
            }
        }
        if (done && _Input.SpaceInput)
        {
            SceneManager.LoadScene("MAL_Select");
        }
    }
}
