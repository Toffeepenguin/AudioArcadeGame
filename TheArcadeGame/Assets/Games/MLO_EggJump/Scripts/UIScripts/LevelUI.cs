using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject game_handler;
    GameLoop game_handler_script;

    void Start()
    {
        game_handler_script = game_handler.GetComponent<GameLoop>();
    }

    public void UpdateLevelUI()
    {
        text.SetText("LEVEL: " + (game_handler_script.level + 1));
    }
}
