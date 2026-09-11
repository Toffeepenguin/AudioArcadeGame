using FMODUnity;
using System.Collections;
using UnityEngine;

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

    [Header("Juice & visuals")]
    [SerializeField] private Transform visual_mesh;
    [SerializeField] private AnimationCurve squash_stretch_curve;
    [SerializeField] private Vector2 jumping_squash_stretch;
    [SerializeField] private float mesh_scale;
    [SerializeField] private float landing_squash_duration;
    [SerializeField] private Vector2 landing_squash_stretch;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask ground_layer;
    [SerializeField] private Vector3 ground_check_offset;
    [SerializeField] private float ground_check_radius;

    [Header("References")]
    [SerializeField] private GameLoop game_handler_script;
    [SerializeField] private InputActions inputs;

    [Header("FMOD Events")]
    public EventReference FMOD_jump_sound;
    public EventReference FMOD_fall_sound;
    public EventReference FMOD_land_sound;
    public EventReference FMOD_music_sound;

    private FMOD.Studio.EventInstance music_instance;
    [SerializeField] private bool is_executing;
    private Vector3 start_position = new(0, 1, 0);

    private void Awake()
    {
        music_instance = RuntimeManager.CreateInstance(FMOD_music_sound);
        music_instance.start();
        music_instance.release();
        inputs = new InputActions();
    }

    private void OnEnable() => inputs.ActionMap.Enable();

    private void OnDisable() => inputs.ActionMap.Disable();

    void Update()
    {
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
        Vector2 input = inputs.ActionMap.Movement.ReadValue<Vector2>();
        if (input.sqrMagnitude < 0.1f) return Vector3.zero;
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) return (input.x > 0 ? Vector3.right : Vector3.left) * jump_distance;
        else return (input.y > 0 ? Vector3.forward : Vector3.back) * jump_distance;
    }
  
    private IEnumerator PerformJump(Vector3 direction)
    {
        is_executing = true;
        if (game_handler_script.score == 0) game_handler_script.StartGame();
        FMODAudioUtilsObject.Get3DAttRef(FMOD_jump_sound, gameObject);
        Vector3 target_position = start_position + direction;
        float progress = 0f;
        float speed = game_handler_script.speed_multiplier;
        Quaternion initial_mesh_rotation = visual_mesh.rotation;
        Quaternion target_mesh_rotation = Random.Range(0, 5) == 0 ? Quaternion.AngleAxis(180f, Vector3.Cross(Vector3.up, direction.normalized)) * initial_mesh_rotation : visual_mesh.rotation;
        while (progress < 1f)
        {
            progress += Time.deltaTime * speed;
            float clamped_progress = Mathf.Min(progress, 1f);
            Vector3 current_position = Vector3.Lerp(start_position, target_position, clamped_progress);
            current_position.y = start_position.y + (jump_curve.Evaluate(clamped_progress) * jump_height);
            transform.position = current_position;
            visual_mesh.rotation = Quaternion.Slerp(initial_mesh_rotation, target_mesh_rotation, 1f - clamped_progress);
            if (squash_stretch_curve != null)
            {
                float air_stretch = squash_stretch_curve.Evaluate(clamped_progress);
                visual_mesh.localScale = new Vector3(
                    Mathf.Lerp(mesh_scale, jumping_squash_stretch.x, air_stretch), 
                    Mathf.Lerp(mesh_scale, jumping_squash_stretch.y, air_stretch), 
                    Mathf.Lerp(mesh_scale, jumping_squash_stretch.x, air_stretch));
            }
            yield return null;
        }
        start_position = target_position;
        transform.position = start_position;
        visual_mesh.rotation = target_mesh_rotation;
        visual_mesh.localScale = Vector3.one * mesh_scale;
        if (IsGrounded())
        {
            game_handler_script.IncreaseScoreLevel(new Vector3(start_position.x, start_position.y - 0.85f, start_position.z));
            FMODAudioUtilsObject.Get3DAttRef(FMOD_land_sound, gameObject);
            yield return StartCoroutine(PerformLandingSquash());
            is_executing = false;
        }
        else yield return StartCoroutine(PerformDeath(1));
    }

    private IEnumerator PerformLandingSquash()
    {
        float elapsed = 0f;
        Vector3 initial_mesh_local_pos = visual_mesh.localPosition;
        while (elapsed < landing_squash_duration)
        {
            elapsed += Time.deltaTime;
            float impact_intensity = Mathf.Sin(elapsed / landing_squash_duration * Mathf.PI);
            float current_scale_x = Mathf.Lerp(mesh_scale, landing_squash_stretch.x, impact_intensity);
            float current_scale_y = Mathf.Lerp(mesh_scale, landing_squash_stretch.y, impact_intensity);
            visual_mesh.localScale = new Vector3(current_scale_x, current_scale_y, current_scale_x);
            visual_mesh.localPosition = new Vector3(
                initial_mesh_local_pos.x,
                initial_mesh_local_pos.y - (mesh_scale - current_scale_y) * 0.5f,
                initial_mesh_local_pos.z);
            yield return null;
        }
        visual_mesh.localScale = Vector3.one * mesh_scale;
        visual_mesh.localPosition = initial_mesh_local_pos;
    }

    private IEnumerator PerformDeath(int type)
    {
        is_executing = true;
        game_handler_script.Menu();
        FMODAudioUtilsObject.Get3DAttRef(FMOD_fall_sound, gameObject);
        float elapsed = 0f;
        bool halfway = false;
        Vector3 death_start_position = transform.position;
        float tilt = 45f;
        Quaternion target_rotation = Quaternion.Euler(
            Random.Range(-tilt, tilt), 0f,
            Random.Range(-tilt, tilt));
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            transform.position = new Vector3(
                death_start_position.x,
                ((type == 0 ? fallaway_curve : fall_curve).Evaluate(Mathf.Min(elapsed, 1f))
                    * (type == 0 ? fallaway_distance : fall_distance)) - (type == 0 ? fallaway_distance : fall_distance) + 1,
                death_start_position.z);
            visual_mesh.rotation = Quaternion.Slerp(Quaternion.identity, target_rotation, Mathf.Min(elapsed, 1f));
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
        if (visual_mesh != null)
        {
            visual_mesh.rotation = Quaternion.identity;
            visual_mesh.localScale = Vector3.one;
        }
        is_executing = false;
    }

    private void OnDestroy()
    {
        music_instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        music_instance.release();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 check_center = transform.TransformPoint(ground_check_offset);
        Gizmos.DrawWireSphere(check_center, ground_check_radius);
    }
}