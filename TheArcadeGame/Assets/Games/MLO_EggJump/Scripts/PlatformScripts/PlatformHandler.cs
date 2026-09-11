using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    public List<GameObject> platforms;
    public int platform_count;
    private float platform_interval = 1f;
    private int current_platform_index = 0;
    public GameObject platform;
    private int current_direction;
    private int next_direction;
    private Vector2 next_direction_offset;
    public int spacing;
    private Vector3 location = new(0, -3, 0);

    private float time_elapsed;
    private bool trophy_spawned = false;
    private bool playing;

    private static readonly Vector2[] direction_vectors = new[]
    {
        Vector2.up,
        Vector2.right,
        Vector2.down,
        Vector2.left
    };

    [SerializeField] private GameLoop game_handler_script;
    [SerializeField] private MLO_TrophyScript trophy_script;
    public EventReference FMOD_platform_rise_sound;

    void Start()
    {
        current_direction = Random.Range(0, 4);
        for (int i = 0; i < platform_count; i++) platforms.Add(Instantiate(platform, Vector3.up * 500, Quaternion.identity));
        PositionNewPlatform();
    }

    public void PositionNewPlatform() 
    {
        next_direction = Random.Range(0, 4);
        if ((current_direction + 2) % 4 == next_direction) next_direction = current_direction;
        next_direction_offset = direction_vectors[next_direction] * spacing;

        current_direction = next_direction;
        location = new Vector3(location.x + next_direction_offset.x, -3, location.z + next_direction_offset.y);

        int next = (current_platform_index + 1) % platform_count;
        platforms[current_platform_index].GetComponent<Platform>().Rise(location);
        platforms[next].GetComponent<Platform>().Fall();
        current_platform_index = next;

        if (game_handler_script.score >= game_handler_script.trophy_score && !trophy_spawned) 
        {
            trophy_spawned = true;
            trophy_script.SpawnTrophy(location + Vector3.up * 3);
        }
        FMODAudioUtilsObject.Get3DAttRef(FMOD_platform_rise_sound, platforms[current_platform_index].gameObject);
    }

    private void Update()
    {
        if (playing)
        {
            time_elapsed += Time.deltaTime;
            if (time_elapsed > platform_interval)
            {
                PositionNewPlatform();
                time_elapsed = 0;
            }
        }
    }

    public void UpdateInterval()
    {
        platform_interval = 1 / (game_handler_script.level + 1.5f) + .3f;
    }

    public void StartGame()
    {
        playing = true;
    }

    public void PlatformReset()
    {
        foreach (GameObject platform in platforms) platform.GetComponent<Platform>().Fall();
        playing = false;
        location = new Vector3(0, -3, 0);
        platform_interval = 1f;
        PositionNewPlatform();
    }
}
