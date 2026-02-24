using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KCY_GameOver : MonoBehaviour
{
    public KCY_ObstacleSpawner spawner;
    public TMP_Text overText;
    RectTransform textBox;
    public AudioSource loseSource;
    float timer = 0.0f;
    bool timeStart = false;
    //public Enemy enemy;
    //public Obstacle obst;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindWithTag("EnemyProjectile").GetComponent<KCY_ObstacleSpawner>();
        textBox = overText.GetComponent<RectTransform>();
        //enemy = GetComponent<Enemy>();
        //enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

        //obst = GameObject.FindWithTag("Obstacle").GetComponent<Obstacle>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeStart)
        {
            print("time started");
            if (timer < 1.0f)
            {
                timer += Time.deltaTime;
                print("time running...");
            }
            else
            {
                print("loading start");
                SceneManager.LoadScene("KCY_StartScene");
                timer = 0;
            }
        }
        
    }

    IEnumerator TimerToStart()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("KCY_StartScene");

    }

    public void EndGame()
    {
        Time.timeScale = 0;
        loseSource.Play();
        textBox.localPosition = new Vector2(-80, 25);
        StartCoroutine(TimerToStart());
        //timeStart = true;

        //spawner.time = 0;
        //spawner.enemyInst.GetComponent<Enemy>().StopGame();
        //spawner.obstacleInst.GetComponent<Obstacle>().StopGame();
    }
}
