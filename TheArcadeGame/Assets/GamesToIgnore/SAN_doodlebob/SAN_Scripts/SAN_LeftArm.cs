using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SAN_LeftArm : MonoBehaviour
{
    //private InputSubscription PlayerControls;
    private SAN_Movement SAN_Move;
    public SAN_ScoreManager SAN_Score;
    public AudioClip Punch;
    public AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerControls = GameObject.Find("GameManager").GetComponent<InputSubscription>();
        SAN_Move = gameObject.GetComponentInParent<SAN_Movement>();
        /*SAN_Score = gameObject.GetComponentInParent<SAN_ScoreManager>();*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && SAN_Move.PunchHitbox == true && SAN_Move.PLeft == true) 
        {
            Destroy(collision.gameObject);
            SAN_Score.AddScore(10000);
            AudioSource.PlayOneShot(Punch);
        }
    }
}
