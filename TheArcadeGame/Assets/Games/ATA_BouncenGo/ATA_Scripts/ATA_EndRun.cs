using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ATA_EndRun : MonoBehaviour
{
    public GameObject liveDis;
    public GameObject endScreen;
    public Button quitButton;
    public Button restartButton;

    private Button[] buttons; // Array to hold the buttons
    private int selectedButtonIndex = 0; // Index of the currently selected button
    private InputSubscription input; // Reference to your input subscription script
    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip buttonSelectSound; // Reference to the sound to play

    private float navigationCooldown = 0.2f; // Cooldown duration in seconds
    private float nextNavigationTime = 0f; // Time when navigation is allowed again

    // Reference to ATA_Score script to access stars
    private ATA_Score scoreScript;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = FindObjectOfType<ATA_Score>(); // Get the ATA_Score script

        StartCoroutine(EndSeq());

        // Initialize buttons and input system
        buttons = new Button[] { quitButton, restartButton };

        // Assign button click events to load different scenes
        quitButton.onClick.AddListener(() => LoadScene(0)); // Quit button loads Scene 0
        restartButton.onClick.AddListener(() => SceneManager.LoadScene("ATA_SceneStart")); // Restart button loads Scene 1

        HighlightButton(selectedButtonIndex); // Highlight the first button by default

        input = FindObjectOfType<InputSubscription>(); // Ensure InputSubscription exists in your scene
    }

    void Update()
    {
        if (input == null) return;

        HandleNavigation();
        HandleSelection();
    }

    IEnumerator EndSeq()
    {
        yield return new WaitForSeconds(1);
        liveDis.SetActive(false);
        endScreen.SetActive(true);

        // Update stars on the end screen
        if (scoreScript != null)
        {
            scoreScript.UpdateStars(); // Ensure stars are updated at the end
        }
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
        // Update the selected button index and wrap around if necessary
        selectedButtonIndex = (selectedButtonIndex + direction + buttons.Length) % buttons.Length;
        HighlightButton(selectedButtonIndex);
    }

    private void HighlightButton(int index)
    {
        // Custom highlight method: Adjust button appearance (scale and color)
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

        // Play the sound when the selection changes
        if (audioSource != null && buttonSelectSound != null)
        {
            audioSource.PlayOneShot(buttonSelectSound); // Play the sound
        }
    }

    private void LoadScene(int sceneIndex)
    {
        // Load the specified scene by index
        SceneManager.LoadScene(sceneIndex);
    }
}
