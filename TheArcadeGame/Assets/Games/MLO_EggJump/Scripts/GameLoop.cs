using FMODUnity;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

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

    [SerializeField] private GameObject land_particle_prefab;
    private readonly List<ParticleSystem> particles = new();
    private int current_particle_index;

    private float lerp_count = 0f;

    [SerializeField] private MLO_TransitionScript transition_UI_script;
    [SerializeField] private TextMeshProUGUI score_rndr;
    [SerializeField] private TextMeshProUGUI level_rndr;
    [SerializeField] private TextMeshProUGUI logo_rndr;
    [SerializeField] private TextMeshProUGUI record_rndr;
    [SerializeField] private TextMeshProUGUI fee_rndr;
   
    public EventReference FMOD_level_sound;
    public EventReference FMOD_coin_sound;

    [SerializeField] private UnityEvent GameStarted;
    [SerializeField] private UnityEvent GameEnded;
    [SerializeField] private UnityEvent GetScore;
    [SerializeField] private UnityEvent GetLevel;
    [SerializeField] private UnityEvent GetTrophy;
    [SerializeField] private UnityEvent GetHelp;

    [HideInInspector] public float speed_multiplier => 2.5f * Mathf.Pow(1.1f, level);

    private void Start()
    {
        if (land_particle_prefab != null) for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(land_particle_prefab, new Vector3(1000f, 1000f, 1000f), Quaternion.identity);
            if (obj.TryGetComponent(out ParticleSystem ps)) particles.Add(ps);
        }
        current_particle_index = 0;
    }

    public void StartGame()
    {
        started = true;
        ended = false;
        lerp_count = 0f;
        FMODAudioUtilsObject.GetUnattenuatedRef(FMOD_coin_sound);
        wait = false;
        GameStarted.Invoke();
    }

    public void EndGame()
    {
        started = false;
        if (high_score < score)
        {
            high_score = score;
        }
        score = 0;
        level = 0;
        ended = true;
        wait = true;
        wait_count = 0f;
        GameEnded.Invoke();
    }

    public void IncreaseScoreLevel(Vector3 platform_position)
    {
        score ++;
        GetScore.Invoke();
        if (particles.Count > 0)
        {
            current_particle_index = (current_particle_index + 1) % particles.Count;
            particles[current_particle_index].transform.position = platform_position;
            particles[current_particle_index].Emit(1);
        }
        while (score >= 10f * Mathf.Pow(1.35f, level)) 
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
        if (started) {
            lerp_count += Time.deltaTime;
            float progress = lerp_count / 2f;
            score_rndr.alpha = progress;
            level_rndr.alpha = progress;
            float inverse_progress = 1f - progress;
            record_rndr.alpha = inverse_progress;
            logo_rndr.alpha = inverse_progress;
            fee_rndr.alpha = inverse_progress;
            if (lerp_count > 2)
            {
                lerp_count = 0;
                started = false;
            }
        }
        else if (ended) {
            score_rndr.alpha = 0f;
            level_rndr.alpha = 0f;
            record_rndr.alpha = 1f;
            logo_rndr.alpha = 1f;
            fee_rndr.alpha = 1f;
            ended = false;
        }
    }
}
