using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HLL_TrophyManager : MonoBehaviour
{
    public GameObject HLL_TROPHY;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TrophyText;
    

    public bool trophywon = false;


    public int HomeScore = 0;
    public int awayscore = 0;
    
    



    void Start()
    {
        if (HLL_TROPHY != null)
        {
            HLL_TROPHY.SetActive(false);
            
            
        }

        ScoreText.text = $"Home Team: {HomeScore}";
    }



    // Update is called once per frame
    public void Hometeamscores()
    {
    
        if (HLL_TROPHY != null)
        {
            HLL_TROPHY .SetActive(true);
            trophywon = true;
            StartCoroutine(Trophyhide(5f));
        }

        if(PlayerPrefs.GetInt("HLL_Trophie_Int") != 1)
        {
            PlayerPrefs.SetInt("HLL_Trophie_Int", 1);
            PlayerPrefs.Save();
        }

    }

    

    IEnumerator Trophyhide(float delay)
    {
        yield return new WaitForSeconds(5);

        HLL_TROPHY.SetActive(false);

        
    }

    


}



