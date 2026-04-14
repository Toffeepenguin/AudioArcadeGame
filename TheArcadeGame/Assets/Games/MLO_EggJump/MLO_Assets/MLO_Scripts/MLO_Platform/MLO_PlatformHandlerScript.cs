using FMODUnity;
using UnityEngine;

public class MLO_PlatformHandlerScript : MonoBehaviour
{
    public GameObject[] platformList;
    public GameObject platform;
    int start;
    public int current_direction = 1; // up = 0, right = 1, down = 2, left = 3
    int next_direction;
    int hor; //izontal
    int ver; //tical
    int spacing = 4;
    Vector3 location;
    public int num_of_platforms;
    float platform_interval = 1f;
    float time_elapsed;
    static System.Random r;
    bool trophy_spawned = false;
    public bool play_game;

    public GameObject game_handler;
    MLO_GameHandlerScript game_handler_script;

    public GameObject trophy;
    MLO_TrophyScript trophy_script;

    public EventReference FMOD_platform_rise_sound;

    // Start is called before the first frame update
    void Start()
    {
        game_handler_script = game_handler.GetComponent<MLO_GameHandlerScript>();
        trophy_script = trophy.GetComponent<MLO_TrophyScript>();
        platformList = new GameObject[num_of_platforms+1];
        location = new Vector3(0, -3, 0); // new location for next platform
        r = new System.Random();
        hor = 4; ver = 0;
        platform_interval = 1f;
        trophy.transform.position = new Vector3(1000, 1000, 1000);
        InstantiatePlatform();
    }

    public void InstantiatePlatform(Vector3? position_override = null) 
        
    {
        // loop start pointer when it surpasses the last index
        if (start <= num_of_platforms - 1) 
        {
            start += 1;
        }
        else
        {
            start = 0;
        }

        // move in a random direction, including the same direction
        next_direction = r.Next(0, 4);
        switch (current_direction)
        {
            case 0:
                if (next_direction == 2)
                {
                    next_direction = 0;
                }
                break;
            case 1:
                if (next_direction == 3)
                {
                    next_direction = 1;
                }
                break;
            case 2:
                if (next_direction == 0)
                {
                    next_direction = 2;
                }
                break;
            case 3:
                if (next_direction == 1)
                {
                    next_direction = 3;
                }
                break;
        }

        // update hor and ver to displace the next platform correctly
        switch (next_direction)
        {
            case 0:
                hor = 0;
                ver = spacing;
                break;
            case 1:
                hor = spacing;
                ver = 0;
                break;
            case 2:
                hor = 0;
                ver = -spacing;
                break;
            case 3:
                hor = -spacing;
                ver = 0;
                break;
        }

        current_direction = next_direction;

        // if default position value was not given
        if (position_override == null) 
        {
            location = new Vector3(location.x + hor, -3, location.z + ver);
        }
        else
        {
            location = (Vector3)position_override;
        }

        // update age of each platform in their own scripts
        for (int i = 0; i < num_of_platforms + 1; i++)
        {
            if (platformList[i] != null)
            {
                platformList[i].GetComponent<MLO_PlatformScript>().updateAge();
            }
        }

        // instantiate the new platform
        platformList[start] = Instantiate(platform, location, Quaternion.identity);
        platformList[start].AddComponent<MLO_PlatformScript>();
        platformList[start].AddComponent<BoxCollider>().isTrigger = true;
        platformList[start].GetComponent<BoxCollider>().center = new Vector3(
            platformList[start].GetComponent<BoxCollider>().center.x,
            platformList[start].GetComponent<BoxCollider>().center.y + 4,
            platformList[start].GetComponent<BoxCollider>().center.z);
        platformList[start].AddComponent<Rigidbody>().useGravity = false;
        platformList[start].GetComponent<Rigidbody>().isKinematic = false;
        platformList[start].tag = "Ground";
        location = new Vector3(platformList[start].transform.position.x, -3, platformList[start].transform.position.z);
        if (game_handler_script.GetScore() >= game_handler_script.GetTrophyScore() && !trophy_spawned) 
        {
            trophy_spawned = true;
            trophy_script.SpawnTrophy(location);
        }
        FMODAudioUtilsObject.Get3DAttRef(FMOD_platform_rise_sound, platformList[start].gameObject);
        // destroy the oldest platform
        Destroy(platformList[mod((start - num_of_platforms), platformList.Length)]); 
        platformList[mod((start - num_of_platforms), (platformList.Length))] = null;  // set it to null in the list
    }
    private void Update()
    {
        if (play_game)
        {
            time_elapsed += Time.deltaTime;
            if (time_elapsed > platform_interval)
            {
                InstantiatePlatform();
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
        for (int i = 0; i < num_of_platforms + 1; i++)
        {
            if (platformList[i] != null)
            {
                // destroy all platforms
                Destroy(platformList[i]);
                platformList[i] = null;  // set it to null in the list
            }
        }
        play_game = false;

        hor = 4;
        ver = 0;
        location = new Vector3(0, -3, 0); // new location for next platform
        r = new System.Random();
        platform_interval = 1f;
        InstantiatePlatform();
    }
    int mod(int x, int m) // custom modulo function, the '%' operator doesn't have the correct behaviour with negative numbers.
    {
        return (x % m + m) % m;
    }
}
