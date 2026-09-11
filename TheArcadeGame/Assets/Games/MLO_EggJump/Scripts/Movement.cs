using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float jump_distance;
    [SerializeField] private AnimationCurve jump_curve;
    [SerializeField] private float jump_height;
    [SerializeField] private AnimationCurve fallaway_curve;
    [SerializeField] private float fallaway_distance;
    [SerializeField] private AnimationCurve fall_curve;
    [SerializeField] private float fall_distance;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private Vector3 ground_check_offset;
    [SerializeField] private float ground_check_radius;

    [Header("References")]
    [SerializeField] private GameLoop game_handler_script;
    [SerializeField] private InputSubscription inputs;

    [Header("FMOD Events")]
    public EventReference FMOD_jump_sound;
    public EventReference FMOD_fall_sound;
    public EventReference FMOD_land_sound;
    public EventReference FMOD_music_sound;

    private FMOD.Studio.EventInstance music_instance;
    [SerializeField] private bool is_executing;
    private Vector3 start_position = new(0, 1, 0);
    private bool is_initialized = false;

    private IEnumerator Start()
    {
        music_instance = RuntimeManager.CreateInstance(FMOD_music_sound);
        music_instance.start();
        music_instance.release();

        yield return StartCoroutine(InitializePhysicsBuffer());
    }

    private IEnumerator InitializePhysicsBuffer()
    {
        is_initialized = false;
        yield return new WaitForFixedUpdate();
        is_initialized = true;
    }

    void Update()
    {        
        if (inputs.MenuInput)
        {
            SceneManager.LoadScene(0);
            return;
        }
        if (!is_initialized) return;
        if (!is_executing)
        {
            if (!IsGrounded())
            {
                StartCoroutine(PerformDeath(0));
                return;
            }
            Vector3 jump_direction = GetInputDirection();
            if (jump_direction != Vector3.zero)
            {
                StartCoroutine(PerformJump(jump_direction));
            }
        }
    }

    private bool IsGrounded()
    {
        Vector3 check_center = transform.TransformPoint(ground_check_offset);
        Collider[] hits = Physics.OverlapSphere(
            check_center,
            ground_check_radius,
            ~0,
            QueryTriggerInteraction.Collide
        );
        foreach (Collider hit in hits) if (hit.gameObject != gameObject) return true;
        return false;
    }

    private Vector3 GetInputDirection()
    {
        Vector2 input = inputs.AnalogMovementInput;
        if (input.magnitude <= 0.9f) return Vector3.zero;
        if (input.y > 0.9f) return Vector3.forward * jump_distance;
        if (input.x > 0.9f) return Vector3.right * jump_distance;
        if (input.y < -0.9f) return Vector3.back * jump_distance;
        if (input.x < -0.9f) return Vector3.left * jump_distance;
        return Vector3.zero;
    }

    private IEnumerator PerformJump(Vector3 direction)
    {
        is_executing = true;
        if (game_handler_script.score == 0) game_handler_script.StartGame();
        FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);
        Vector3 target_position = start_position + direction;
        float progress = 0f;
        float speed = game_handler_script.speed_multiplier;
        while (progress < 1f)
        {
            progress += Time.deltaTime * speed;
            Vector3 current_position = Vector3.Lerp(start_position, target_position, progress);
            current_position.y = start_position.y + (jump_curve.Evaluate(Mathf.Min(progress, 1f)) * jump_height);
            transform.position = current_position;
            yield return null;
        }
        start_position = target_position;
        transform.position = start_position;
        if (IsGrounded())
        {
            game_handler_script.IncreaseScoreLevel(new Vector3(start_position.x, start_position.y - 0.85f, start_position.z));
            FMODAudioUtilsObject.Get3DAttRef(FMOD_land_sound, gameObject);
            is_executing = false;
        }
        else yield return StartCoroutine(PerformDeath(1));
    }

    private IEnumerator PerformDeath(int type)
    {
        
        is_executing = true;
        game_handler_script.Menu();
        FMODAudioUtilsObject.Get3DAttRef(FMOD_fall_sound, gameObject);
        float elapsed = 0f;
        bool halfway = false;
        Vector3 death_start_position = transform.position;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            transform.position = new Vector3(
                death_start_position.x, 
                ((type == 0 ? fallaway_curve : fall_curve).Evaluate(Mathf.Min(elapsed, 1f)) 
                    * (type == 0 ? fallaway_distance : fall_distance)) - (type == 0 ? fallaway_distance : fall_distance) + 1, 
                death_start_position.z);
            if (elapsed > .6f && !halfway) { game_handler_script.EndGame(); halfway = true; }
            yield return null;
        }
        PlayerReset();
    }

    public void PlayerReset()
    {
        StopAllCoroutines();
        start_position = new Vector3(0, 1, 0);
        transform.position = start_position;
        is_executing = false;
        StartCoroutine(InitializePhysicsBuffer());
    }

    private void OnDestroy()
    {
        music_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music_instance.release();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Match the exact transform calculation used in IsGrounded()
        Vector3 check_center = transform.TransformPoint(ground_check_offset);
        Gizmos.DrawWireSphere(check_center, ground_check_radius);
    }
}