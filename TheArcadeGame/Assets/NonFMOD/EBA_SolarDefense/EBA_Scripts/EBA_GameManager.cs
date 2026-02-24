
using UnityEngine;
using UnityEngine.SceneManagement;

public class EBA_GameManager : MonoBehaviour
{
    InputSubscription Getinput;
    public static int score = 0;
    public Transform scoreobj;
    [SerializeField] GameObject EBA_gameOverMenu;
    [SerializeField] GameObject EBA_trophy;
    public static int TrophyIs = 0;
    public AudioSource source;
    public AudioClip shootclip;

    void Awake()
    {
        Getinput = GetComponent<InputSubscription>();

        if (PlayerPrefs.GetInt("trophy Earned", 0) == 1)
        {
            EBA_trophy.SetActive(true);
        }
        else
        {
            EBA_trophy.SetActive(false);
        }
       

        score = 0;

    }

    public void Update()
    {
        //Added by izzy//
        if (Getinput.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
       //////
        scoreobj.GetComponent<TextMesh> ().text = "Score: " + score.ToString();

        Score();

       

    }

    public void Score()
    {
        if (score >= 150)//gain trophy when points achived
        {
            if (PlayerPrefs.GetInt("EBA_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("EBA_Trophie_Int", 1);
                PlayerPrefs.Save();
            }

            
            if (TrophyIs == 0)
            {
                TrophyIs += 1;
                
                PlayerPrefs.SetInt("trophy Earned", 1);
                PlayerPrefs.Save();
                
                EBA_trophy.SetActive(true);

                if (source != null && shootclip != null)
                {
                    source.PlayOneShot(shootclip);
                }
            }              

        }
    
    }

    public void restart() // reset the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        score = 0;
        TrophyIs = 0;
        
    }

    public void Exit() //exit the game
    {
        
       SceneManager.LoadScene(0);
        Debug.Log("exit");

       // PlayerPrefs.SetInt("trophy Earned", 0);
       // PlayerPrefs.Save();
    }

}
