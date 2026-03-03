using UnityEngine.UI;
using UnityEngine;

public class AAS_PlayerScoreUI : MonoBehaviour
{

    public Transform player;
    public Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = player.position.z.ToString("0");
    }
}
