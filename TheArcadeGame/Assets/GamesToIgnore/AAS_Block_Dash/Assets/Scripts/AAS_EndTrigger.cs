using UnityEngine;

public class AAS_EndTrigger : MonoBehaviour
{

    public AAS_GameManager gameManager;

    void OnTriggerEnter ()
    {
        gameManager.CompleteLevel();

        if (PlayerPrefs.GetInt("AAS_Trophie_Int") != 1)
        {
            PlayerPrefs.SetInt("AAS_Trophie_Int", 1);
            PlayerPrefs.Save();
        }
    }


}
