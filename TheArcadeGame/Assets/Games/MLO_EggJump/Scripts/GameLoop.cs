using FMODUnity;
using TMPro;
using UnityEngine;

public class MLO_GameHandlerScript : MonoBehaviour
{
    int level = 0;
    int score = 0;
    int high_score = 0;
    int trophy_score = 100;
    bool started = false;
    bool wait = true;
    float wait_count = 0f;
    bool ended = true;
    public GameObject player;
    MLO_MovementScript player_handler;

    public GameObject background;

    public GameObject platform;
    MLO_PlatformHandlerScript platform_handler;

    public GameObject start_platform;
    MLO_StartPlatformScript start_platform_handler;

    public GameObject land_particle;
    GameObject[] particles;
    int particle_ptr;

    public GameObject transition_UI;
    MLO_TransitionScript transition_UI_script;
    public GameObject score_UI;
    TextMeshProUGUI score_rndr;
    public GameObject level_UI;
    TextMeshProUGUI level_rndr;
    public GameObject logo_UI;
    TextMeshProUGUI logo_rndr;
    public GameObject record_UI;
    TextMeshProUGUI record_rndr;
    public GameObject fee_UI;
    TextMeshProUGUI fee_rndr;
    float lerp_count = 0f;

    public GameObject help_UI;
    MLO_HelpScript help_script;
   
    public EventReference FMOD_level_sound;
    public EventReference FMOD_coin_sound;

    float speed_multiplier = 2.66f;

    void Start()
    {
        player_handler = player.GetComponent<MLO_MovementScript>();
        platform_handler = platform.GetComponent<MLO_PlatformHandlerScript>();
        start_platform_handler = start_platform.GetComponent<MLO_StartPlatformScript>();
        transition_UI_script = transition_UI.GetComponent<MLO_TransitionScript>();

        score_rndr = score_UI.GetComponent<TextMeshProUGUI>();
        level_rndr = level_UI.GetComponent<TextMeshProUGUI>();
        logo_rndr = logo_UI.GetComponent<TextMeshProUGUI>();
        record_rndr = record_UI.GetComponent<TextMeshProUGUI>();
        fee_rndr = fee_UI.GetComponent<TextMeshProUGUI>();

        help_script = help_UI.GetComponent<MLO_HelpScript>();

        particles = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            particles[i] = Instantiate(land_particle, new Vector3(1000, 1000, 1000), Quaternion.identity);
        }
        particle_ptr = 0;
    }
    public void StartGame()
    {
        started = true;
        FMODAudioUtilsObject.GetUnattenuatedRef(FMOD_coin_sound);
        help_script.StopHelp();
        wait = false;
        platform_handler.play_game = true;

        speed_multiplier = 2.66f;
        player_handler.UpdateSpeed(speed_multiplier);

        start_platform_handler.fallPlatform();
    }

    public void EndGame()
    {
        if (high_score < score)
        {
            high_score = score;
        }
        score = 0;
        level = 0;
        ended = true;
        wait = true;
        wait_count = 0f;

        player_handler.PlayerReset();

        platform_handler.PlatformReset();

        start_platform_handler.PlatformReset();
    }

    public void IncreaseScoreLevel(Vector3 platform_position)
    {
        score += level + 1;

        if (particle_ptr >= 3) {
            particle_ptr = 0;
        }
        else {
            particle_ptr++;
        }
        particles[particle_ptr].GetComponent<ParticleSystem>().transform.position = platform_position;
        particles[particle_ptr].GetComponent<ParticleSystem>().Emit(1);
        

        if (score > Mathf.Pow((float)level+1, 2) * 10) 
        {
            FMODAudioUtilsObject.Get3DAttRef(FMOD_level_sound, player);
            level++;
            platform_handler.UpdateInterval(1 / (level + 1.5f) + .3f);
            speed_multiplier = 12 - (100 / (level + 10));
            player_handler.UpdateSpeed(speed_multiplier);
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return high_score;
    }

    public int GetTrophyScore()
    {
        return trophy_score;
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
        }
        if (started) {
            lerp_count += Time.deltaTime;
            score_rndr.faceColor = new Color(score_rndr.material.color.r, score_rndr.material.color.g, score_rndr.material.color.b, lerp_count / 2);
            level_rndr.faceColor = new Color(level_rndr.material.color.r, level_rndr.material.color.g, level_rndr.material.color.b, lerp_count / 2);
            record_rndr.faceColor = new Color(record_rndr.material.color.r, record_rndr.material.color.g, record_rndr.material.color.b, 1 - lerp_count / 2);
            logo_rndr.faceColor = new Color(logo_rndr.material.color.r, logo_rndr.material.color.g, logo_rndr.material.color.b, 1 - lerp_count / 2);
            fee_rndr.faceColor = new Color(fee_rndr.material.color.r, fee_rndr.material.color.g, fee_rndr.material.color.b, 1 - lerp_count / 2);
        }

        else if (ended) {
            score_rndr.faceColor = new Color(score_rndr.material.color.r, score_rndr.material.color.g, score_rndr.material.color.b, 0);
            level_rndr.faceColor = new Color(level_rndr.material.color.r, level_rndr.material.color.g, level_rndr.material.color.b, 0);
            record_rndr.faceColor = new Color(record_rndr.material.color.r, record_rndr.material.color.g, record_rndr.material.color.b, 1);
            logo_rndr.faceColor = new Color(level_rndr.material.color.r, level_rndr.material.color.g, level_rndr.material.color.b, 1);
            fee_rndr.faceColor = new Color(fee_rndr.material.color.r, fee_rndr.material.color.g, fee_rndr.material.color.b, 1);
            ended = false;
        }
        if (lerp_count > 2 && started)
        {
            lerp_count = 0;
            started = false;
        }
        if (wait_count > 10f && wait)
        {
            wait = false;
            help_script.GetHelp();
        }
    }
}
