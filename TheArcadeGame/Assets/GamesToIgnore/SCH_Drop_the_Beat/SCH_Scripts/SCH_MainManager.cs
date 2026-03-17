using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCH_MainManager : MonoBehaviour
{
    GameObject inputObj;
    private InputSubscription inputScp;
    public static SCH_MainManager Instance;

    public int SCH_tScore { get; private set; } //total score
    public int SCH_perfect { get; private set; } //number of perfect note
    public int SCH_good { get; private set; } //number of good note
    public int SCH_bad { get; private set; } //number of bad note
    public int SCH_missed { get; private set; } //number of missed note
    public int SCH_sCombo { get; private set; } //maximum combo of strings hitted
    public int SCH_nCombo { get; private set; } //maximum combo of notes hitted
    public int SCH_numOfStrings { get; set; } //total number of strings spawned
    public int SCH_numOfNotes { get; set; } //total number of notes spawned

    public bool SCH_trophyCollected { get; private set; } //If the trophy is collected

    private void Awake()
    {
        inputObj = GameObject.Find("SCH_InputSystem");
        inputScp = inputObj.GetComponent<InputSubscription>();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CollectScores()
    {
        GameObject scoreboardObj = GameObject.Find("SCH_ScoreBoard");
        SCH_score scoreboardScp = scoreboardObj.GetComponent<SCH_score>();

        //get variables from
        SCH_tScore = scoreboardScp.SCH_totalScore;
        SCH_perfect = scoreboardScp.SCH_perfectNote;
        SCH_good = scoreboardScp.SCH_goodNote;
        SCH_bad = scoreboardScp.SCH_badNote;
        SCH_missed = scoreboardScp.SCH_missedNote;
        SCH_sCombo = scoreboardScp.SCH_maxStringCombo;
        SCH_nCombo = scoreboardScp.SCH_maxNotesCombo;

        if (SCH_tScore >= 9000)
        {
            SCH_trophyCollected = true;

            if (PlayerPrefs.GetInt("SCH_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("SCH_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }

    void Update()
    {
        if (inputScp.MenuInput)
        {
            Debug.Log("Menu pressed");
            ///////////Added by Izzy/////////////
            SceneManager.LoadScene(0);
            ///////////////////////////////////////
        }
    }
}
