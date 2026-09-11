using TMPro;
using UnityEngine;

public class LevelUI : FlashingUI
{
    [SerializeField] GameLoop game_handler_script;

    public void UpdateLevelUI(bool flash)
    {
        if (game_handler_script != null && text != null) text.SetText("LEVEL: " + (game_handler_script.level + 1));
        if (flash) StartFlashing();
    }
}
