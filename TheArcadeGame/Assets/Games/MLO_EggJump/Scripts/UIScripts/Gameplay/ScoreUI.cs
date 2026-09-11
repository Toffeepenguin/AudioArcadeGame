using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject game_handler;
    GameLoop game_handler_script;

    void Start()
    {
        game_handler_script = game_handler.GetComponent<GameLoop>();
    }

    public void UpdateScoreUI()
    {
        text.SetText("SCORE: " + game_handler_script.score);
    }
}
