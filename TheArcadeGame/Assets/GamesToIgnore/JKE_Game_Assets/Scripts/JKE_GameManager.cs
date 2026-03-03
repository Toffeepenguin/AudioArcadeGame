
using UnityEngine;
using UnityEngine.SceneManagement;

public class JKE_GameManager : MonoBehaviour
{
    InputSubscription GetInputGM;
    public static JKE_GameManager Instance;

    //Singleton!
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public static bool Trophy = false;
    public float currentScore = 0f;
    public bool isPlaying = false;
    public bool gameOver = false;
    public int ActualScore;

    private void Start()
    {
        GetInputGM = GetComponent<InputSubscription>();
        gameOver = false;
        ActualScore = 0;
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }
        if (GetInputGM.ActionInput1)
        {
            isPlaying = true;
        }
        if (GetInputGM.ActionInput2)
        {
            SceneManager.LoadScene("VAG_VirtualArcadeScene");
        }
        ActualScore = Mathf.RoundToInt(currentScore * 69);


        if (GetInputGM.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        currentScore = 0f;
        isPlaying = false;
        gameOver = true;

    }

    public string intScore()
    {
        return Mathf.RoundToInt(currentScore*69).ToString();
    }


}
