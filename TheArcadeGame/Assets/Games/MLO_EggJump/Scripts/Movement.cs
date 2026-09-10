using FMOD.Studio;
using FMODUnity;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private float time = 0;
    private float speed_multiplier;
    private bool move = false;
    private bool dead = false;
    private Vector3 start_position = new(0, 1, 0);
    private Vector3 end_position = new(0, 0, 0);
    private Camera playerCamera;
    private bool colliding = true;
    private float cam_y;

    [SerializeField] private GameLoop game_handler_script;
    [SerializeField] private InputSubscription inputs;

    public EventReference FMOD_jump_sound;
    public EventReference FMOD_fall_sound;
    public EventReference FMOD_land_sound;
    public EventReference FMOD_music_sound;
    private FMOD.Studio.EventInstance music_instance;

    void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam_y = playerCamera.transform.position.y;
        
        music_instance = RuntimeManager.CreateInstance(FMOD_music_sound);
        music_instance.start();
        music_instance.release();
    }

    void Update()
    {
        if (!colliding && !move && !dead)
        {
            dead = true;
            time = 0;
            FMODAudioUtilsObject.Get3DAttRef(FMOD_fall_sound, gameObject);
        }
        if (!move && !dead)
        {
            if (inputs.AnalogMovementInput.magnitude > .9) 
            {
                if (inputs.AnalogMovementInput.y > .9)
                {
                    end_position = new Vector3(start_position.x, start_position.y, start_position.z + 4);
                    move = true;
                    FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);

                }
                else if (inputs.AnalogMovementInput.x > .9)
                {
                    end_position = new Vector3(start_position.x + 4, start_position.y, start_position.z);
                    move = true;
                    FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);
                }
                else if (inputs.AnalogMovementInput.y < -.9)
                {
                    end_position = new Vector3(start_position.x, start_position.y, start_position.z - 4);
                    move = true;
                    FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);

                }
                else if (inputs.AnalogMovementInput.x < -.9)
                {
                    end_position = new Vector3(start_position.x - 4, start_position.y, start_position.z);
                    move = true;
                    FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);
                }
                if (move && game_handler_script.score == 0) game_handler_script.StartGame();
            }
        }
        if (move && !dead)
        {
            time += (Time.deltaTime * speed_multiplier);
            gameObject.transform.position = new Vector3(
            MyLerp(start_position.x, end_position.x, time),
            (float)(-(Math.Pow((2 * time - 1), 2)) + 1) + 1,
            MyLerp(start_position.z, end_position.z, time));
            playerCamera.transform.position = new Vector3(
                playerCamera.transform.position.x, 
                gameObject.transform.position.y - (float)(-(Math.Pow((2 * time - 1), 2)) + 1) + 16.37f, 
                playerCamera.transform.position.z);
            if (time >= 1)
            {
                gameObject.transform.position = new Vector3(
                    Mathf.Round(gameObject.transform.position.x / 4) * 4,
                    Mathf.Round(gameObject.transform.position.y / 4) * 4 + 1,
                    Mathf.Round(gameObject.transform.position.z / 4) * 4);
                move = false;
                start_position = transform.position;
                time = 0;
                if (colliding)
                {
                    game_handler_script.IncreaseScoreLevel(new Vector3(end_position.x, end_position.y - .85f, end_position.z));
                    FMODAudioUtilsObject.Get3DAttRef(FMOD_land_sound, gameObject);
                }
            }
        }
        if (dead)
        {
            if (time < 0.15f) game_handler_script.Menu();
            time += Time.deltaTime;
            if (gameObject.transform.position.y > -20)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 16 * Mathf.Cos(time * Mathf.PI / 1.5f) - 15, gameObject.transform.position.z);
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, cam_y, playerCamera.transform.position.z);
            }
            if (time > 1) game_handler_script.EndGame();
        }
        if (inputs.MenuInput) SceneManager.LoadScene(0);
    }

    float MyLerp(float start_var, float end_var, float t)
    {
        return (float)((1 - t) * start_var + t * end_var);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) colliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground")) colliding = false;
    }

    public void UpdateSpeed()
    {
        speed_multiplier = game_handler_script.speed_multiplier;
    }

    public void PlayerReset()
    {
        start_position = new Vector3(0, 1, 0);
        end_position = new Vector3(0, 0, 0);
        dead = false;
        time = 0;
        transform.position = start_position;
        colliding = true;
        move = false;
        playerCamera.transform.position = new Vector3(-13.4f, 17.37f, -29.4f);
        cam_y = playerCamera.transform.position.y;
    }

    private void OnDestroy()
    {
        music_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music_instance.release();
    }
}