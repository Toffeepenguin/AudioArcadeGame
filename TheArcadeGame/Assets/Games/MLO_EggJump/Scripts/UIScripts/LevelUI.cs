using TMPro;
using UnityEngine;

public class MLO_LevelUIScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject game_handler;
    MLO_GameHandlerScript game_handler_script;

    void Start()
    {
        game_handler_script = game_handler.GetComponent<MLO_GameHandlerScript>();
    }

    void Update()
    {
        text.SetText("LEVEL: " + (game_handler_script.GetLevel() + 1));
    }
}
