using FMODUnity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformHandlerScript : MonoBehaviour
{
    public List<GameObject> platforms;
    public int platform_count;
    private float platform_interval = 1f;
    private int current_platform_index = 0;
    public GameObject platform;
    public int current_direction = 1; // up = 0, right = 1, down = 2, left = 3
    private int next_direction;
    private Vector2 next_direction_offset;
    public int spacing;
    private Vector3 location = new(0, -3, 0);

    private float time_elapsed;
    private bool trophy_spawned = false;
    public bool play_game;

    public GameObject game_handler;
    private MLO_GameHandlerScript game_handler_script;
    public GameObject trophy;
    private MLO_TrophyScript trophy_script;
    public EventReference FMOD_platform_rise_sound;

    void Start()
    {
        game_handler_script = game_handler.GetComponent<MLO_GameHandlerScript>();
        trophy_script = trophy.GetComponent<MLO_TrophyScript>();
        for (int i = 0; i < platform_count; i++) platforms.Add(Instantiate(platform, Vector3.up * 500, Quaternion.identity));
        trophy.transform.position = Vector3.up * 1000;
        PositionNewPlatform();
    }

    public void PositionNewPlatform() 
    {
        // forces no 180 deg turns
        next_direction = Random.Range(0, 4);
        if ((current_direction + 2) % 4 == next_direction) next_direction = current_direction;
        switch (next_direction)
        {
            case 0:
                next_direction_offset = Vector2.up * spacing;
                break;
            case 1:
                next_direction_offset = Vector2.right * spacing;
                break;
            case 2:
                next_direction_offset = Vector2.down * spacing;
                break;
            case 3:
                next_direction_offset = Vector2.left * spacing;
                break;
        }

        current_direction = next_direction;
        location = new Vector3(location.x + next_direction_offset.x, -3, location.z + next_direction_offset.y);

        int next = (current_platform_index + 1) % platform_count;
        platforms[current_platform_index].GetComponent<PlatformScript>().Rise(location);
        platforms[next].GetComponent<PlatformScript>().Fall();
        current_platform_index = next;

        if (game_handler_script.GetScore() >= game_handler_script.GetTrophyScore() && !trophy_spawned) 
        {
            trophy_spawned = true;
            trophy_script.SpawnTrophy(location + Vector3.up * 3);
        }
        FMODAudioUtilsObject.Get3DAttRef(FMOD_platform_rise_sound, platforms[current_platform_index].gameObject);
    }

    private void Update()
    {
        if (play_game)
        {
            time_elapsed += Time.deltaTime;
            if (time_elapsed > platform_interval)
            {
                PositionNewPlatform();
                time_elapsed = 0;
            }
        }
    }

    public void UpdateInterval(float interval)
    {
        platform_interval = interval;
    }

    public void PlatformReset()
    {
        foreach (GameObject platform in platforms) platform.GetComponent<PlatformScript>().Fall();
        play_game = false;
        location = new Vector3(0, -3, 0);
        platform_interval = 1f;
        PositionNewPlatform();
    }
}
