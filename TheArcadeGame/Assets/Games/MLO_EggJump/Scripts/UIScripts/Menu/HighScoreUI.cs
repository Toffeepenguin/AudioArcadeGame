using UnityEngine;

public class HighScoreUI : FlashingUI
{
    [SerializeField] private GameLoop game_handler_script;
    public GameObject trophy;
    private Trophy trophy_script;

    void Start()
    {
        trophy_script = trophy.GetComponent<Trophy>();
    }

    public void UpdateHighScoreUI()
    {
        if (game_handler_script.high_score != 0) {
            text.enabled = true;
            if (!trophy_script.collected) text.SetText($"Highscore: {game_handler_script.high_score} | {game_handler_script.trophy_score - game_handler_script.high_score} points away from the trophy ({game_handler_script.trophy_score})");
            else { text.SetText($"Highscore: {game_handler_script.high_score} | You have collected the trophy!"); StartFlashing(); }
        }
        else if (game_handler_script.high_score == 0) text.enabled = false;
    }
}