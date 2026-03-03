using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JRO_Hud : MonoBehaviour
{

    basicplayercontroller bpc;

    public GameObject heart1, heart2; //heart3;

    // Start is called before the first frame update
    void Start()
    {
        bpc = FindObjectOfType<basicplayercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bpc.PlayerHealth == 2)
        {
            //heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        //else if (bpc.PlayerHealth == 2)
        //{
        //    heart3.SetActive(false);
        //}
        else if (bpc.PlayerHealth == 1)
        {
            heart2.SetActive(false);
        }
        else
        {
            heart1.SetActive(false);
        }
    }
}
