using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FMA_CoinManager : MonoBehaviour
{
    public int coinCount;
    public Text coinText;
    public Text trophyText;

    public GameObject uiText;
    public static bool Trophy = false;
    // Start is called before the first frame update
    void Start()
    {
        uiText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Score: " + coinCount.ToString();

        if (coinCount == 16)
        {
            uiText.SetActive(true);
            Trophy = true;

            if (PlayerPrefs.GetInt("FMA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("FMA_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }

        
    }
}
