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
        last_position = transform.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, last_position);
        if (distance > minimum_stride_distance)
        {
            side *= -1;
            initial = !walking;
            walking = true;
            stop_timer = 0f;

            PlayFootstep();

            last_position = transform.position;
            last_rotation = transform.rotation;
        }
        else if (walking)
        {
            stop_timer += Time.deltaTime;
            if (stop_timer > final_footstep_threshold)
            {
                walking = false;
                side *= -1;

                Debug.Log("Final stop step triggered");
                PlayFootstep();
                last_position = transform.position;
            }
        }
    }

    private void PlayFootstep()
    {
        FMOD.Studio.EventInstance event_instance = RuntimeManager.CreateInstance(footstep_sound);
        FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(gameObject);
        event_instance.set3DAttributes(attributes);
        event_instance.setParameterByName("FootSide", side * 100f);
        FMODAudioUtilsObject.PlayInstance(event_instance);
        return;
    }
}
