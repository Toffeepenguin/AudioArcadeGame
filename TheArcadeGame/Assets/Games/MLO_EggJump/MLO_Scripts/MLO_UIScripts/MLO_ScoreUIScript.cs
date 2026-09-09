using TMPro;
using UnityEngine;

public class MLO_ScoreUIScript : MonoBehaviour
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
        text.SetText("SCORE: " + game_handler_script.GetScore());
    }
}
