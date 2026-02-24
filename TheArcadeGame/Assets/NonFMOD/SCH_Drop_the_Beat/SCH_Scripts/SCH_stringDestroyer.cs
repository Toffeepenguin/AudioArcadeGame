using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCH_stringDestroyer : MonoBehaviour
{
    GameObject scoreBoardObj;
    private SCH_score scoreScp;
    // Start is called before the first frame update
    void Start()
    {
        scoreBoardObj = GameObject.Find("SCH_ScoreBoard");
        scoreScp = scoreBoardObj.GetComponent<SCH_score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            //Debug.Log("String missed");
            scoreScp.FindMaxStringCombo();
            Destroy(collision.gameObject);
            scoreScp.SCH_stringCombo = 0;
        }
    }
}
