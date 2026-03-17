using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Creating a class of racers to have in inspector & to make list out of
[System.Serializable]
public class Racer
{
    // Holds information for each racer, what they are & where they are
    public string name;
    public GameObject racerObject;
    public int currentCheckpoint = 0;
    public int currentLap = 0;
    public int finalPlacement = 0;
    public float distanceToNextCheckpoint;
    public float raceTime = 0;
    public float finalRaceTime = 0;
    public bool hasRacerFinished = false;
    public bool onlytriggerOnce = false;
}

public class WHA_RaceManager : MonoBehaviour
{
    [Header("Racers")]
    // Making a list out of the racers
    public List<Racer> racers = new List<Racer>();

    [Header("Checkpoints")]
    public List<Transform> checkpoints = new List<Transform>(); // List of checkpoints
    public int totalLaps = 3; // Number of laps

    [Header("Placement")]
    public GameObject racePlacementObj;
    public TMP_Text racePlacement;
    public GameObject stText;
    public GameObject ndText;
    public GameObject rdText;
    public GameObject thText;
    public TMP_Text lapCounterText;

    [Header("Leaderboard")]
    public GameObject leaderboardPanel; // Reference to the leaderboard panel
    public Transform leaderboardContent; // Reference to the Vertical Layout Group
    public GameObject leaderboardEntryPrefab; // Prefab for displaying each racer's placement
    public bool hasPlayerFinished = false;
    public bool isPlayerInFirst = false;

    [Header("Leaderboard UI")]
    public GameObject leaderboardButtons;

    [Header("Coutdown UI")]
    public TMP_Text countdownText;
    public float countdownTime = 3f;
    [HideInInspector] public bool raceStarted = false;

    [Header("Trophy Stuff")]
    private WHA_TrophyManager trophyMan;

    private void Start()
    {
        trophyMan = GetComponent<WHA_TrophyManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        leaderboardPanel.SetActive(false);
        leaderboardButtons.SetActive(false);

        UpdateLapCounter();
        StartCoroutine(StartCountdown());

        stText.SetActive(false);
        ndText.SetActive(false);
        rdText.SetActive(false);
        thText.SetActive(true);
    }

    #region Countdown

    IEnumerator StartCountdown()
    {
        // Show countdown text
        countdownText.gameObject.SetActive(true);

        for (int i = (int)countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // Start the race
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);

        raceStarted = true; // Allow the race to start
    }

    #endregion

    void Update()
    {
        if (!raceStarted)
            return;

        // Update racers' distances to their next checkpoint for sorting
        foreach (var racer in racers)
        {
            racer.raceTime += Time.deltaTime;

            int nextCheckpointIndex = (racer.currentCheckpoint + 1) % checkpoints.Count;
            Transform nextCheckpoint = checkpoints[nextCheckpointIndex];
            racer.distanceToNextCheckpoint = Vector3.Distance(racer.racerObject.transform.position, nextCheckpoint.position);

            if (racer.currentLap >= totalLaps)
            {
                racer.hasRacerFinished = true;

                if (racer.hasRacerFinished && !racer.onlytriggerOnce)
                {
                    // Update the racer's placement and time when they finish
                    racer.finalPlacement = racers.IndexOf(racer) + 1;
                    racer.finalRaceTime = Mathf.Round(racer.raceTime * 100f) / 100f;

                    racer.onlytriggerOnce = true;
                }

                // Update the leaderboard in real-time
                UpdateLeaderboard();
            }

            if (racer.name == "The Player" && racer.currentLap >= totalLaps)
            {
                hasPlayerFinished = true;
            }
        }

        // Sort racers by laps completed, checkpoint progress, and distance to next checkpoint
        racers = racers.OrderByDescending(r => r.currentLap)
                       .ThenByDescending(r => r.currentCheckpoint)
                       .ThenBy(r => r.distanceToNextCheckpoint)
                       .ToList();

        //
        // This little if statement is needed to trigger the winning bool
        // This bool is needed for the arcade trophy stuff
        //
        var player = racers.FirstOrDefault(r => r.name == "The Player");
        if (player != null)
        {
            int playerIndex = racers.IndexOf(player);

            isPlayerInFirst = (playerIndex == 0);

            if (hasPlayerFinished)
            {
                trophyMan.playerWinSet((playerIndex == 0));
            }
            
        }

        UpdatePlayerPosition();
    }

    // Check if checkpoint has been triggered
    public void CheckpointTriggered(GameObject racerObject, Transform checkpoint)
    {
        // Finds the racer that triggered the checkpoint
        Racer racer = racers.FirstOrDefault(r => r.racerObject == racerObject);
        if (racer == null) return;

        // Finds out what checkpoint was triggered
        int checkpointIndex = checkpoints.IndexOf(checkpoint);

        // Determines next checkpoint
        // If the racer triggered the correct checkpoint
        if (checkpointIndex == (racer.currentCheckpoint + 1) % checkpoints.Count)
        {
            racer.currentCheckpoint++;

            // If the racer completed all checkpoints, increase their lap
            if (racer.currentCheckpoint >= checkpoints.Count)
            {
                racer.currentCheckpoint = 0;
                racer.currentLap++;

                if (racer.name == "The Player")
                {
                    UpdateLapCounter(); // Update lap counter UI for the player
                }

                if (racer.currentLap >= totalLaps) // When the racer’s lap is the total laps, they win
                {
                    Debug.Log($"{racer.name} has finished the race!");

                    racer.finalPlacement = racers.IndexOf(racer) + 1;

                    // Stop timer for finished racer
                    racer.raceTime = Mathf.Round(racer.raceTime * 100f) / 100f; // Optional: round to 2 decimal places
                }
            }
        }
    }

