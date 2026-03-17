using UnityEngine;

public class MLO_Footstep_Logic : MonoBehaviour
{
    bool walking = false;
    float walk_sound_interval = 0.5f;
    float prev_time;

    public AudioSource footstep_1;
    public AudioSource footstep_2;

    private Rigidbody rB;
    // Start is called before the first frame update
    void Start()
    {
        prev_time = Time.time;
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (walking && rB.linearVelocity.magnitude != 0) {
            if (Time.time - prev_time > walk_sound_interval)
            {
                // play random sound
                prev_time = Time.time;
                float rndm = Random.value;
                float pitch = Random.value / 2 + .5f;
                Debug.Log(rndm);
                if (rndm > .5f)
                {
                    footstep_1.pitch = pitch;
                    footstep_1.Play();
                }
                else {
                    footstep_2.pitch = pitch;
                    footstep_2.Play();
                }
            }
        }

        void Walk()
        {
            walking = true;
        }
    }
}
