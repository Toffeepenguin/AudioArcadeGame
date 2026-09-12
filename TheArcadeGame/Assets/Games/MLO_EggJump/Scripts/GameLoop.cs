using FMODUnity;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    public int level = 0;
    public int score = 0;
    public int high_score = 0;
    public int trophy_score;
    private bool started = false;
    private bool wait = true;
    private float wait_count = 0f;
    private bool ended = true;
    public GameObject player;

    [SerializeField] private PlatformHandler platform_handler; 
    [SerializeField] private Movement movement_script;
    [SerializeField] private Button start_button;
    [SerializeField] private Trophy trophy_script;
    [SerializeField] private GameObject land_particle_prefab;
    private readonly List<ParticleSystem> particles = new();
    private int current_particle_index;

    private float lerp_count = 0f;

    [Header("UI Group References")]
    [SerializeField] private CanvasGroup gameplay_group;
    [SerializeField] private CanvasGroup menu_group; 
    [SerializeField] private MLO_TransitionScript transition_UI_script;

    [Header("Input System")]
    [SerializeField] private InputActionAsset input_actions;
    [SerializeField] private Selectable initial_ui_element;

    public EventReference FMOD_level_sound;
    public EventReference FMOD_coin_sound;

    [SerializeField] private UnityEvent GameStarted;
    [SerializeField] private UnityEvent GameBegan;
    [SerializeField] private UnityEvent GameEnded;
    [SerializeField] private UnityEvent GetScore;
    [SerializeField] private UnityEvent GetLevel;
    [SerializeField] private UnityEvent GetTrophy;
    [SerializeField] private UnityEvent GetHelp;

    [HideInInspector] public float speed_multiplier => 2.5f * Mathf.Pow(1.1f, level);

    private void Start()
    {
        transition_UI_script.gameObject.SetActive(true);
        if (land_particle_prefab != null) for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(land_particle_prefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity);
            if (obj.TryGetComponent(out ParticleSystem ps)) particles.Add(ps);
        }
        current_particle_index = 0;
        input_actions.FindActionMap("UI")?.Enable();
        FocusInitialUI();
    }

    public void StartGame()
    {
        GameStarted.Invoke(); 
        started = true;
        ended = false;
        lerp_count = 0f;
        FMODAudioUtilsObject.Get3DAttRef(FMOD_coin_sound, player);
        start_button.interactable = false;

        menu_group.blocksRaycasts = false;
        menu_group.interactable = false;

        input_actions.FindActionMap("UI")?.Disable();
        EventSystem.current.SetSelectedGameObject(null);
        movement_script.EnableGameplayInputs();
    }

    public void BeginGame()
    {
        wait = false;
        GameBegan.Invoke();
    }

    public void EndGame()
    {
        started = false;
        if (high_score < score) high_score = score;
        score = 0;
        level = 0;
        ended = true;
        wait = true;
        wait_count = 0f;
        start_button.interactable = true;
        GameEnded.Invoke();

        menu_group.blocksRaycasts = true;
        menu_group.interactable = true;

        movement_script.DisableGameplayInputs();
        input_actions.FindActionMap("UI")?.Enable();
        FocusInitialUI();
    }

    private void FocusInitialUI()
    {
        if (initial_ui_element != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(initial_ui_element.gameObject);
        }
    }

    public void IncreaseScoreLevel(Vector3 platform_position)
    {
        score++;
        if (score == trophy_score)
        {
            GetTrophy.Invoke();
            trophy_script.TriggerTrophy(movement_script.transform);
        }
        GetScore.Invoke();
        if (particles.Count > 0)
        {
            current_particle_index = (current_particle_index + 1) % particles.Count;
            ParticleSystem ps = particles[current_particle_index];
            ps.transform.position = platform_position;
            ps.Clear();
            ps.Emit(1);
        }
        while (score >= 15f * Mathf.Pow(1.8f, level)) 
        {
            FMODAudioUtilsObject.Get3DAttRef(FMOD_level_sound, player);
            level++;
            GetLevel.Invoke();
        }
    }

    public void Menu()
    {
        transition_UI_script.Move();
    }

    private void Update()
    {
        if (wait)
        {
            wait_count += Time.deltaTime;
            if (wait_count > 10f)
            {
                wait = false;
                GetHelp.Invoke();
            }
        }

        if (started)
        {
            lerp_count += Time.deltaTime;
            float progress = Mathf.Clamp01(lerp_count / 1.5f);

            gameplay_group.alpha = progress;
            menu_group.alpha = 1f - progress;

            if (lerp_count > 1.5f)
            {
                lerp_count = 0f;
                started = false;
            }
        }

        else if (ended)
        {
            gameplay_group.alpha = 0f;
            menu_group.alpha = 1f;

            ended = false;
        }
    }
}
