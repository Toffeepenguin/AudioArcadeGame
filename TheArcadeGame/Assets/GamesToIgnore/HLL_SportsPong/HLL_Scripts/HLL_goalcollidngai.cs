
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class HLL_Goalcollidingai : MonoBehaviour
{

    public HLL_TrophyManager TrophyManager;

    private Scene scene;
    [SerializeField] HLL_Goalcolliding goal;

    private void Start()
    {
     scene = SceneManager.GetActiveScene();
     ScoreText.text = $"Away Team: {AwayScore}"; 
    }



    public static int AwayScore = 0; // set aways score to static to keep it there after each time the game reloads
    public TextMeshProUGUI ScoreText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) //if the beach ball collides with the trigger set on the goal
        {
            Destroy(collision.gameObject); // beach ball will be destroyed

            
            SceneManager.LoadScene(scene.name); // game scene will be reloaded
            UpdateScore(); // score is updated so after each reload home and away scores will be set to 0

        }

    }

    private void UpdateScore()
    {
        AwayScore += 1; // each time player scores a goal, score will be updated
        ScoreText.text = $"Away Team: {AwayScore}";


        if (AwayScore >= 3)
        {

            SceneManager.LoadScene("HLL_GameLose"); // load lose scene for the player because ai won 
            ScoreText.text = $"Away Team: {AwayScore}";

            goal.changeHomeScore(); // ai needs to know home score

            AwayScore = 0; //reset score back to 0 


        }
        
    }


    public void changeAwayScore()
    {
        AwayScore = 0;
    }

   

}
