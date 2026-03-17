using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CYU_GameManager : MonoBehaviour
{
    InputSubscription CYU_Input;

    [SerializeField] private CYU_Obstacle CYU_obstaclePrefab;
    [SerializeField] private CYU_Collectible CYU_collectiblePrefab;
    [SerializeField] private CYU_HealthPowerup CYU_healthPrefab;
    [SerializeField] private CYU_PlayerInput CYU_playerInput;

    [SerializeField] AudioSource CYU_playerHurt;
    [SerializeField] AudioSource CYU_asteroidDestroy;
    [SerializeField] AudioSource CYU_collectibleObtained;
    [SerializeField] AudioSource CYU_music;

    public Text CYU_scoreText;
    public Text CYU_levelText;
    public Text CYU_healthText;

    public int CYU_obstacleCount = 0;
    public int CYU_collectibleCount = 0;
    private int CYU_gravimaticLevel = 0;

    public int CYU_playerHealth = 3;
    public int CYU_score = 0;

    private float CYU_scoreTime = 0f;
    private float CYU_timerIncrement = 0.1f;

    private bool CYU_runUpdate = true;

    // Start is called before the first frame update
    void Awake()
    {
        CYU_Input = GetComponent<InputSubscription>();

        CYU_music.loop = true;
        CYU_music.volume = 0.3f;
        CYU_music.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        /////////////Added by Izzy///////////////////
        if (CYU_Input.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
        ////////////////////////////////
        ///
        // Check if the update function is allowed to run (see ResetLevel function)
        if (CYU_runUpdate == true)
        {
            // Runs the function that will increment the score according to how long the player has survived
            CYU_AddTimeScore();

            // Checks if there are no more obstacles left in the scene
            if (CYU_obstacleCount == 0)
            {
                // Increment the level
                CYU_gravimaticLevel++;
                CYU_levelText.text = "LEVEL " + CYU_gravimaticLevel.ToString();

                // Makes game harder over time by increasing number of obstacles spawned on current level (higher level = more stuff to avoid)
                int CYU_numOfObstacles = 1 + (1 * CYU_gravimaticLevel);
                int CYU_numOfCollectibles = Mathf.RoundToInt(CYU_gravimaticLevel / 3);

                // Runs a loop that spawns however many obstacles are needed in current level (see SpawnObject func for spawn details)
                for (int i = 0; i < CYU_numOfObstacles; i++)
                {
                    CYU_SpawnObstacle();
                }
                for (int i = 0; i < CYU_numOfCollectibles; i++)
                {
                    float CYU_randomPickup = Random.Range(0f, 1f);
                    if ( CYU_randomPickup < 0.85)
                    {
                        CYU_SpawnCollectible();
                    }
                    else if ( CYU_randomPickup > 0.85)
                    {
                        CYU_SpawnHealth();
                    }
                    else
                    {
                        CYU_SpawnCollectible();
                    }
                }
            }
        }
    }

    // Method for spawning obstacles
    private void CYU_SpawnObstacle()
    {
        // Gives the obstacle being spawned a random position off screen
        // The X axis is given a random range to mimic random spawn times, this may be changed later to an actual random time spawner
        Vector2 CYU_worldSpawnPosition = new Vector2(Random.Range(20f, 60f), Random.Range(-10f, 10f));

        // Instantiates the obstacle (see script "CYU_Obstacle" for obstacle behaviour)
        CYU_Obstacle CYU_Obstacle = Instantiate(CYU_obstaclePrefab, CYU_worldSpawnPosition, Quaternion.identity);

        // Sets the instantiated obstacle's gamemanager variable to "this". Used for when it needs to be destroyed
        CYU_Obstacle.CYU_gameManager = this;
        
    }

    private void CYU_SpawnCollectible()
    {
        // Gives the collectible being spawned a random position off screen
        // The X axis is given a random range to mimic random spawn times, this may be changed later to an actual random time spawner
        Vector2 CYU_worldSpawnPosition = new Vector2(Random.Range(20f, 60f), Random.Range(-10f, 10f));

        // Instantiates the obstacle (see script "CYU_Obstacle" for obstacle behaviour)
        CYU_Collectible CYU_Collectible = Instantiate(CYU_collectiblePrefab, CYU_worldSpawnPosition, Quaternion.identity);

        // Sets the instantiated obstacle's gamemanager variable to "this". Used for when it needs to be destroyed
        CYU_Collectible.CYU_gameManager = this;
    }

    private void CYU_SpawnHealth()
    {
        // Gives the collectible being spawned a random position off screen
        // The X axis is given a random range to mimic random spawn times, this may be changed later to an actual random time spawner
        Vector2 CYU_worldSpawnPosition = new Vector2(Random.Range(20f, 60f), Random.Range(-10f, 10f));

        // Instantiates the obstacle (see script "CYU_Obstacle" for obstacle behaviour)
        CYU_HealthPowerup CYU_HealthPowerup = Instantiate(CYU_healthPrefab, CYU_worldSpawnPosition, Quaternion.identity);

        // Sets the instantiated obstacle's gamemanager variable to "this". Used for when it needs to be destroyed
        CYU_HealthPowerup.CYU_gameManager = this;
    }

    // Reset level handles checking player health and restarting the scene
    public void CYU_ResetLevel()
    {

        CYU_playerHurt.Play();
        CYU_asteroidDestroy.Play();

        // Decrement Health by 1
        CYU_playerHealth -= 1;
        CYU_healthText.text = "HEALTH: " + CYU_playerHealth.ToString();

        // Checks for if the player has lost all health
        if (CYU_playerHealth == 0)
        {
            // Disable the update function from running, and force the gameobject to persist after gameover screen loads
            CYU_runUpdate = false;
            CYU_music.loop = false;
            CYU_music.Stop();
            Object.DontDestroyOnLoad(this);
            // Load into Game Over Screen
            SceneManager.LoadScene("CYU_GameOver");
        }
    }

    // Handles adding score based off of time (see AddCollectibleScore for the other half of the score system)
    private void CYU_AddTimeScore()
    {
        CYU_scoreTime += Time.deltaTime;

        if (CYU_scoreTime >= CYU_timerIncrement)
        {
            CYU_score++;
            CYU_scoreTime -= CYU_timerIncrement;
            CYU_scoreText.text = CYU_score.ToString("D4");
        }
    }

    // Adds 100 points to the current score
    public void CYU_CollectibleScore()
    {
        CYU_collectibleObtained.Play();
        CYU_score = CYU_score + 100;
    }

    public void CYU_AddHealth()
    {
        if (CYU_playerHealth < 3)
        {
            CYU_playerHealth++;
            CYU_healthText.text = "HEALTH: " + CYU_playerHealth.ToString();
        }
        else
        {
            CYU_score += 100;
        }
    }
}
