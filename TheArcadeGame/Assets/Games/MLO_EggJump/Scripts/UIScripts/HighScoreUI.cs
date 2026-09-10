using TMPro;
using UnityEngine;

public class RecordUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject game_handler;
    GameLoop game_handler_script;
    public GameObject trophy;
    MLO_TrophyScript trophy_script;
    int trophy_score;
    int high_score;

    void Start()
    {
        game_handler_script = game_handler.GetComponent<GameLoop>();
        trophy_script = trophy.GetComponent<MLO_TrophyScript>();
        trophy_score = game_handler_script.trophy_score;
    }

    void Update()
    {
        high_score = game_handler_script.high_score;
        if (high_score != 0) {
            text.enabled = true;
            if (high_score < trophy_score) {
                text.SetText("Highscore: " + high_score + "\n" + (trophy_score - high_score) + " points away from the trophy (" + trophy_score + ")");
            }
            else if (trophy_script.IsCollected())
            {
                text.SetText("Highscore: " + high_score + "\nYou have collected the trophy!");
            }
        }
        else if (high_score == 0) text.enabled = false;
    }
}