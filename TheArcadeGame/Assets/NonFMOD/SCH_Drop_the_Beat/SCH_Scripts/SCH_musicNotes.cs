using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SCH_musicNotes : MonoBehaviour
{
    GameObject inputObj;
    GameObject scoreBoardObj;
    private Rigidbody2D note;
    private BoxCollider2D coll;
    private InputSubscription inputScp;
    private SCH_score scoreScp;
    private bool withinPerfectTriggerBox = false;
    private bool withinGoodTriggerBox = false;
    private bool withinBadTriggerBox = false;

    public char noteColor;// 'R' = red, 'B' = blue, 'G' = green, 'Y' = yellow
    private bool noteCalculated = false;

    private int keyOneCounter = 0;
    private int keyTwoCounter = 0;
    private int keyThreeCounter = 0;
    private int keyFourCounter = 0;
    private float GUIfloatingSpeed = 100f;


    //Score GUI 
    public Rigidbody2D perfectGUI;
    public Rigidbody2D goodGUI;
    public Rigidbody2D badGUI;
    public Rigidbody2D missedGUI;


    // Start is called before the first frame update
    void Start()
    {
        inputObj = GameObject.Find("SCH_InputSystem");
        scoreBoardObj = GameObject.Find("SCH_ScoreBoard");
        inputScp = inputObj.GetComponent<InputSubscription>();
        scoreScp = scoreBoardObj.GetComponent<SCH_score>();
        note = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    bool keyOneDetected() //If key 1 was pressed recently
    {
        if (inputScp.ActionInput1)
        {
            if ( keyOneCounter >= 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    bool keyTwoDetected() //If key 2 was pressed recently
    {
        if (inputScp.ActionInput2)
        {
            if (keyTwoCounter >= 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    bool keyThreeDetected() //If key 3 was pressed recently
    {
        if (inputScp.ActionInput3)
        {
            if (keyThreeCounter >= 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    bool keyFourDetected() //If key 4 was pressed recently
    {
        if (inputScp.ActionInput4)
        {
            if (keyFourCounter >= 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            withinPerfectTriggerBox = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            withinGoodTriggerBox = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            withinBadTriggerBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            withinPerfectTriggerBox = false;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            withinGoodTriggerBox = false;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            withinBadTriggerBox = false;
        }
    }

    void PerfectTrigger()
    {
        scoreScp.SCH_totalScore += 50;
        scoreScp.SCH_perfectNote++;
        scoreScp.SCH_notesCombo++;
        Rigidbody2D PerfectPfb = Instantiate(perfectGUI, new Vector3(transform.position.x, 
        transform.position.y + 4, transform.position.z), Quaternion.identity) as Rigidbody2D;
        PerfectPfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, GUIfloatingSpeed, 0));
        //Debug.Log("Perfect");
        //Debug.Log(scoreScp.SCH_totalScore);
        noteCalculated = true;
        Destroy(gameObject);
    }

    void GoodTrigger()
    {
        scoreScp.SCH_totalScore += 25;
        scoreScp.SCH_goodNote++;
        scoreScp.SCH_notesCombo++;
        Rigidbody2D GoodPfb = Instantiate(goodGUI, new Vector3(transform.position.x,
        transform.position.y + 4, transform.position.z), Quaternion.identity) as Rigidbody2D;
        GoodPfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, GUIfloatingSpeed, 0));
        //Debug.Log("Good");
        //Debug.Log(scoreScp.SCH_totalScore);
        noteCalculated = true;
        Destroy(gameObject);
    }

    void BadTrigger()
    {
        scoreScp.SCH_totalScore += 5;
        scoreScp.SCH_badNote++;
        if (scoreScp.SCH_maxNotesCombo < scoreScp.SCH_notesCombo)
        {
            scoreScp.SCH_maxNotesCombo = scoreScp.SCH_notesCombo;
        }
        scoreScp.SCH_notesCombo = 0;
        Rigidbody2D BadPfb = Instantiate(badGUI, new Vector3(transform.position.x,
        transform.position.y + 4, transform.position.z), Quaternion.identity) as Rigidbody2D;
        BadPfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, GUIfloatingSpeed, 0));
        //Debug.Log("Bad");
        //Debug.Log(scoreScp.SCH_totalScore);
        noteCalculated = true;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (inputScp.ActionInput1)
        {
            keyOneCounter++;
        }
        else
        {
            keyOneCounter = 0;
        }

        if (inputScp.ActionInput2)
        {
            keyTwoCounter++;
        }
        else
        {
            keyTwoCounter = 0;
        }

        if (inputScp.ActionInput3)
        {
            keyThreeCounter++;
        }
        else
        {
            keyThreeCounter = 0;
        }

        if (inputScp.ActionInput4)
        {
            keyFourCounter++;
        }
        else
        {
            keyFourCounter = 0;
        }


        if (withinPerfectTriggerBox == true)
        {
            if ((keyOneDetected() && noteColor == 'R') ||
                (keyTwoDetected() && noteColor == 'Y') ||
                (keyThreeDetected() && noteColor == 'G') ||
                (keyFourDetected() && noteColor == 'B'))
            {
                PerfectTrigger();
            }
        }

        else if (withinGoodTriggerBox == true)
        {
            if ((keyOneDetected() && noteColor == 'R') ||
                (keyTwoDetected() && noteColor == 'Y') ||
                (keyThreeDetected() && noteColor == 'G') ||
                (keyFourDetected() && noteColor == 'B'))
            {
                GoodTrigger();
            }
        }

        else if (withinBadTriggerBox == true)
        {
            if ((keyOneDetected() && noteColor == 'R')||
                (keyTwoDetected() && noteColor == 'Y')||
                (keyThreeDetected() && noteColor == 'G')||
                (keyFourDetected() && noteColor == 'B'))
            {
               BadTrigger();
            }
        }
    }

    private void OnBecameInvisible()
    {
        if (!noteCalculated)
        {
            scoreScp.FindMaxNotesCombo();
            scoreScp.SCH_notesCombo = 0;
            scoreScp.SCH_missedNote ++;
            //Debug.Log("Missed");
            Destroy(gameObject);
            Rigidbody2D MissedPfb = Instantiate(missedGUI, new Vector3(transform.position.x,
            transform.position.y + 10, transform.position.z), Quaternion.identity) as Rigidbody2D;
            MissedPfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, GUIfloatingSpeed, 0));
        }
    }
}

