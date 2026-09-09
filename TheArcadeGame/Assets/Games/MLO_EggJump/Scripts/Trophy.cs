using FMODUnity;
using UnityEngine;

public class MLO_TrophyScript : MonoBehaviour
{
    public GameObject death_particle;
    public EventReference FMOD_trophy_sound;
    public GameObject trophyUI;
    private MLO_TrophyUIScript trophy_UI_script;

    private float y_pos = 100.2f;
    private float spawn_time = 0f;
    private bool spawn = false;
    private bool collected = false;

    void Start()
    {
        trophy_UI_script = trophyUI.GetComponent<MLO_TrophyUIScript>();
        transform.position = new Vector3(0, y_pos, 0);
    }

    public bool IsCollected()
    {
        return collected;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, y_pos + Mathf.Sin(Time.time) / 4, transform.position.z);

        if (spawn && Time.time - spawn_time < .5f)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Sin((Time.time - spawn_time) % 1 * Mathf.PI) * 2 - 1, transform.position.z);
        }

        if (spawn && Time.time - spawn_time > .5f)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            spawn = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collected = true;
            spawn_time = Time.time;
            FMODAudioUtilsObject.Get3DAttRef(FMOD_trophy_sound, gameObject);
            trophy_UI_script.RunUI();
            Instantiate(death_particle, transform.position, Quaternion.identity);
            death_particle.GetComponent<ParticleSystem>().Emit(1);
            gameObject.SetActive(false);

            if (PlayerPrefs.GetInt("MLO_Trophie_Int") != 1)
            {
                PlayerPrefs.SetInt("MLO_Trophie_Int", 1);
                PlayerPrefs.Save();
            }
        }
    }

    public void SpawnTrophy(Vector3 location)
    {
        y_pos = location.y + 4.2f;
        transform.position = new Vector3(location.x, y_pos, location.z);
        spawn = true;
        spawn_time = Time.time;
    }
}
