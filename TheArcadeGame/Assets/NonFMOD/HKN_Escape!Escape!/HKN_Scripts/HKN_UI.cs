using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HKN_UI : MonoBehaviour
{
    public Text healthtext;
    public Text scoretext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthtext.text = "Health : " + HKN_GameManager.playerHealth;
        scoretext.text = "Score : " + HKN_GameManager.Score;
    }
}
