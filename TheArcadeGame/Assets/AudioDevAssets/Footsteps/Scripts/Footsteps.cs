using FMODUnity;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [Header("Inputs")]
    private Vector3 last_position;
    private Vector3 delta_position;
    private Quaternion last_rotation;
    private Quaternion delta_rotation;
    private float stop_timer;
    public float final_footstep_threshold;
    public float maximum_rotation_amplitude;
    public float maximum_elevation;
    public float minimum_stride_distance;
    [SerializeField] private EventReference footstep_sound;

    [Header("Outputs")]
    private int side;
    private bool walking;
    private bool initial;

    private void Start()
    {
        side = 1;
    }

    private void Update()
    {
        delta_position = transform.position - last_position;
        delta_rotation = Quaternion.Inverse(last_rotation) * transform.rotation;
        if (Step())
        {
            initial = !walking;
            walking = true;
            stop_timer = 0f;
            side *= -1;

            // INITIAL FOOTSTEP
            int initial_factor = initial ? 1 : 0;

            // ROTATION
            // side * d_rot: positive means same side; magnitude is strength, outside footsteps are heavier
            float rotation_factor = Mathf.Clamp(delta_rotation.x * side, 0, maximum_rotation_amplitude);

            // ELEVATION 
            // the further the change in elevation, means heavier step
            float elevation = delta_position.y;
            float elevation_factor = Mathf.Min(Mathf.Abs(elevation), maximum_elevation);
            float incline_factor = Mathf.InverseLerp(-maximum_elevation, maximum_elevation, elevation) * 2f - 1f;

            // SPEED
            // speed means steps are heavier
            float speed_factor = delta_position.magnitude / Time.deltaTime;

            PlayFootstep(rotation_factor, elevation_factor, incline_factor, speed_factor, initial_factor);

            last_position = transform.position;
            last_rotation = transform.rotation;
        }
        else if (walking)
        {
            stop_timer += Time.deltaTime;
            if (stop_timer > final_footstep_threshold)
            {
                walking = false;
                PlayFootstep(0f, 0f, 0f, 0f, 2);
            }
        }
    }

    private void PlayFootstep(float rotation_factor, float elevation_factor, float incline_factor, float speed_factor, int footstep_type)
    {
        //dond need this anymore
        //GetComponent<FMODUnity.StudioEventEmitter>().Play();
        //Use this to play sound instead
        var eventInstance = RuntimeManager.CreateInstance(footstep_sound);
        //This is needed to select the audio from bank and then selecting the parameter
        eventInstance.setParameterByNameWithLabel("CharacterFootsteps", "Value A");

        //This part is used for doing one shot audio, important to release due to memory leaks
        eventInstance.start();
        eventInstance.release();
        return;
    }

    private bool Step()
    {
        return Mathf.Abs(Vector3.Distance(transform.position, last_position)) > minimum_stride_distance;
    }

    private void GetWalkingSurface()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            int hit_layer = hit.collider.gameObject.layer;
        }
        return;
    }
}