    // Respawn the racer at the last checkpoint
    public void RespawnAtLastCheckpoint(GameObject racerObject)
    {
        // Find the racer
        Racer racer = racers.FirstOrDefault(r => r.racerObject == racerObject);
        if (racer == null) return;

        // Ensure the racer has triggered at least one checkpoint
        if (racer.currentCheckpoint > 0)
        {
            // Get the position of the last triggered checkpoint
            Transform lastCheckpoint = checkpoints[racer.currentCheckpoint];

            // Move the racer back to the last checkpoint position
            racer.racerObject.transform.position = lastCheckpoint.position;
            racer.racerObject.transform.rotation = lastCheckpoint.rotation;  // Optional: Ensure the racer faces the correct direction

            Debug.Log($"{racer.name} has respawned at checkpoint {racer.currentCheckpoint}");
        }
        else
        {
            // If the racer hasn't triggered any checkpoint, respawn at the start (checkpoint 0)
            Transform startCheckpoint = checkpoints[0];
            racer.racerObject.transform.position = startCheckpoint.position;
            racer.racerObject.transform.rotation = startCheckpoint.rotation;

            Debug.Log($"{racer.name} has respawned at the start line.");
        }
    }

    void UpdatePlayerPosition()
    {
        // Find the player in the racers list
        Racer playerRacer = racers.FirstOrDefault(r => r.name == "The Player");
        if (playerRacer != null)
        {
            // Find the player's position in the race (1-based index)
            int playerPosition = racers.IndexOf(playerRacer) + 1;

            // Display the player's position in the TMP_Text
            if (hasPlayerFinished)
            {
                racePlacementObj.SetActive(false);
                lapCounterText.enabled = false;
            }
            else
            {
                racePlacementObj.SetActive(true);
                racePlacement.text = $"{playerPosition}";
                lapCounterText.enabled = true;
            }
        }

        if (racePlacement.text == "1")
        {
            stText.SetActive(true);
            ndText.SetActive(false);
            rdText.SetActive(false);
            thText.SetActive(false);
        }
        else if (racePlacement.text == "2")
        {
            stText.SetActive(false);
            ndText.SetActive(true);
            rdText.SetActive(false);
            thText.SetActive(false);
        }
        else if (racePlacement.text == "3")
        {
            stText.SetActive(false);
            ndText.SetActive(false);
            rdText.SetActive(true);
            thText.SetActive(false);
        }
        else if (racePlacement.text == "4")
        {
            stText.SetActive(false);
            ndText.SetActive(false);
            rdText.SetActive(false);
            thText.SetActive(true);
        }
    }

    // Show individual racer on the leaderboard once they've finished
    // Show the leaderboard with all racers once the player finishes
    public void UpdateLeaderboard()
    {
        // Only show the leaderboard after the player finishes
        if (hasPlayerFinished)
        {
            leaderboardPanel.SetActive(true);
            leaderboardButtons.SetActive(true);
        }

        // Sort racers by FinalPlacement (position)
        var sortedRacers = racers.OrderBy(r => r.finalPlacement).ToList();

        // Clear previous leaderboard entries
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new leaderboard entry for each racer that has finished
        foreach (var finishedRacer in sortedRacers)
        {
            if (finishedRacer.finalPlacement == 0) continue; // Skip racers who haven't finished yet

            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContent);

            TMP_Text placementText = entry.transform.Find("Racer").GetComponent<TMP_Text>();
            TMP_Text raceTimeText = entry.transform.Find("RaceTime").GetComponent<TMP_Text>();

            if (placementText != null)
            {
                string positionSuffix = GetPositionSuffix(finishedRacer.finalPlacement);
                placementText.text = $"{finishedRacer.finalPlacement}{positionSuffix}: {finishedRacer.name}";
            }

            if (raceTimeText != null)
            {
                string formattedTime = FormatTime(finishedRacer.finalRaceTime);
                raceTimeText.text = formattedTime;
            }
        }
    }

    private string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    private string GetPositionSuffix(int position)
    {
        if (position % 10 == 1 && position % 100 != 11) return "st";
        if (position % 10 == 2 && position % 100 != 12) return "nd";
        if (position % 10 == 3 && position % 100 != 13) return "rd";
        return "th";
    }

    void UpdateLapCounter()
    {
        // Find the player's racer object
        Racer playerRacer = racers.FirstOrDefault(r => r.name == "The Player");
        if (playerRacer != null)
        {
            // Update the lap counter text
            lapCounterText.text = $"Lap: {playerRacer.currentLap + 1}/{totalLaps}";
        }
    }
}