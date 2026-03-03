using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class EBD_GameOverManager : MonoBehaviour
{
    [Header("Game Over UI Elements")]
    public GameObject liveDisplay;
    public GameObject endScreen;
    public Button quitButton;
    public Button restartButton;
    private Button[] buttons; // Array to hold the buttons for navigation
    private int selectedButtonIndex = 0; // Index of the currently selected button
    private InputSubscription input; // Reference to your input subscription script

    [Header("Player and Level Control")]
    public GameObject player; // The player GameObject
    public GameObject levelControl; // Level management GameObject

    [Header("Game Over Settings")]
    public float gameOverDelay = 1.0f; // Delay before showing game-over screen
    private bool isGameOver = false;

    [Header("Timer Settings")]
    public float gameDuration = 60f; // Total time for the game
    private float remainingTime;
    public Text timerDisplay; // Text UI to show remaining time

    [Header("Stars GameObjects")]
    public GameObject starPrefab; // The prefab for the star GameObject
    public Transform starsParent; // Parent object where stars will be placed
    private GameObject[] stars; // Array to hold star GameObjects
    private int starsAwarded;

    private float finalDistance;

    [Header("Final Distance Text")]
    public Text finalDistanceText; // Reference to the UI Text for displaying final distance

    [Header("Navigation Cooldown")]
    public float navigationCooldown = 0.2f; // Cooldown for button navigation
    private float nextNavigationTime = 0f;

    private void Start()
    {
        // Initialize timer and UI
        remainingTime = gameDuration;

        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);

        buttons = new Button[] { quitButton, restartButton };
        HighlightButton(selectedButtonIndex);
        input = FindObjectOfType<InputSubscription>(); // Ensure InputSubscription exists in your scene
    

        if (endScreen != null)
        {
            endScreen.SetActive(false); // Hide end screen initially
        }

    }

    private void Update()
    {
        if (input.MenuInput)
        {
            SceneManager.LoadScene(0);
        }

        if (input == null) return;

        HandleNavigation();
        HandleSelection();

        if (!isGameOver)
        {
            HandleTimer(); // Update the timer if the game isn't over
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver) return;

        // Check if the collision is with an obstacle
        if (other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("Obstacle"))
        {
            TriggerGameOver();
        }
    }

    private void HandleTimer()
    {
        remainingTime -= Time.deltaTime;

        if (timerDisplay != null)
        {
            timerDisplay.text = Mathf.CeilToInt(remainingTime).ToString(); // Update the UI with the remaining time
        }

        if (remainingTime <= 0)
        {
            remainingTime = 0; // Prevent negative time
            TriggerGameOver(); // Trigger game over when time runs out
        }
    }


    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        
        // Disable player movement and calculate the final distance covered
        if (player != null)
        {
            var playerMovement = player.GetComponent<EBD_BikeController>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            var distanceScript = levelControl.GetComponent<EBD_DistanceCovered>();
            if (distanceScript != null)
            {
                finalDistance = distanceScript.GetDistanceCovered();
                // Determine stars based on the final distance

            }
        }

        StartCoroutine(GameOverSequence());
        
    }

    private IEnumerator GameOverSequence()
    {
        yield return new WaitForSeconds(gameOverDelay);

        // Update and display the final distance
        if (finalDistanceText != null)
        {
            finalDistanceText.text = "Final Distance: " + finalDistance.ToString("F2") + " meters";
            Time.timeScale = 0.0f;
        }



        if (liveDisplay != null) liveDisplay.SetActive(false);
        if (endScreen != null) endScreen.SetActive(true);
    }



    private void HandleNavigation()
    {
        if (Time.time < nextNavigationTime) return; // Prevent navigation if still in cooldown

        // Use the left and right arrow keys for navigation
        if (input.NormalizedMovementInput.x > 0.5f) // Right arrow key
        {
            ChangeSelection(1);
            nextNavigationTime = Time.time + navigationCooldown; // Set cooldown
        }
        else if (input.NormalizedMovementInput.x < -0.5f) // Left arrow key
        {
            ChangeSelection(-1);
            nextNavigationTime = Time.time + navigationCooldown; // Set cooldown
        }
    }

    private void HandleSelection()
    {
        if (input.ConfirmInput) // Check if the confirm button (Enter) is pressed
        {
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }

    private void ChangeSelection(int direction)
    {
        selectedButtonIndex = (selectedButtonIndex + direction + buttons.Length) % buttons.Length;
        HighlightButton(selectedButtonIndex);
    }

    private void HighlightButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index)
            {
                // Highlight the selected button
                buttons[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // Enlarge the button
                var colors = buttons[i].colors;
                colors.normalColor = Color.yellow; // Change the button color to yellow
                buttons[i].colors = colors;
            }
            else
            {
                // Reset other buttons
                buttons[i].transform.localScale = new Vector3(1f, 1f, 1f); // Reset the scale
                var colors = buttons[i].colors;
                colors.normalColor = Color.white; // Change the button color back to white
                buttons[i].colors = colors;
            }
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        Debug.Log("Quitting the game...");
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        Debug.Log("Restarting the game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
