using System.Collections;
using FMODUnity;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public GameObject death_particle;
    public EventReference FMOD_trophy_sound;
    private Collider trophy_collider;
    private Vector3 target_position;
    public bool collected = false;
    private float float_offset = 0.25f;

    private void Awake()
    {
        trophy_collider = GetComponent<Collider>();
        target_position = transform.position;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            float hover = Mathf.Sin(Time.time * 2f) * 0.25f;
            transform.position = new Vector3(target_position.x, target_position.y + float_offset + hover, target_position.z);
        }
    }

    public void SpawnTrophy(Vector3 platform_position)
    {
        target_position = new Vector3(platform_position.x, platform_position.y + 1f, platform_position.z);
        collected = false;
        gameObject.SetActive(true);
        if (trophy_collider != null) trophy_collider.enabled = true;
        StopAllCoroutines();
        StartCoroutine(RiseRoutine(0.5f));
    }

    private IEnumerator RiseRoutine(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float sin = Mathf.Sin(t * Mathf.PI / 2f);
            float_offset = Mathf.Lerp(3f, 0.25f, sin);

            elapsed += Time.deltaTime;
            yield return null;
        }
        float_offset = 0f;
    }

    public void TriggerTrophy()
    {
        collected = true;
        if (trophy_collider != null) trophy_collider.enabled = false;
        FMODAudioUtilsObject.Get3DAttRef(FMOD_trophy_sound, gameObject);
        if (death_particle != null)
        {
            GameObject p = Instantiate(death_particle, transform.position, Quaternion.identity);
            if (p.TryGetComponent<ParticleSystem>(out var ps)) ps.Emit(1);
        }
        if (PlayerPrefs.GetInt("MLO_Trophie_Int") != 1)
        {
            PlayerPrefs.SetInt("MLO_Trophie_Int", 1);
            PlayerPrefs.Save();
        }
        gameObject.SetActive(false);
    }
}