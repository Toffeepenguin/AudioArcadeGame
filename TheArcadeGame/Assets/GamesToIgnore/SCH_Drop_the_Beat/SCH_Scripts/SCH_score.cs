using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SCH_score : MonoBehaviour
{
    public int SCH_totalScore { get; set; } = 0;
    public int SCH_perfectNote { get; set; } = 0;
    public int SCH_goodNote { get; set; } = 0;
    public int SCH_badNote { get; set; } = 0;
    public int SCH_stringCombo { get; set; } = 0;
    public int SCH_missedNote { get; set; } = 0;
    public int SCH_notesCombo { get; set; } = 0;
    public int SCH_maxNotesCombo { get; set; } = 0;
    public int SCH_maxStringCombo { get; set; } = 0;


    //GUI Elements:
    public Text scoreText;
    public Text comboText;
    public Text stringComboText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = SCH_totalScore.ToString();
        comboText.text = SCH_notesCombo.ToString() + " Combos";
        stringComboText.text = SCH_stringCombo.ToString() + " Combos";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = SCH_totalScore.ToString();
        comboText.text = SCH_notesCombo.ToString() + " Combos";
        stringComboText.text = SCH_stringCombo.ToString() + " Combos";
    }

    public void FindMaxStringCombo()
    {
        if (SCH_maxStringCombo < SCH_stringCombo)
        {
            SCH_maxStringCombo = SCH_stringCombo;
        }
    }

    public void FindMaxNotesCombo()
    {
        if (SCH_maxNotesCombo < SCH_notesCombo)
        {
            SCH_maxNotesCombo = SCH_notesCombo;
        }
    }
}

//Reference: Coco Code (2021) Points counter, HIGH SCORE and display UI in your game - Score points Unity tutorial.
//[video] Available at: https://www.youtube.com/watch?v=YUcvy9PHeXs [Accessed 19 November 2024].