using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using TMPro;

public class THU_Drifter_UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StartMenuPanel;
    [SerializeField] private GameObject GamePlayPanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button MainMenuButton;

    [SerializeField] private Image powerUpImage;

    [SerializeField] private Slider BoostSlider;

    private float MaxBoostEnergy = 100f;
    private float CurrentBoostEnergy;

    private bool isPaused = false;
    private Button[] buttons;
    private int currentButtonIndex = 0;

    private static bool isRestarting = false;

    [SerializeField] private TMP_Text timerText;
    private float elapsedTime = 0f;

    [SerializeField] private TMP_Text winScoreText;
    [SerializeField] private TMP_Text loseScoreText;

    private int winFinalScore = 0;
    private int loseFinalScore = 0;

    [SerializeField] private TMP_Text liveScoreText;
    private int currentScore = 0;

    [SerializeField] private TMP_Text countdownText;
    private bool isCountdownActive = false;

    public static THU_Drifter_UIManager Instance { get; private set; }

    public bool isGameRunning = true;


    void Start()
    {
        CurrentBoostEnergy = MaxBoostEnergy;
        UpdateBoostSlider();

        if (isRestarting)
        {
            StartMenuPanel?.SetActive(false);
            GamePlayPanel?.SetActive(true);
            PausePanel?.SetActive(false);
            WinPanel?.SetActive(false);
            LosePanel?.SetActive(false);
            Time.timeScale = 1.0f;

            StartCoroutine(StartCountdown());
            isRestarting = false;
        }
        else
        {
            StartMenuPanel?.SetActive(true);
            GamePlayPanel?.SetActive(false);
            PausePanel?.SetActive(false);
            WinPanel?.SetActive(false);
            LosePanel?.SetActive(false);
            Time.timeScale = 0f;
        }

        if (startButton != null) startButton.onClick.AddListener(StartGame);
        if (resumeButton != null) resumeButton.onClick.AddListener(Resume);
        if (playAgainButton != null) playAgainButton.onClick.AddListener(PlayAgain);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
        if (MainMenuButton != null) MainMenuButton.onClick.AddListener(BackToMainMenu);

        buttons = new Button[] { startButton, resumeButton, playAgainButton, MainMenuButton, quitButton };
        SetSelectedButton(startButton);
    }

    void Update()
    {
        if (isGameRunning && !isCountdownActive)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
            UpdateScoreUI(elapsedTime);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseMenu();
            else
                Resume();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isPaused)
                PauseMenu();
            else
                Resume();
        }

        if (StartMenuPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                NavigateMenu(Input.GetKeyDown(KeyCode.UpArrow), new Button[] { startButton });
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                buttons[currentButtonIndex]?.onClick.Invoke();
            }
        }
        else if (isPaused || WinPanel?.activeSelf == true || LosePanel?.activeSelf == true)
        {
            if (buttons.Length > 1 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)))
            {
                NavigateMenu(Input.GetKeyDown(KeyCode.UpArrow));
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                buttons[currentButtonIndex]?.onClick.Invoke();
            }
        }
    }

    private void NavigateMenu(bool isUpArrow, Button[] menuButtons = null)
    {
        Button[] activeButtons = menuButtons ?? buttons;

        currentButtonIndex += isUpArrow ? -1 : 1;

        if (currentButtonIndex < 0) currentButtonIndex = activeButtons.Length - 1;
        if (currentButtonIndex >= activeButtons.Length) currentButtonIndex = 0;

        SetSelectedButton(activeButtons[currentButtonIndex]);
    }

    public void StartGame()
    {
        StartMenuPanel?.SetActive(false);
        GamePlayPanel?.SetActive(true);
        PausePanel?.SetActive(false);
        WinPanel?.SetActive(false);
        LosePanel?.SetActive(false);

        Time.timeScale = 0f;
        isGameRunning = true;
        elapsedTime = 0f;

        if (!isCountdownActive)
        {
            StartCoroutine(StartCountdown());
        }

        buttons = new Button[] { resumeButton, playAgainButton, quitButton };
        currentButtonIndex = 0;
        SetSelectedButton(resumeButton);
    }

    public void WinScreen()
    {
        GamePlayPanel?.SetActive(false);
        WinPanel?.SetActive(true);
        Time.timeScale = 0f;
        isGameRunning = false;

        winFinalScore = Mathf.FloorToInt(elapsedTime * 5f) * 2;

        if (winScoreText != null)
        {
            winScoreText.text = " " + winFinalScore.ToString();
        }
        else
        {
            Debug.LogWarning("Win Score Text is not assigned in the inspector!");
        }

        buttons = new Button[] { playAgainButton, MainMenuButton };
        currentButtonIndex = 0;
        SetSelectedButton(playAgainButton);
    }

    public void LoseScreen()
    {
        GamePlayPanel?.SetActive(false);
        LosePanel?.SetActive(true);
        Time.timeScale = 0f;
        isGameRunning = false;

        loseFinalScore = Mathf.Max(0, Mathf.FloorToInt(elapsedTime * 5f) / 2);
        if (loseScoreText != null)
        {
            loseScoreText.text = " " + loseFinalScore.ToString();
        }
        else
        {
            Debug.LogWarning("Lose Score Text is not assigned in the inspector!");
        }

        buttons = new Button[] { playAgainButton, MainMenuButton };
        currentButtonIndex = 0;
        SetSelectedButton(playAgainButton);
    }

    public void PauseMenu()
    {
        GamePlayPanel?.SetActive(false);
        PausePanel?.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        buttons = new Button[] { resumeButton, playAgainButton, MainMenuButton };
        currentButtonIndex = 0;
        SetSelectedButton(resumeButton);
    }

    public void Resume()
    {
        GamePlayPanel?.SetActive(true);
        PausePanel?.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }

    public void PlayAgain()
    {
        isRestarting = true;
        isGameRunning = false;
        isCountdownActive = true;

        FindObjectOfType<THU_PlayerMovement>().CanMove = false;
        foreach (var enemy in FindObjectsOfType<THU_EnemyMovement>())
        {
            enemy.ResetEnemy();
            enemy.CanMove = false;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 0f;
        StartCoroutine(StartCountdown());
    }

    public void BackToMainMenu()
    {
        isGameRunning = false;
        isCountdownActive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartMenuPanel?.SetActive(true);
        GamePlayPanel?.SetActive(false);
        PausePanel?.SetActive(false);
        WinPanel?.SetActive(false);
        LosePanel?.SetActive(false);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    private void SetSelectedButton(Button button)
    {
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
    }

    public void SetBoostEnergy(float energy)
    {
        CurrentBoostEnergy = Mathf.Clamp(energy, 0, MaxBoostEnergy);
        Debug.Log($"Boost Energy Set: {CurrentBoostEnergy}/{MaxBoostEnergy}");
        UpdateBoostSlider();
    }

    private void UpdateBoostSlider()
    {
        if (BoostSlider != null)
        {
            StartCoroutine(SmoothSliderUpdate(BoostSlider, CurrentBoostEnergy / MaxBoostEnergy));
        }
        else
        {
            Debug.LogWarning("BoostSlider is not assigned in the inspector!");
        }
    }

    private IEnumerator SmoothSliderUpdate(Slider slider, float targetValue)
    {
        float initialValue = slider.value;
        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(initialValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = targetValue;
    }

    public float GetCurrentBoostEnergy()
    {
        return CurrentBoostEnergy;
    }

    public float GetMaxBoostEnergy()
    {
        return MaxBoostEnergy;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePowerUpUI(Sprite powerUpIcon)
    {
        if (powerUpImage != null)
        {
            if (powerUpIcon != null)
            {
                powerUpImage.sprite = powerUpIcon;
                powerUpImage.enabled = true;
                Debug.Log($"Power-Up UI updated with icon: {powerUpIcon.name}");
            }
            else
            {
                Debug.LogWarning("Power-Up icon is null. Clearing UI.");
                ClearPowerUpUI();
            }
        }
        else
        {
            Debug.LogWarning("PowerUpImage is not assigned in the inspector!");
        }
    }

    public void ClearPowerUpUI()
    {
        if (powerUpImage != null)
        {
            powerUpImage.sprite = null;
            powerUpImage.enabled = false;
        }
    }


    public void TriggerWin()
    {
        Debug.Log("Player won the game.");
        WinScreen();
    }

    public void TriggerLoss()
    {
        Debug.Log("Player lost the game.");
        LoseScreen();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            float minutes = Mathf.FloorToInt(elapsedTime / 60);
            float seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogWarning("Timer Text (TMP) is not assigned in the inspector!");
        }
    }

    public void UpdateScoreUI(float timeElapsed)
    {
        float timeMultiplier = 5f;
        currentScore = Mathf.FloorToInt(timeElapsed * timeMultiplier);

        if (liveScoreText != null)
        {
            liveScoreText.text = " " + currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("Live Score Text (TMP) is not assigned in the inspector!");
        }
    }

    public void OnLapCompleted()
    {
        UpdateScoreUI(100);
        UpdateLiveScoreUI();
        Debug.Log("Lap completed! Current Score: " + currentScore);
    }

    private IEnumerator StartCountdown()
    {
        int countdownValue = 3;
        isCountdownActive = true;
        isGameRunning = false;

        FindObjectOfType<THU_PlayerMovement>().CanMove = false;
        foreach (var enemy in FindObjectsOfType<THU_EnemyMovement>())
        {
            enemy.CanMove = false;
        }

        while (countdownValue > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = countdownValue.ToString();
            }
            countdownValue--;
            yield return new WaitForSecondsRealtime(1f);
        }

        if (countdownText != null)
        {
            countdownText.text = "";
        }

        isCountdownActive = false;
        isGameRunning = true;
        Time.timeScale = 1f;

        FindObjectOfType<THU_PlayerMovement>().CanMove = true;
        foreach (var enemy in FindObjectsOfType<THU_EnemyMovement>())
        {
            enemy.CanMove = true;
        }
    }

    private void UpdateLiveScoreUI()
    {
        if (liveScoreText != null)
        {
            liveScoreText.text = " " + currentScore.ToString();
        }
        else
        {
            Debug.LogWarning("Live Score Text (TMP) is not assigned in the inspector!");
        }
    }

}
