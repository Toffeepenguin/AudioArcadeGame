using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MMI_GameManager : MonoBehaviour
{
    //int INT ;
    //PlayerPrefs.SetInt("INT", 1);
    //LevelsUnlocked = PlayerPrefs.GetInt("INT", 1);
    //PlayerPrefs.Save();



    InputSubscription GetInput;
    [SerializeField] MMI_LoadingScreen LoadingScreen;

    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject GameMaterial;

    [SerializeField] GameObject[] Buffs;
    float BuffDropTimer;


    [SerializeField] GameObject EndGameUI;
    [SerializeField] Text StatusText;
    
    [SerializeField] Text TimeText;
    string TimeFormat = "{0,2:00}:{1,2:00}:{2,2:00}.{3,3:000}";


    [SerializeField] Text ScoreText;
    [SerializeField] Text InGameScoreText;
    string ScoreFormat = "{0,9:000000000}";
    float Miliseconds = 0f;
    int Seconds, Minutes, Hours;


    [SerializeField] Text ShotsFiredText;
    [SerializeField] Text EnemiesHitText;
    [SerializeField] Text PowerUpsCollectedText;
    [SerializeField] Text ProjectileTakenText;
    //string  Format("{0,4:0000}");


    [SerializeField] AudioSource SFXPlayer;
    [SerializeField] AudioClip[] EnemyImpactAudio;
    [SerializeField] AudioClip PowerUpSFX;
    [SerializeField] AudioClip GameOverSound;

    public bool GameOver;
    bool GameStopped;

    //stats
    public static int Score = 0;
    int PlayerShots = 0;
    int EnemiesHit = 0;
    int ProjectilesEaten = 0;
    int PowerUpsCollected = 0;



    private void OnEnable()
    {
        MMI_ActionListerner.OnBossDefeat += BossDefeated;
        MMI_ActionListerner.OnPlayerDeath += PlayerDeath;
        MMI_ActionListerner.OnShotFired += CountPlayerShots;

        MMI_ActionListerner.OnProjectilesTaken += CountPlayerProjectileColisions;
        MMI_ActionListerner.OnEnemiesHit += CountEnemiesHit;
        MMI_ActionListerner.OnWallProjCol += PlProjColWallAudio;

        MMI_ActionListerner.OnPowerupCollected += PowerUpsAction;
        
    }
    private void OnDisable()
    {
        MMI_ActionListerner.OnBossDefeat -= BossDefeated;
        MMI_ActionListerner.OnPlayerDeath -= PlayerDeath;
        MMI_ActionListerner.OnShotFired -= CountPlayerShots;


        MMI_ActionListerner.OnProjectilesTaken -= CountPlayerProjectileColisions;
        MMI_ActionListerner.OnEnemiesHit -= CountEnemiesHit;
        MMI_ActionListerner.OnWallProjCol -= PlProjColWallAudio;

        MMI_ActionListerner.OnPowerupCollected -= PowerUpsAction;
    }


    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
    }





    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameOver = false;
        EndGameUI.SetActive(false);

        PlayerShots = 0;

        Miliseconds = 0;
        Seconds = 0;
        Minutes = 0;
        Hours = 0;

        BuffDropTimer = 10f;


    }


    void Update()
    {
        InGameScoreText.text = string.Format(ScoreFormat, Score);

        if (GetInput.MenuInput && !GameStopped)
        {
            StatusText.text = "Status: Frenched out, L + Ratio";
            Score -= 15000;

            GameStopped = true;
            OnEscapePressed();
        }

        if ( GameOver && !GameStopped)
        {
            GameStopped = true;
            OnEscapePressed();
        }

        if(!GameStopped)
        HandleTimer();

        HandleBuffDrops();
    }


    //==========================================================================================
    // HANDLE GAME END UIs
    //==========================================================================================

    void HandleBuffDrops()
    {
        if (BuffDropTimer > 0)
        {
            BuffDropTimer -= Time.deltaTime;
        }

        if (BuffDropTimer <= 0f)
        {
            int rng = Random.Range(0, Buffs.Length);
            Instantiate(Buffs[rng], new Vector2(Random.Range(-9, 9), 12), transform.rotation);
            BuffDropTimer = 10;

        }


    }



    void HandleTimer ()
    {
        
        Miliseconds += Time.deltaTime * 100;
        if (Miliseconds >= 100f)
        {
            Miliseconds = 0;
            Seconds++;
            if (Seconds == 60)
            {
                Seconds = 0;
                Minutes++;
                if (Minutes == 60)
                {
                    Minutes = 0;
                    Hours++;
                }
            }

        }
       
       
        
          

    }


    void BossDefeated()
    {
        StatusText.text = "Status: Real Gamer, W";
        GameOver = true;
        Score += 25000;
        
    }
    void PlayerDeath()
    {
        StatusText.text = "Status: Skill Issue, L";
        GameOver = true;
        Score -= 5000;
        
    }

    void OnEscapePressed()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        EndGameUI.SetActive(true);

        Time.timeScale = 0f;

        Boss.SetActive(false);
        Player.SetActive(false);
        GameMaterial.SetActive(false);

        SFXPlayer.PlayOneShot(GameOverSound, 1f);
        


        //display all stuff.

        if (Score > 0)
        { 
            ScoreText.text = "Score: " + string.Format(ScoreFormat, Score); 
        }
        else
        {
            ScoreText.text = "Score: 0";
        }

        ShotsFiredText.text = "Shots fired: " + PlayerShots;
        
        float accuracy = PlayerShots > 0 ? (float)EnemiesHit / PlayerShots * 100 : 0;
        EnemiesHitText.text = "Enemies hit: " + EnemiesHit + " => " + $"{accuracy:00.00}%";

        ProjectileTakenText.text = "Projectiles taken: " + ProjectilesEaten;
        TimeText.text = "Time: " + string.Format(TimeFormat, Hours, Minutes, Seconds, Miliseconds);

        PowerUpsCollectedText.text = "Power-ups collected: " + PowerUpsCollected;

    }



    public void QuitButton()
    {
        Time.timeScale = 1f;
        LoadingScreen.LoadScene(0);

    }


    public void RestartButton()
    {
        Time.timeScale = 1f;
        LoadingScreen.LoadScene(1);
    }





    //==========================================================================================
    // ACTION EVENTS
    //==========================================================================================
    void PlProjColWallAudio()
    {
        AudioClip clip = EnemyImpactAudio[Random.Range(0, EnemyImpactAudio.Length)];
        SFXPlayer.PlayOneShot(clip, 0.25f);

        Score -= 10;
    }

    void PowerUpsAction()
    {
        SFXPlayer.PlayOneShot(PowerUpSFX, 0.5f);
        Score += 500;
        PowerUpsCollected++;
    }

    void CountPlayerShots()
    {
        PlayerShots++;
        Score += 5;
    }
    void CountPlayerProjectileColisions()
    {
        ProjectilesEaten++;
    }
    void CountEnemiesHit()
    {
        Score += 25;
        EnemiesHit++;
    }

}
