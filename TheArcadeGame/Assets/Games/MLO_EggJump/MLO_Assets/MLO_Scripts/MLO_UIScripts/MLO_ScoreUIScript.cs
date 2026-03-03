using TMPro;
using UnityEngine;

public class MLO_ScoreUIScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject game_handler;
    MLO_GameHandlerScript game_handler_script;


    // Start is called before the first frame update
    void Start()
    {
        game_handler_script = game_handler.GetComponent<MLO_GameHandlerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("SCORE: " + game_handler_script.GetScore());
    }
}
