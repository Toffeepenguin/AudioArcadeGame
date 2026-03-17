using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKN_Shooting : MonoBehaviour
{
    AudioSource HKN_GhostDeath;
    public AudioSource GhostDeath;
    
    private void Start()
    {
     HKN_GhostDeath = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D (Collision2D coll)
    {
        HKN_GhostDeath.Play(0);

        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log("Monster Killed");
            HKN_GameManager.MonstersKilled = HKN_GameManager.MonstersKilled + 1;
            HKN_GameManager.Score = HKN_GameManager.Score + 10;
            
            
            Destroy(coll.gameObject);
            Destroy(gameObject);
            GhostDeath.Play(0);
        }

        Destroy(gameObject);


    }
}
