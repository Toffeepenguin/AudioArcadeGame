using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ATA_Score : MonoBehaviour
{
    public int disRun;
    public GameObject disDisplayed;
    public bool addingDis = false;
    public GameObject disDisplayedEnd;

    // Star system
    public GameObject[] stars; // Array of star objects
    private int starCount = 0;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (addingDis == false)
        {
            addingDis = true;
            StartCoroutine(AddiingDis());
        }

        // Update stars based on the distance covered
        UpdateStars();
    }

    IEnumerator AddiingDis()
    {
        disRun += 1;
        disDisplayed.GetComponent<TextMeshProUGUI>().text = disRun.ToString();
        disDisplayedEnd.GetComponent<TextMeshProUGUI>().text = disRun.ToString();
        yield return new WaitForSeconds(0.25f);
        addingDis = false;
    }

    // Update the star system based on the distance run
    public void UpdateStars()
    {
        if (disRun >= 500 && starCount < 3)
        {
            SetStars(3);

            if (PlayerPrefs.GetInt("ATA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("ATA_Trophie_Int", 1);
                PlayerPrefs.Save();
                Debug.Log("Andrei Trophy");
            }
        }

        if (disRun >= 350 && starCount < 2)
        {
            SetStars(2);
        }

        if (disRun >= 200 && starCount < 1)
        {
            SetStars(1);
        }

        // Activate stars based on the current rating
        void SetStars(int count)
        {
            starCount = count;

            // Enable stars based on the count
            for (int i = 0; i < stars.Length; i++)
            {
                if (stars[i] != null)
                    stars[i].SetActive(i < starCount);
            }
        }
    }
}
