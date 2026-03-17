
using UnityEngine;
using UnityEngine.SceneManagement;

public class HKN_Win : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (HKN_GameManager.Score >= 150)
        {
            SceneManager.LoadScene("HKN_Win");

            if (PlayerPrefs.GetInt("HKN_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("HKN_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }
}
