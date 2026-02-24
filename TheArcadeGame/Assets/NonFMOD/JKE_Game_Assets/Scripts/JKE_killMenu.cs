using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JKE_killMenu : MonoBehaviour
{
    public GameObject StartMenu;
    public JKE_GameManager gm;
    public JKE_Player_Collision pc;
    // Start is called before the first frame update
    void Start()
    {
        StartMenu.SetActive(true);
        gm = JKE_GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.isPlaying)
        {
            StartMenu.SetActive(false);
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("JKE_Scene");
    }
}
