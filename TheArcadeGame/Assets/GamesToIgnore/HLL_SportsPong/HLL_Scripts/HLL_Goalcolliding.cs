
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class HLL_Goalcolliding : MonoBehaviour
{

    public HLL_Goalcollidingai goal;

    private Scene scene;

    

    private void Start()
    {

        scene = SceneManager.GetActiveScene(); 
        ScoreText.text = $"Home Team: {HomeScore}";
    }



    public static int HomeScore = 0; // home score set to static so score stays on screen for each reload 
    public TextMeshProUGUI ScoreText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // when beach ball collides with obstacle which is the goal
        {
            Destroy(collision.gameObject); // to then destroy beahc ball game object


            SceneManager.LoadScene(scene.name); // reload game scene
            UpdateScore();

        }

    }

    private void UpdateScore()
    {
        HomeScore += 1;
        ScoreText.text = $"Home Score: {HomeScore}";


        if (HomeScore >= 3)
        {

            ShowTrophy();
            Debug.Log("New Achievement unlocked");
            SceneManager.LoadScene("HLL_GameWin");
            
            ScoreText.text = $"Home Team: {HomeScore}";

            goal.changeAwayScore();

            HomeScore = 0;


        }
    }


    private void ShowTrophy()
    {
        FindAnyObjectByType<HLL_TrophyManager>().Hometeamscores();
    }


    public void changeHomeScore()
    {
        HomeScore = 0;
    }



}

