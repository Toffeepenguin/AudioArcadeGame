using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MLO_MovementScript : MonoBehaviour
{
    float time = 0;
    float speed_multiplier;
    bool move = false;
    bool dead = false;
    Vector3 start_position = new Vector3(0, 1, 0);
    Vector3 end_position = new Vector3(0, 0, 0);
    public MLO_PlatformHandlerScript platform_script;
    Camera playerCamera;
    bool colliding = true;
    float cam_y;

    public GameObject game_handler;
    MLO_GameHandlerScript game_handler_script;

    public GameObject input_manager;
    InputSubscription _input;

    //public AudioSource jump_sound;
    //public AudioSource fall_sound;
    public EventReference FMOD_jump_sound;
    public EventReference FMOD_fall_sound;
    public EventReference FMOD_land_sound;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam_y = playerCamera.transform.position.y;
        game_handler_script = game_handler.GetComponent<MLO_GameHandlerScript>();
        _input = input_manager.GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!colliding && !move && !dead) // not colliding when in landed state
        {
            dead = true;
            time = 0;
            //fall_sound.Play();
            FMODAudioUtilsObject.Get3DAttRef(FMOD_fall_sound, gameObject);
        }

        if (!move && !dead) // stationary, ready for input
        {
            if (_input.AnalogMovementInput.y > .9)
            {
                end_position = new Vector3(start_position.x, start_position.y, start_position.z + 4);
                move = true;
                FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);

            }
            else if (_input.AnalogMovementInput.x > .9)
            {
                end_position = new Vector3(start_position.x + 4, start_position.y, start_position.z);
                move = true;
                FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);

            }
            else if (_input.AnalogMovementInput.y < -.9)
            {
                end_position = new Vector3(start_position.x, start_position.y, start_position.z - 4);
                move = true;
                FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);

            }
            else if (_input.AnalogMovementInput.x < -.9)
            {
                end_position = new Vector3(start_position.x - 4, start_position.y, start_position.z);
                move = true;
                FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);

            }
        }

        if (move && game_handler_script.GetScore() == 0) // if this is the first jump, which starts the game
        {
            game_handler_script.StartGame();
        }

        if (move && !dead) // jump state
        {
            time += (Time.deltaTime * speed_multiplier);
            gameObject.transform.position = new Vector3(
            MyLerp(start_position.x, end_position.x, time),
            (float)(-(Math.Pow((2 * time - 1), 2)) + 1) + 1,
            MyLerp(start_position.z, end_position.z, time));

            playerCamera.transform.position = new Vector3( // make camera follow player, except the bobbing
                playerCamera.transform.position.x, 
                gameObject.transform.position.y - (float)(-(Math.Pow((2 * time - 1), 2)) + 1) + 16.37f, 
                playerCamera.transform.position.z);

            if (time >= 1) // finished jump
            {
                gameObject.transform.position = new Vector3( // round vector
                    Mathf.Round(gameObject.transform.position.x / 4) * 4,
                    Mathf.Round(gameObject.transform.position.y / 4) * 4 + 1,
                    Mathf.Round(gameObject.transform.position.z / 4) * 4);
                move = false;
                start_position = transform.position;
                time = 0;

                if (colliding) // final precaution to make sure the player succeeded
                {
                    game_handler_script.IncreaseScoreLevel(new Vector3(end_position.x, end_position.y - .85f, end_position.z));
                    FMODAudioUtilsObject.Get3DAttRef(FMOD_fall_sound, gameObject);
                }
            }
        }

        if (dead)
        {
            if (time < 0.15f)
            {
                game_handler_script.Menu();
            }
            time += Time.deltaTime;
            if (gameObject.transform.position.y > -20)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 16 * Mathf.Cos(time * Mathf.PI / 1.5f) - 15, gameObject.transform.position.z);
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, cam_y, playerCamera.transform.position.z);
            }

            if (time > 1) // reset
            {
                game_handler_script.EndGame();
            }
        }

        if (_input.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
    }

    float MyLerp(float start_var, float end_var, float t)
    {
        return (float)((1 - t) * start_var + t * end_var);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground")) {
            colliding = true;
        }
    }
        

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            colliding = false;
        }
    }

    public void UpdateSpeed(float speed)
    {
        speed_multiplier = speed;
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
}
