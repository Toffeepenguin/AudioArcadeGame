using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SCH_gameoverGUI : MonoBehaviour
{
    private int score;
    private int perfect;
    private int good;
    private int bad;
    private int missed;
    private int stringCombo;
    private int notesCombo;
    private int StringsNum;
    private int NotesNum;
    private float GUIfloatingSpeed = 1000f;

    //Text GUIs
    public Text scoreText;
    public Text perfectText;
    public Text goodText;
    public Text badText;
    public Text missedText;
    public Text stringComboText;
    public Text notesComboText;


    private bool fullComboPrinted = false;    //Check if the FULL COMBO was printed or not
    private bool hundredPerfectPrinted = false;    //Check if the 100% Perfect was printed or not
    private float time; //Store the time when the scene is loaded

    public Rigidbody2D fullComboPfb;
    public Rigidbody2D beatMasterPfb;

    private bool doOnce = false;
    private void Awake()
    {
        time = Time.time;
        GameObject mainManagerObj = GameObject.Find("SCH_MainManager");
        SCH_MainManager mainManagerScp = mainManagerObj.GetComponent<SCH_MainManager>();

        //Get variables from the main manager
        score = mainManagerScp.SCH_tScore;
        perfect = mainManagerScp.SCH_perfect;
        good = mainManagerScp.SCH_good;
        bad = mainManagerScp.SCH_bad;
        missed = mainManagerScp.SCH_missed;
        stringCombo = mainManagerScp.SCH_sCombo;
        notesCombo = mainManagerScp.SCH_nCombo;
        NotesNum = mainManagerScp.SCH_numOfNotes;
        StringsNum = mainManagerScp.SCH_numOfStrings;

        //Print results into corresponding position
        scoreText.text = "Total Score: " + score.ToString();
        perfectText.text = perfect.ToString();
        goodText.text = good.ToString();
        badText.text = bad.ToString();
        missedText.text = missed.ToString();
        stringComboText.text = stringCombo.ToString();
        notesComboText.text = notesCombo.ToString();
        doOnce = false;
    }

    public void SCH_Retry()
    {
        doOnce = false;
        SceneManager.LoadScene("SCH_DroptheBeatScene");
    }

    public void SCH_Quit()
    {
        doOnce = false;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Time.time - time >= 2 && Time.time - time <= 2.1)
        {
            if (stringCombo == StringsNum)
            {
                if(perfect == NotesNum && !hundredPerfectPrinted)
                {
                    if (!doOnce)
                    {
                        Rigidbody2D fullCombo = Instantiate(fullComboPfb, new Vector3(transform.position.x,
                        transform.position.y - 45, transform.position.z - 2), Quaternion.identity) as Rigidbody2D;
                        fullCombo.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, GUIfloatingSpeed, 0));
                        doOnce = true;
                    }
                }
                else if (notesCombo == NotesNum && !fullComboPrinted)
                {
                    if (!doOnce)
                    {
                        Rigidbody2D beatMaster = Instantiate(beatMasterPfb, new Vector3(transform.position.x,
                        transform.position.y - 45, transform.position.z - 2), Quaternion.identity) as Rigidbody2D;
                        beatMaster.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, GUIfloatingSpeed, 0));
                        doOnce = true;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
