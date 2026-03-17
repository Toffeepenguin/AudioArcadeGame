using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCH_level_MountainKing : MonoBehaviour
{
    public SCH_spawner spawnerScp;
    private float time = 0f; // Time when the script is initialized
    private float delayTime = 0f; // Time stored for calculation
    public float stringSpawningPeriod = 0.05f;
    public float musicLength = 120f;
    private bool NoteSpawned = false;

    private int noOfStringsSpawned = 0;
    private int noOfNotesSpawned = 0;

    public float notesLandingtime = 1.9f;
    public float stringLandingtime = 2.5f;

    [SerializeField] private AudioSource levelSong;

    // Start is called before the first frame update
    void Start()
    {
        spawnerScp.notesFallingSpeed = 2500f;
        spawnerScp.stringMovementSpeed = 2000f;
        time = Time.time;
        delayTime = Time.time;
        levelSong.Play();
        //Debug.Log(levelSong.volume);
    }

    void CreateStrings()
    {
        //Debug.Log("CreateStrings called");
        if (Time.time - delayTime >= stringSpawningPeriod)
        {
            //Debug.Log("spawnString");
            delayTime = Time.time;
            spawnerScp.SCH_SpawnString();
            noOfStringsSpawned++;
        }
    }

    void MovingUpwards()
    {
        //Debug.Log(Time.time);
        delayTime = Time.time;
        spawnerScp.SCH_MoveUpwards(); 
    }

    void MovingDownwards()
    {
        delayTime = Time.time;
        spawnerScp.SCH_MoveDownwards();
    }

    void GenerateWhileUpward()
    {
        spawnerScp.SCH_MoveUpwards();
        if (Time.time - delayTime >= stringSpawningPeriod)
        {
            //Debug.Log("spawnString");
            delayTime = Time.time;
            spawnerScp.SCH_SpawnString();
            noOfStringsSpawned++;
        }
    }

    void GenerateWhileDownward()
    {
        spawnerScp.SCH_MoveDownwards();
        if (Time.time - delayTime >= stringSpawningPeriod)
        {
            //Debug.Log("spawnString");
            delayTime = Time.time;
            spawnerScp.SCH_SpawnString();
            noOfStringsSpawned++;
        }
    }

    void GenerateRedNote()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnRedNote();
            noOfNotesSpawned++;
        }
        NoteSpawned = true;
    }

    void GenerateYellowNote()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnYellowNote();
            noOfNotesSpawned++;
        }
        NoteSpawned = true;
    }

    void GenerateGreenNote()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnGreenNote();
            noOfNotesSpawned++;
        }
        NoteSpawned = true;
    }

    void GenerateBlueNote()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnBlueNote();
            noOfNotesSpawned++;
        }
        NoteSpawned = true;
    }

    void Generate14Notes()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnRedNote();
            spawnerScp.SCH_SpawnBlueNote();
            noOfNotesSpawned += 2;
        }
        NoteSpawned = true;
    }

    void Generate23Notes()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnYellowNote();
            spawnerScp.SCH_SpawnGreenNote();
            noOfNotesSpawned += 2;
        }
        NoteSpawned = true;
    }

    void Generate12Notes()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnYellowNote();
            spawnerScp.SCH_SpawnRedNote();
            noOfNotesSpawned += 2;
        }
        NoteSpawned = true;
    }

    void Generate34Notes()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnBlueNote();
            spawnerScp.SCH_SpawnGreenNote();
            noOfNotesSpawned += 2;
        }
        NoteSpawned = true;
    }

    void Generate13Notes()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnRedNote();
            spawnerScp.SCH_SpawnGreenNote();
            noOfNotesSpawned += 2;
        }
        NoteSpawned = true;
    }

    void Generate24Notes()
    {
        if (!NoteSpawned)
        {
            spawnerScp.SCH_SpawnYellowNote();
            spawnerScp.SCH_SpawnBlueNote();
            noOfNotesSpawned += 2;
        }
        NoteSpawned = true;
    }


    // Update is called once per frame
    void Update()
    {

        //Control music volume so that it was not too loud
        if (Time.time - time >= 54 && Time.time - time <= 54.1)
        {
            levelSong.volume = 0.85f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 61.7 && Time.time - time <= 61.8)
        {
            levelSong.volume = 0.7f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 69.9 && Time.time - time <= 70)
        {
            levelSong.volume = 0.6f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 76.8 && Time.time - time <= 76.9)
        {
            levelSong.volume = 0.55f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 83.7 && Time.time - time <= 83.8)
        {
            levelSong.volume = 0.4f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 90.6 && Time.time - time <= 90.7)
        {
            levelSong.volume = 0.25f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 108.6 && Time.time - time <= 108.7)
        {
            levelSong.volume = 0.2f;
            //Debug.Log(Time.time);
        }

        if (Time.time - time >= 120 && Time.time - time <= 120.1)
        {
            levelSong.volume = 0.25f;
            //Debug.Log(Time.time);
        }



        //Notes timing

        if ((Time.time - time + stringLandingtime >= 4) && Time.time - time + stringLandingtime <= 4.3)
        {
            MovingUpwards();
        }

        if (Time.time - time + notesLandingtime >= 5.45 && Time.time - time + notesLandingtime < 5.55)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 5.55 && Time.time - time + notesLandingtime < 5.65)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 6.6 && Time.time - time + notesLandingtime < 6.6)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 6.7 && Time.time - time + notesLandingtime < 6.8)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 7.73 && Time.time - time + notesLandingtime < 7.83)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 7.83 && Time.time - time + notesLandingtime < 8)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 8.7 && Time.time - time + notesLandingtime < 8.8)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 8.8 && Time.time - time + notesLandingtime < 8.9)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 9.82 && Time.time - time + notesLandingtime < 9.92)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 9.92 && Time.time - time + notesLandingtime < 10)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 10.92 && Time.time - time + notesLandingtime < 11.02)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 11.02 && Time.time - time + notesLandingtime < 11.12)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 11.95 && Time.time - time + notesLandingtime < 12.05)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 12.05 && Time.time - time + notesLandingtime < 12.15)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 13) && (Time.time - time + stringLandingtime < 14))
        {
            CreateStrings();
        }

        if (Time.time - time + notesLandingtime >= 14.1 && Time.time - time + notesLandingtime < 14.2)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 14.2 && Time.time - time + notesLandingtime < 14.3)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 15.08 && Time.time - time + notesLandingtime < 15.18)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 15.18 && Time.time - time + notesLandingtime < 15.28)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 16.16 && Time.time - time + notesLandingtime < 16.26)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 16.26 && Time.time - time + notesLandingtime < 16.36)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 17.15 && Time.time - time + notesLandingtime < 17.25)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 17.25 && Time.time - time + notesLandingtime < 17.35)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 18.28 && Time.time - time + notesLandingtime < 18.38)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 18.38 && Time.time - time + notesLandingtime < 18.48)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 19.33 && Time.time - time + notesLandingtime < 19.43)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 19.43 && Time.time - time + notesLandingtime < 19.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 20.34 && Time.time - time + notesLandingtime < 20.44)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 20.44 && Time.time - time + notesLandingtime < 20.54)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 20.73 && Time.time - time + notesLandingtime < 20.83)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 20.83 && Time.time - time + notesLandingtime < 20.93)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 21.25 && Time.time - time + notesLandingtime < 21.35)
        {
            Generate14Notes();
        }

        if (Time.time - time + notesLandingtime >= 21.35 && Time.time - time + notesLandingtime < 21.45)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 22.33 && Time.time - time + notesLandingtime < 22.43)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 22.43 && Time.time - time + notesLandingtime < 22.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 22.8 && Time.time - time + notesLandingtime < 22.9)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 22.9 && Time.time - time + notesLandingtime < 23)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 23.33 && Time.time - time + notesLandingtime < 23.43)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 23.43 && Time.time - time + notesLandingtime < 23.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 24.28 && Time.time - time + notesLandingtime < 24.38)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 24.38 && Time.time - time + notesLandingtime < 24.48)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 24.7 && Time.time - time + notesLandingtime < 24.8)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 24.8 && Time.time - time + notesLandingtime < 24.9)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 25.33 && Time.time - time + notesLandingtime < 25.43)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 25.43 && Time.time - time + notesLandingtime < 25.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 25.8 && Time.time - time + notesLandingtime < 25.9)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 25.9 && Time.time - time + notesLandingtime < 26)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 26.33 && Time.time - time + notesLandingtime < 26.43)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 26.43 && Time.time - time + notesLandingtime < 26.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 26.76 && Time.time - time + notesLandingtime < 26.86)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 26.86 && Time.time - time + notesLandingtime < 26.96)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 27.34 && Time.time - time + notesLandingtime < 27.44)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 27.44 && Time.time - time + notesLandingtime < 27.54)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 28.32 && Time.time - time + notesLandingtime < 28.42)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 28.42 && Time.time - time + notesLandingtime < 28.52)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 28.75 && Time.time - time + notesLandingtime < 28.85)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 28.85 && Time.time - time + notesLandingtime < 28.95)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 29.3) && (Time.time - time + stringLandingtime <= 30.3))
        {
            GenerateWhileDownward();
        }

        if (Time.time - time + notesLandingtime >= 30.33 && Time.time - time + notesLandingtime < 30.43)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 30.43 && Time.time - time + notesLandingtime < 30.44)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 30.9 && Time.time - time + notesLandingtime < 31)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 31 && Time.time - time + notesLandingtime < 31.1)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 31.35 && Time.time - time + notesLandingtime < 31.45)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 31.45 && Time.time - time + notesLandingtime < 31.55)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 31.83 && Time.time - time + notesLandingtime < 31.93)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 31.93 && Time.time - time + notesLandingtime < 32.03)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 32.27 && Time.time - time + notesLandingtime < 32.37)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 32.37 && Time.time - time + notesLandingtime < 32.47)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 32.79 && Time.time - time + notesLandingtime < 32.89)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 32.89 && Time.time - time + notesLandingtime < 32.99)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 33.8 && Time.time - time + notesLandingtime < 33.9)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 33.9 && Time.time - time + notesLandingtime < 34)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 34.52 && Time.time - time + notesLandingtime < 34.62)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 34.62 && Time.time - time + notesLandingtime < 34.72)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 34.8 && Time.time - time + notesLandingtime < 34.9)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 34.9 && Time.time - time + notesLandingtime < 35)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 35.32 && Time.time - time + notesLandingtime < 35.42)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 35.42 && Time.time - time + notesLandingtime < 35.52)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 36.27 && Time.time - time + notesLandingtime < 36.37)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 36.37 && Time.time - time + notesLandingtime < 36.47)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 36.8 && Time.time - time + notesLandingtime < 36.9)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 36.9 && Time.time - time + notesLandingtime < 37)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 35.1) && Time.time - time + stringLandingtime <= 35.4)
        {
            MovingUpwards();
        }
        
        if ((Time.time - time + stringLandingtime >= 37.32) && (Time.time - time + stringLandingtime < 38.2))
        {
            CreateStrings();
        }

        if (Time.time - time + notesLandingtime >= 38.44 && Time.time - time + notesLandingtime < 38.49)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 38.49 && Time.time - time + notesLandingtime < 38.54)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 38.7 && Time.time - time + notesLandingtime < 38.75)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 38.75 && Time.time - time + notesLandingtime < 38.8)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 38.89 && Time.time - time + notesLandingtime < 38.94)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 38.94 && Time.time - time + notesLandingtime < 39)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 39.18 && Time.time - time + notesLandingtime < 39.23)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 39.23 && Time.time - time + notesLandingtime < 39.28)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 39.4 && Time.time - time + notesLandingtime < 39.45)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 39.45 && Time.time - time + notesLandingtime < 39.5)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 39.7 && Time.time - time + notesLandingtime < 39.75)
        {
            GenerateGreenNote();
        }

        if (Time.time - time + notesLandingtime >= 39.75 && Time.time - time + notesLandingtime < 39.8)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 39.95 && Time.time - time + notesLandingtime < 40)
        {
            GenerateBlueNote();
        }

        if (Time.time - time + notesLandingtime >= 40 && Time.time - time + notesLandingtime < 40.05)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 40.47 && Time.time - time + notesLandingtime < 40.52)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 40.52 && Time.time - time + notesLandingtime < 40.57)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 40.73 && Time.time - time + notesLandingtime < 40.78)
        {
            GenerateYellowNote();
        }

        if (Time.time - time + notesLandingtime >= 40.78 && Time.time - time + notesLandingtime < 40.83)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 40.94 && Time.time - time + notesLandingtime < 40.99)
        {
            GenerateRedNote();
        }

        if (Time.time - time + notesLandingtime >= 40.99 && Time.time - time + notesLandingtime < 41.04)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 41.45 && Time.time - time + notesLandingtime < 41.5)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 41.5 && Time.time - time + notesLandingtime < 41.55)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 41.72 && Time.time - time + notesLandingtime < 41.77)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 41.77 && Time.time - time + notesLandingtime < 41.82)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 41.98 && Time.time - time + notesLandingtime < 42.03)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 42.03 && Time.time - time + notesLandingtime < 42.08)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 42.52 && Time.time - time + notesLandingtime < 42.57)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 42.57 && Time.time - time + notesLandingtime < 42.62)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 42.73 && Time.time - time + notesLandingtime < 42.78)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 42.78 && Time.time - time + notesLandingtime < 42.83)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 42.96 && Time.time - time + notesLandingtime < 43.01)
        {
            GenerateBlueNote();
        } 
        if (Time.time - time + notesLandingtime >= 43.01 && Time.time - time + notesLandingtime < 43.06)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 43.22 && Time.time - time + notesLandingtime < 43.27)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 43.27 && Time.time - time + notesLandingtime < 43.32)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 43.5 && Time.time - time + notesLandingtime < 43.55)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 43.55 && Time.time - time + notesLandingtime < 43.6)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 43.71 && Time.time - time + notesLandingtime < 43.76)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 43.76 && Time.time - time + notesLandingtime < 43.81)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 43.96 && Time.time - time + notesLandingtime < 44.01)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 44.01 && Time.time - time + notesLandingtime < 44.06)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 44.21 && Time.time - time + notesLandingtime < 44.26)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 44.26 && Time.time - time + notesLandingtime < 44.31)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 44.48 && Time.time - time + notesLandingtime < 44.53)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 44.53 && Time.time - time + notesLandingtime < 44.58)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 44.67 && Time.time - time + notesLandingtime < 44.72)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 44.72 && Time.time - time + notesLandingtime < 44.77)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 44.94 && Time.time - time + notesLandingtime < 44.99)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 44.99 && Time.time - time + notesLandingtime < 45.04)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 45.15 && Time.time - time + notesLandingtime < 45.2)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 45.2 && Time.time - time + notesLandingtime < 45.25)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 45.45 && Time.time - time + notesLandingtime < 45.5)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 45.5 && Time.time - time + notesLandingtime < 45.55)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 46.5 && Time.time - time + notesLandingtime < 46.55)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 46.55 && Time.time - time + notesLandingtime < 46.6)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 46.75 && Time.time - time + notesLandingtime < 46.8)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 46.8 && Time.time - time + notesLandingtime < 46.85)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 47.03 && Time.time - time + notesLandingtime < 47.08)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 47.08 && Time.time - time + notesLandingtime < 47.13)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 47.26 && Time.time - time + notesLandingtime < 47.31)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 47.31 && Time.time - time + notesLandingtime < 47.36)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 47.47 && Time.time - time + notesLandingtime < 47.52)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 47.52 && Time.time - time + notesLandingtime < 47.57)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 47.75 && Time.time - time + notesLandingtime < 47.80)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 47.80 && Time.time - time + notesLandingtime < 47.85)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 48.05 && Time.time - time + notesLandingtime < 48.1)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 48.1 && Time.time - time + notesLandingtime < 48.15)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 48.52 && Time.time - time + notesLandingtime < 48.57)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 48.57 && Time.time - time + notesLandingtime < 48.62)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 48.78 && Time.time - time + notesLandingtime < 48.83)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 48.83 && Time.time - time + notesLandingtime < 48.88)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 49.04 && Time.time - time + notesLandingtime < 49.09)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 49.09 && Time.time - time + notesLandingtime < 49.14)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 49.55 && Time.time - time + notesLandingtime < 49.6)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 49.6 && Time.time - time + notesLandingtime < 49.65)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 49.8 && Time.time - time + notesLandingtime < 49.85)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 49.85 && Time.time - time + notesLandingtime < 49.9)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 50.05 && Time.time - time + notesLandingtime < 50.1)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 50.1 && Time.time - time + notesLandingtime < 50.15)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 50.57 && Time.time - time + notesLandingtime < 50.62)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 50.62 && Time.time - time + notesLandingtime < 50.67)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 51.06 && Time.time - time + notesLandingtime < 51.11)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 51.11 && Time.time - time + notesLandingtime < 51.16)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 51.58 && Time.time - time + notesLandingtime < 51.63)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 51.63 && Time.time - time + notesLandingtime < 51.68)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 52.02 && Time.time - time + notesLandingtime < 52.07)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 52.07 && Time.time - time + notesLandingtime < 52.12)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 52.48 && Time.time - time + notesLandingtime < 52.53)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 52.53 && Time.time - time + notesLandingtime < 52.58)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 52.98 && Time.time - time + notesLandingtime < 53.03)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 53.03 && Time.time - time + notesLandingtime < 53.08)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 53.49 && Time.time - time + notesLandingtime < 53.54)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 53.54 && Time.time - time + notesLandingtime < 53.59)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 54.57 && Time.time - time + notesLandingtime < 54.62)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 54.62 && Time.time - time + notesLandingtime < 54.67)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 55.1 && Time.time - time + notesLandingtime < 55.15)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 55.15 && Time.time - time + notesLandingtime < 55.2)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 55.53 && Time.time - time + notesLandingtime < 55.58)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 55.58 && Time.time - time + notesLandingtime < 55.63)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 56.06 && Time.time - time + notesLandingtime < 56.11)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 56.11 && Time.time - time + notesLandingtime < 56.16)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 56.52 && Time.time - time + notesLandingtime < 56.57)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 56.57 && Time.time - time + notesLandingtime < 56.62)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 57.02 && Time.time - time + notesLandingtime < 57.07)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 57.07 && Time.time - time + notesLandingtime < 57.12)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 57.43 && Time.time - time + notesLandingtime < 57.48)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 57.48 && Time.time - time + notesLandingtime < 57.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 57.98 && Time.time - time + notesLandingtime < 58.03)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 58.03 && Time.time - time + notesLandingtime < 58.08)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 58.48 && Time.time - time + notesLandingtime < 58.53)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 58.53 && Time.time - time + notesLandingtime < 58.58)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 58.92 && Time.time - time + notesLandingtime < 58.97)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 58.97 && Time.time - time + notesLandingtime < 59.02)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 59.46 && Time.time - time + notesLandingtime < 59.51)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 59.51 && Time.time - time + notesLandingtime < 59.56)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 59.92 && Time.time - time + notesLandingtime < 59.97)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 59.97 && Time.time - time + notesLandingtime < 60.02)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 60.38 && Time.time - time + notesLandingtime < 60.43)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 60.43 && Time.time - time + notesLandingtime < 60.48)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 60.87 && Time.time - time + notesLandingtime < 60.92)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 60.92 && Time.time - time + notesLandingtime < 60.97)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 61.84 && Time.time - time + notesLandingtime < 61.89)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 61.89 && Time.time - time + notesLandingtime < 61.94)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 59) && Time.time - time + stringLandingtime <= 59.4)
        {
            MovingDownwards();
        }

        if ((Time.time - time + stringLandingtime >= 61.36) && (Time.time - time + stringLandingtime < 62.34))
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 62.34 && Time.time - time + notesLandingtime < 62.39)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 62.39 && Time.time - time + notesLandingtime < 62.44)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 62.82 && Time.time - time + notesLandingtime < 62.87)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 62.87 && Time.time - time + notesLandingtime < 62.92)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 63.31 && Time.time - time + notesLandingtime < 63.36)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 63.36 && Time.time - time + notesLandingtime < 63.41)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 63.77 && Time.time - time + notesLandingtime < 63.82)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 63.82 && Time.time - time + notesLandingtime < 63.87)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 64.23 && Time.time - time + notesLandingtime < 64.28)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 64.28 && Time.time - time + notesLandingtime < 64.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 64.72 && Time.time - time + notesLandingtime < 64.77)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 64.77 && Time.time - time + notesLandingtime < 64.82)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 65.19 && Time.time - time + notesLandingtime < 65.24)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 65.24 && Time.time - time + notesLandingtime < 65.29)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 65.66 && Time.time - time + notesLandingtime < 65.71)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 65.71 && Time.time - time + notesLandingtime < 65.76)
        {
            NoteSpawned = false;
        }
         
        if (Time.time - time + notesLandingtime >= 66.13 && Time.time - time + notesLandingtime < 66.18)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 66.18 && Time.time - time + notesLandingtime < 66.23)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 66.61 && Time.time - time + notesLandingtime < 66.66)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 66.66 && Time.time - time + notesLandingtime < 66.71)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 67.08 && Time.time - time + notesLandingtime < 67.13)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 67.13 && Time.time - time + notesLandingtime < 67.18)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 67.53 && Time.time - time + notesLandingtime < 67.58)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 67.58 && Time.time - time + notesLandingtime < 67.63)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 68 && Time.time - time + notesLandingtime < 68.05)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 68.05 && Time.time - time + notesLandingtime < 68.1)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 68.46 && Time.time - time + notesLandingtime < 68.51)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 68.51 && Time.time - time + notesLandingtime < 68.56)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 68.93 && Time.time - time + notesLandingtime < 68.98)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 68.98 && Time.time - time + notesLandingtime < 69.03)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + stringLandingtime >= 68.95 && Time.time - time + stringLandingtime < 69.88)
        {
            CreateStrings();
        }


        if (Time.time - time + notesLandingtime >= 69.89 && Time.time - time + notesLandingtime < 69.94)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 69.94 && Time.time - time + notesLandingtime < 69.99)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 70.36 && Time.time - time + notesLandingtime < 70.41)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 70.41 && Time.time - time + notesLandingtime < 70.46)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 70.84 && Time.time - time + notesLandingtime < 70.89)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 70.89 && Time.time - time + notesLandingtime < 70.94)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 71.3 && Time.time - time + notesLandingtime < 71.35)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 71.35 && Time.time - time + notesLandingtime < 71.4)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 71.74 && Time.time - time + notesLandingtime < 71.79)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 71.79 && Time.time - time + notesLandingtime < 71.84)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 72.19 && Time.time - time + notesLandingtime < 72.24)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 72.24 && Time.time - time + notesLandingtime < 72.29)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 72.66 && Time.time - time + notesLandingtime < 72.71)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 72.71 && Time.time - time + notesLandingtime < 72.76)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 73.1 && Time.time - time + notesLandingtime < 73.15)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 73.15 && Time.time - time + notesLandingtime < 73.2)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 73.58 && Time.time - time + notesLandingtime < 73.63)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 73.63 && Time.time - time + notesLandingtime < 73.68)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 74.01 && Time.time - time + notesLandingtime < 74.06)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 74.06 && Time.time - time + notesLandingtime < 74.11)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 74.48 && Time.time - time + notesLandingtime < 74.53)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 74.53 && Time.time - time + notesLandingtime < 74.58)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 74.92 && Time.time - time + notesLandingtime < 74.97)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 74.97 && Time.time - time + notesLandingtime < 75.02)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 75.32 && Time.time - time + notesLandingtime < 75.37)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 75.37 && Time.time - time + notesLandingtime < 75.42)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 75.79 && Time.time - time + notesLandingtime < 75.84)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 75.84 && Time.time - time + notesLandingtime < 75.89)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 76.71 && Time.time - time + notesLandingtime < 76.76)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 76.76 && Time.time - time + notesLandingtime < 76.81)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 76.25) && (Time.time - time + stringLandingtime < 77.15))
        {
            GenerateWhileDownward();
        }


        if (Time.time - time + notesLandingtime >= 77.16 && Time.time - time + notesLandingtime < 77.21)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 77.21 && Time.time - time + notesLandingtime < 77.26)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 77.61 && Time.time - time + notesLandingtime < 77.66)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 77.66 && Time.time - time + notesLandingtime < 77.71)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 78.04 && Time.time - time + notesLandingtime < 78.09)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 78.09 && Time.time - time + notesLandingtime < 78.14)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 78.48 && Time.time - time + notesLandingtime < 78.53)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 78.53 && Time.time - time + notesLandingtime < 78.58)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 78.92 && Time.time - time + notesLandingtime < 78.97)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 78.97 && Time.time - time + notesLandingtime < 79.02)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 79.34 && Time.time - time + notesLandingtime < 79.39)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 79.39 && Time.time - time + notesLandingtime < 79.44)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 79.8 && Time.time - time + notesLandingtime < 79.85)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 79.85 && Time.time - time + notesLandingtime < 79.9)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 80.23 && Time.time - time + notesLandingtime < 80.28)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 80.28 && Time.time - time + notesLandingtime < 80.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 80.67 && Time.time - time + notesLandingtime < 80.72)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 80.72 && Time.time - time + notesLandingtime < 80.77)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 81.1 && Time.time - time + notesLandingtime < 81.15)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 81.15 && Time.time - time + notesLandingtime < 81.2)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 81.54 && Time.time - time + notesLandingtime < 81.59)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 81.59 && Time.time - time + notesLandingtime < 81.64)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 81.96 && Time.time - time + notesLandingtime < 82.01)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 82.01 && Time.time - time + notesLandingtime < 82.06)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 82.4 && Time.time - time + notesLandingtime < 82.45)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 82.45 && Time.time - time + notesLandingtime < 82.5)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 82.84 && Time.time - time + notesLandingtime < 82.89)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 82.89 && Time.time - time + notesLandingtime < 82.94)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 83.26 && Time.time - time + notesLandingtime < 83.31)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 83.31 && Time.time - time + notesLandingtime < 83.36)
        {
            NoteSpawned = false;
        }



        if (Time.time - time + notesLandingtime >= 84.1 && Time.time - time + notesLandingtime < 84.15)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 84.15 && Time.time - time + notesLandingtime < 84.2)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 84.55 && Time.time - time + notesLandingtime < 84.6)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 84.6 && Time.time - time + notesLandingtime < 84.65)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 84.95 && Time.time - time + notesLandingtime < 85)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 85 && Time.time - time + notesLandingtime < 85.05)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 85.43 && Time.time - time + notesLandingtime < 85.48)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 85.48 && Time.time - time + notesLandingtime < 85.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 85.87 && Time.time - time + notesLandingtime < 85.92)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 85.92 && Time.time - time + notesLandingtime < 85.97)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 86.28 && Time.time - time + notesLandingtime < 86.33)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 86.33 && Time.time - time + notesLandingtime < 86.38)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 86.66 && Time.time - time + notesLandingtime < 86.71)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 86.71 && Time.time - time + notesLandingtime < 86.76)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 87.12 && Time.time - time + notesLandingtime < 87.17)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 87.17 && Time.time - time + notesLandingtime < 87.22)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 87.53 && Time.time - time + notesLandingtime < 87.58)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 87.58 && Time.time - time + notesLandingtime < 87.63)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 87.95 && Time.time - time + notesLandingtime < 88)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 88 && Time.time - time + notesLandingtime < 88.05)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 88.37 && Time.time - time + notesLandingtime < 88.42)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 88.42 && Time.time - time + notesLandingtime < 88.47)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 88.78 && Time.time - time + notesLandingtime < 88.83)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 88.83 && Time.time - time + notesLandingtime < 88.88)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 89.18 && Time.time - time + notesLandingtime < 89.23)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 89.23 && Time.time - time + notesLandingtime < 89.28)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 89.61 && Time.time - time + notesLandingtime < 89.66)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 89.66 && Time.time - time + notesLandingtime < 89.71)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 90 && Time.time - time + notesLandingtime < 90.05)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 90.05 && Time.time - time + notesLandingtime < 90.1)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 90) && Time.time - time + stringLandingtime <= 90.8)
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 90.82 && Time.time - time + notesLandingtime < 90.87)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 90.87 && Time.time - time + notesLandingtime < 90.92)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 91.26 && Time.time - time + notesLandingtime < 91.31)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 91.31 && Time.time - time + notesLandingtime < 91.36)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 91.64 && Time.time - time + notesLandingtime < 91.69)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 91.69 && Time.time - time + notesLandingtime < 91.74)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 92.06 && Time.time - time + notesLandingtime < 92.11)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 92.11 && Time.time - time + notesLandingtime < 92.16)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 92.43 && Time.time - time + notesLandingtime < 92.48)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 92.48 && Time.time - time + notesLandingtime < 92.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 92.85 && Time.time - time + notesLandingtime < 92.9)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 92.9 && Time.time - time + notesLandingtime < 92.95)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 93.28 && Time.time - time + notesLandingtime < 93.33)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 93.33 && Time.time - time + notesLandingtime < 93.38)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 93.67 && Time.time - time + notesLandingtime < 93.72)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 93.72 && Time.time - time + notesLandingtime < 93.77)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 93.7) && Time.time - time + stringLandingtime <= 94.05)
        {
            MovingDownwards();
        }

        if (Time.time - time + notesLandingtime >= 94.08 && Time.time - time + notesLandingtime < 94.13)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 94.13 && Time.time - time + notesLandingtime < 94.18)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 94.48 && Time.time - time + notesLandingtime < 94.53)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 94.53 && Time.time - time + notesLandingtime < 94.58)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 94.48) && Time.time - time + stringLandingtime <= 94.87)
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 94.88 && Time.time - time + notesLandingtime < 94.93)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 94.93 && Time.time - time + notesLandingtime < 94.98)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 94.88) && Time.time - time + stringLandingtime <= 95.27)
        {
            GenerateWhileDownward();
        }

        if (Time.time - time + notesLandingtime >= 95.28 && Time.time - time + notesLandingtime < 95.33)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 95.33 && Time.time - time + notesLandingtime < 95.38)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 95.28) && Time.time - time + stringLandingtime <= 95.80)
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 95.68 && Time.time - time + notesLandingtime < 95.73)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 95.73 && Time.time - time + notesLandingtime < 95.78)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 95.68) && Time.time - time + stringLandingtime <= 96.05)
        {
            GenerateWhileDownward();
        }

        if (Time.time - time + notesLandingtime >= 96.06 && Time.time - time + notesLandingtime < 96.11)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 96.11 && Time.time - time + notesLandingtime < 96.16)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 96.06) && Time.time - time + stringLandingtime <= 96.39)
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 96.4 && Time.time - time + notesLandingtime < 96.45)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 96.45 && Time.time - time + notesLandingtime < 96.5)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 96.4) && Time.time - time + stringLandingtime <= 96.8)
        {
            GenerateWhileDownward();
        }

        if (Time.time - time + notesLandingtime >= 96.81 && Time.time - time + notesLandingtime < 96.86)
        {
            Generate34Notes();
        }
        if (Time.time - time + notesLandingtime >= 96.86 && Time.time - time + notesLandingtime < 96.91)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 96.81) && Time.time - time + stringLandingtime <= 97.22)
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 97.61 && Time.time - time + notesLandingtime < 97.66)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 97.66 && Time.time - time + notesLandingtime < 97.71)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 97.93 && Time.time - time + notesLandingtime < 97.98)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 97.98 && Time.time - time + notesLandingtime < 98.03)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 98.33 && Time.time - time + notesLandingtime < 98.38)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 98.38 && Time.time - time + notesLandingtime < 98.43)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 98.7 && Time.time - time + notesLandingtime < 98.75)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 98.75 && Time.time - time + notesLandingtime < 98.8)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 99.13 && Time.time - time + notesLandingtime < 99.18)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 99.18 && Time.time - time + notesLandingtime < 99.23)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 99.48 && Time.time - time + notesLandingtime < 99.53)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 99.53 && Time.time - time + notesLandingtime < 99.58)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 99.83 && Time.time - time + notesLandingtime < 99.88)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 99.88 && Time.time - time + notesLandingtime < 99.93)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 100.23 && Time.time - time + notesLandingtime < 100.28)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 100.28 && Time.time - time + notesLandingtime < 100.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 100.6 && Time.time - time + notesLandingtime < 100.65)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 100.65 && Time.time - time + notesLandingtime < 100.7)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 100.98 && Time.time - time + notesLandingtime < 101.03)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 101.03 && Time.time - time + notesLandingtime < 101.08)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 101.33 && Time.time - time + notesLandingtime < 101.38)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 101.38 && Time.time - time + notesLandingtime < 101.43)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 101.73 && Time.time - time + notesLandingtime < 101.78)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 101.78 && Time.time - time + notesLandingtime < 101.83)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 102.09 && Time.time - time + notesLandingtime < 102.14)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 102.14 && Time.time - time + notesLandingtime < 102.19)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 102.45 && Time.time - time + notesLandingtime < 102.5)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 102.5 && Time.time - time + notesLandingtime < 102.55)
        {
            NoteSpawned = false;
        }



        if ((Time.time - time + stringLandingtime >= 102.45) && Time.time - time + stringLandingtime <= 103.19)
        {
            CreateStrings();
        }

        if (Time.time - time + notesLandingtime >= 103.23 && Time.time - time + notesLandingtime < 103.28)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 103.28 && Time.time - time + notesLandingtime < 103.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 103.62 && Time.time - time + notesLandingtime < 103.67)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 103.67 && Time.time - time + notesLandingtime < 103.72)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 103.99 && Time.time - time + notesLandingtime < 104.04)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 104.04 && Time.time - time + notesLandingtime < 104.09)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 104.33 && Time.time - time + notesLandingtime < 104.38)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 104.38 && Time.time - time + notesLandingtime < 104.43)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 104.72 && Time.time - time + notesLandingtime < 104.77)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 104.77 && Time.time - time + notesLandingtime < 104.82)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 105.07 && Time.time - time + notesLandingtime < 105.12)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 105.12 && Time.time - time + notesLandingtime < 105.17)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 105.43 && Time.time - time + notesLandingtime < 105.48)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 105.48 && Time.time - time + notesLandingtime < 105.53)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 105.83 && Time.time - time + notesLandingtime < 105.88)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 105.88 && Time.time - time + notesLandingtime < 105.93)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 106.23 && Time.time - time + notesLandingtime < 106.28)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 106.28 && Time.time - time + notesLandingtime < 106.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 106.58 && Time.time - time + notesLandingtime < 106.63)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 106.63 && Time.time - time + notesLandingtime < 106.68)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 106.92 && Time.time - time + notesLandingtime < 106.97)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 106.97 && Time.time - time + notesLandingtime < 107.02)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 107.32 && Time.time - time + notesLandingtime < 107.37)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 107.37 && Time.time - time + notesLandingtime < 107.42)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 107.67 && Time.time - time + notesLandingtime < 107.72)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 107.72 && Time.time - time + notesLandingtime < 107.77)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 108.03 && Time.time - time + notesLandingtime < 108.08)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 108.08 && Time.time - time + notesLandingtime < 108.13)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 108.4 && Time.time - time + notesLandingtime < 108.45)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 108.45 && Time.time - time + notesLandingtime < 108.5)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 108.82 && Time.time - time + notesLandingtime < 108.87)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 108.87 && Time.time - time + notesLandingtime < 108.92)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 109.21 && Time.time - time + notesLandingtime < 109.26)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 109.26 && Time.time - time + notesLandingtime < 109.31)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 109.55 && Time.time - time + notesLandingtime < 109.6)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 109.6 && Time.time - time + notesLandingtime < 109.65)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 109.94 && Time.time - time + notesLandingtime < 109.99)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 109.99 && Time.time - time + notesLandingtime < 110.04)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 110.33 && Time.time - time + notesLandingtime < 110.38)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 110.38 && Time.time - time + notesLandingtime < 110.43)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 110.69 && Time.time - time + notesLandingtime < 110.74)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 110.74 && Time.time - time + notesLandingtime < 110.79)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 111.05 && Time.time - time + notesLandingtime < 111.1)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 111.1 && Time.time - time + notesLandingtime < 111.15)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 111.42 && Time.time - time + notesLandingtime < 111.47)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 111.47 && Time.time - time + notesLandingtime < 111.52)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 111.78 && Time.time - time + notesLandingtime < 111.83)
        {
            Generate24Notes();
        }
        if (Time.time - time + notesLandingtime >= 111.83 && Time.time - time + notesLandingtime < 111.88)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 112.15 && Time.time - time + notesLandingtime < 112.2)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 112.2 && Time.time - time + notesLandingtime < 112.25)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 112.28 && Time.time - time + notesLandingtime < 112.33)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 112.33 && Time.time - time + notesLandingtime < 112.38)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 112.55 && Time.time - time + notesLandingtime < 112.6)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 112.6 && Time.time - time + notesLandingtime < 112.65)
        {
            NoteSpawned = false;
        }
        
        if (Time.time - time + notesLandingtime >= 112.73 && Time.time - time + notesLandingtime < 112.78)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 112.78 && Time.time - time + notesLandingtime < 112.83)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 112.9 && Time.time - time + notesLandingtime < 112.95)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 112.95 && Time.time - time + notesLandingtime < 113)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 113.08 && Time.time - time + notesLandingtime < 113.13)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 113.13 && Time.time - time + notesLandingtime < 113.18)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 113.26 && Time.time - time + notesLandingtime < 113.31)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 113.31 && Time.time - time + notesLandingtime < 113.36)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 113.63 && Time.time - time + notesLandingtime < 113.68)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 113.68 && Time.time - time + notesLandingtime < 113.73)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 113.96 && Time.time - time + notesLandingtime < 114.01)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 114.01 && Time.time - time + notesLandingtime < 114.06)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 114.34 && Time.time - time + notesLandingtime < 114.39)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 114.39 && Time.time - time + notesLandingtime < 114.44)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 115.13 && Time.time - time + notesLandingtime < 115.18)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 115.18 && Time.time - time + notesLandingtime < 115.23)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 115.42 && Time.time - time + notesLandingtime < 115.47)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 115.47 && Time.time - time + notesLandingtime < 115.52)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 115.79 && Time.time - time + notesLandingtime < 115.84)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 115.84 && Time.time - time + notesLandingtime < 115.89)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 116.16 && Time.time - time + notesLandingtime < 116.21)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 116.21 && Time.time - time + notesLandingtime < 116.26)
        {
            NoteSpawned = false;
        }


        if (Time.time - time + notesLandingtime >= 116.56 && Time.time - time + notesLandingtime < 116.61)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 116.61 && Time.time - time + notesLandingtime < 116.66)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 116.93 && Time.time - time + notesLandingtime < 116.98)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 116.98 && Time.time - time + notesLandingtime < 117.03)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 117.27 && Time.time - time + notesLandingtime < 117.32)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 117.32 && Time.time - time + notesLandingtime < 117.37)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 117.63 && Time.time - time + notesLandingtime < 117.68)
        {
            Generate13Notes();
        }
        if (Time.time - time + notesLandingtime >= 117.68 && Time.time - time + notesLandingtime < 117.73)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 118.02 && Time.time - time + notesLandingtime < 118.07)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 118.07 && Time.time - time + notesLandingtime < 118.12)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 118.16 && Time.time - time + notesLandingtime < 118.21)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 118.21 && Time.time - time + notesLandingtime < 118.26)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 118.38 && Time.time - time + notesLandingtime < 118.43)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 118.43 && Time.time - time + notesLandingtime < 118.48)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 118.52 && Time.time - time + notesLandingtime < 118.57)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 118.57 && Time.time - time + notesLandingtime < 118.62)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 118.72 && Time.time - time + notesLandingtime < 118.77)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 118.77 && Time.time - time + notesLandingtime < 118.82)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 118.88 && Time.time - time + notesLandingtime < 118.93)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 118.93 && Time.time - time + notesLandingtime < 118.98)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 119.03 && Time.time - time + notesLandingtime < 119.08)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 119.08 && Time.time - time + notesLandingtime < 119.13)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 119.45 && Time.time - time + notesLandingtime < 119.5)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 119.5 && Time.time - time + notesLandingtime < 119.55)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 119.81 && Time.time - time + notesLandingtime < 119.86)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 119.86 && Time.time - time + notesLandingtime < 119.91)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 120.19 && Time.time - time + notesLandingtime < 120.24)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 120.24 && Time.time - time + notesLandingtime < 120.29)
        {
            NoteSpawned = false;
        }


        if ((Time.time - time + stringLandingtime >= 120.95) && Time.time - time + stringLandingtime < 121.65)
        {
            GenerateWhileDownward();
        }

        if ((Time.time - time + stringLandingtime >= 121.65) && Time.time - time + stringLandingtime < 122.01)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 122.01) && Time.time - time + stringLandingtime < 122.34)
        {
            CreateStrings();
        }

        if ((Time.time - time + stringLandingtime >= 122.34) && Time.time - time + stringLandingtime < 123)
        {
            GenerateWhileDownward();
        }

        if ((Time.time - time + stringLandingtime >= 123.78) && Time.time - time + stringLandingtime < 124.48)
        {
            CreateStrings();
        }

        if ((Time.time - time + stringLandingtime >= 124.48) && Time.time - time + stringLandingtime < 125.18)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 125.18) && Time.time - time + stringLandingtime < 125.85)
        {
            GenerateWhileDownward();
        }

        if ((Time.time - time + stringLandingtime >= 125.85) && Time.time - time + stringLandingtime < 126.58)
        {
            CreateStrings();
        }


        if (Time.time - time + notesLandingtime >= 121.29 && Time.time - time + notesLandingtime < 121.34)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 121.34 && Time.time - time + notesLandingtime < 121.39)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 122 && Time.time - time + notesLandingtime < 122.05)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 122.05 && Time.time - time + notesLandingtime < 122)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 122.72 && Time.time - time + notesLandingtime < 122.77)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 122.77 && Time.time - time + notesLandingtime < 122.82)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 123.44 && Time.time - time + notesLandingtime < 123.49)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 123.49 && Time.time - time + notesLandingtime < 123.54)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 124.13 && Time.time - time + notesLandingtime < 124.18)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 124.18 && Time.time - time + notesLandingtime < 124.23)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 124.83 && Time.time - time + notesLandingtime < 124.88)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 124.88 && Time.time - time + notesLandingtime < 124.93)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 125.53 && Time.time - time + notesLandingtime < 125.58)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 125.58 && Time.time - time + notesLandingtime < 125.63)
        {
            NoteSpawned = false;
        }




        if ((Time.time - time + stringLandingtime >= 126.56) && Time.time - time + stringLandingtime < 127.23)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 127.23) && Time.time - time + stringLandingtime < 129.28)
        {
            CreateStrings();
        }

        if ((Time.time - time + stringLandingtime >= 129.28) && Time.time - time + stringLandingtime < 129.91)
        {
            GenerateWhileDownward();
        }

        if ((Time.time - time + stringLandingtime >= 129.91) && Time.time - time + stringLandingtime < 130.56)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 130.56) && Time.time - time + stringLandingtime < 131.25)
        {
            CreateStrings();
        }


        if (Time.time - time + notesLandingtime >= 126.23 && Time.time - time + notesLandingtime < 126.28)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 126.28 && Time.time - time + notesLandingtime < 126.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 126.91 && Time.time - time + notesLandingtime < 126.96)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 126.96 && Time.time - time + notesLandingtime < 127.01)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 127.59 && Time.time - time + notesLandingtime < 127.64)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 127.64 && Time.time - time + notesLandingtime < 127.69)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 128.27 && Time.time - time + notesLandingtime < 128.32)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 128.32 && Time.time - time + notesLandingtime < 128.37)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 128.9 && Time.time - time + notesLandingtime < 128.95)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 128.95 && Time.time - time + notesLandingtime < 129)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 129.59 && Time.time - time + notesLandingtime < 129.64)
        {
            GenerateBlueNote();
        }
        if (Time.time - time + notesLandingtime >= 129.64 && Time.time - time + notesLandingtime < 129.69)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 130.26 && Time.time - time + notesLandingtime < 130.31)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 130.31 && Time.time - time + notesLandingtime < 130.36)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 130.93 && Time.time - time + notesLandingtime < 130.98)
        {
            GenerateRedNote();
        }
        if (Time.time - time + notesLandingtime >= 130.98 && Time.time - time + notesLandingtime < 131.03)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 131.61 && Time.time - time + notesLandingtime < 131.66)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 131.66 && Time.time - time + notesLandingtime < 131.71)
        {
            NoteSpawned = false;
        }


        if ((Time.time - time + stringLandingtime >= 131.7) && Time.time - time + stringLandingtime < 131.95)
        {
            MovingDownwards();
        }

        if (Time.time - time + notesLandingtime >= 131.95 && Time.time - time + notesLandingtime < 132)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 132 && Time.time - time + notesLandingtime < 132.05)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 132.23 && Time.time - time + notesLandingtime < 132.28)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 132.28 && Time.time - time + notesLandingtime < 132.33)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 133.17 && Time.time - time + notesLandingtime < 133.22)
        {
            GenerateGreenNote();
        }
        if (Time.time - time + notesLandingtime >= 133.22 && Time.time - time + notesLandingtime < 133.27)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 133.54 && Time.time - time + notesLandingtime < 133.59)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 133.59 && Time.time - time + notesLandingtime < 133.64)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 134.55) && Time.time - time + stringLandingtime < 135.18)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 135.18) && Time.time - time + stringLandingtime < 135.79)
        {
            GenerateWhileDownward();
        }

        if ((Time.time - time + stringLandingtime >= 135.79) && Time.time - time + stringLandingtime < 136.68)
        {
            CreateStrings();
        }

        if ((Time.time - time + stringLandingtime >= 136.68) && Time.time - time + stringLandingtime < 137.01)
        {
            GenerateWhileDownward();
        }

        if (Time.time - time + notesLandingtime >= 137.01 && Time.time - time + notesLandingtime < 137.06)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 137.06 && Time.time - time + notesLandingtime < 137.11)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 137.34 && Time.time - time + notesLandingtime < 137.39)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 137.39 && Time.time - time + notesLandingtime < 137.44)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 138.25 && Time.time - time + notesLandingtime < 138.3)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 138.3 && Time.time - time + notesLandingtime < 138.35)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 138.57 && Time.time - time + notesLandingtime < 138.62)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 138.62 && Time.time - time + notesLandingtime < 138.67)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 139.55) && Time.time - time + stringLandingtime < 140.18)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 140.18) && Time.time - time + stringLandingtime < 140.79)
        {
            GenerateWhileDownward();
        }

        if ((Time.time - time + stringLandingtime >= 140.79) && Time.time - time + stringLandingtime < 141.68)
        {
            CreateStrings();
        }

        if ((Time.time - time + stringLandingtime >= 141.68) && Time.time - time + stringLandingtime < 142.01)
        {
            GenerateWhileUpward();
        }

        if (Time.time - time + notesLandingtime >= 142.01 && Time.time - time + notesLandingtime < 142.06)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 142.06 && Time.time - time + notesLandingtime < 142.11)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 142.34 && Time.time - time + notesLandingtime < 142.39)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 142.39 && Time.time - time + notesLandingtime < 142.44)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 143.25 && Time.time - time + notesLandingtime < 143.3)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 143.3 && Time.time - time + notesLandingtime < 143.35)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 143.57 && Time.time - time + notesLandingtime < 143.62)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 143.62 && Time.time - time + notesLandingtime < 143.67)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 144.38 && Time.time - time + notesLandingtime < 144.43)
        {
            GenerateYellowNote();
        }
        if (Time.time - time + notesLandingtime >= 144.43 && Time.time - time + notesLandingtime < 144.48)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 144.74 && Time.time - time + notesLandingtime < 144.79)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 144.79 && Time.time - time + notesLandingtime < 144.84)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 144.98 && Time.time - time + notesLandingtime < 145.03)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 145.03 && Time.time - time + notesLandingtime < 145.08)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 145.3 && Time.time - time + notesLandingtime < 145.35)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 145.35 && Time.time - time + notesLandingtime < 145.4)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 145.61 && Time.time - time + notesLandingtime < 145.66)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 145.66 && Time.time - time + notesLandingtime < 145.71)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 145.92 && Time.time - time + notesLandingtime < 145.97)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 145.97 && Time.time - time + notesLandingtime < 146.02)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 146.18 && Time.time - time + notesLandingtime < 146.23)
        {
            Generate23Notes();
        }
        if (Time.time - time + notesLandingtime >= 146.23 && Time.time - time + notesLandingtime < 146.28)
        {
            NoteSpawned = false;
        }

        if (Time.time - time + notesLandingtime >= 146.48 && Time.time - time + notesLandingtime < 146.53)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 146.53 && Time.time - time + notesLandingtime < 146.58)
        {
            NoteSpawned = false;
        }

        if ((Time.time - time + stringLandingtime >= 147) && Time.time - time + stringLandingtime < 147.2)
        {
            MovingDownwards();
        }

        if ((Time.time - time + stringLandingtime >= 148.03) && Time.time - time + stringLandingtime < 148.9)
        {
            GenerateWhileUpward();
        }

        if ((Time.time - time + stringLandingtime >= 148.9) && Time.time - time + stringLandingtime < 149.81)
        {
            GenerateWhileDownward();
        }

        if (Time.time - time + notesLandingtime >= 149.81 && Time.time - time + notesLandingtime < 149.86)
        {
            Generate14Notes();
        }
        if (Time.time - time + notesLandingtime >= 149.86 && Time.time - time + notesLandingtime < 149.91)
        {
            NoteSpawned = false;
        }



        if (Time.time - time > musicLength && Time.time - time < musicLength + 0.1)
        {
            GameObject scoreboardObj = GameObject.Find("SCH_ScoreBoard");
            SCH_score scoreScp = scoreboardObj.GetComponent<SCH_score>();
            scoreScp.FindMaxNotesCombo();
            scoreScp.FindMaxStringCombo();
            GameObject mainManagerObj = GameObject.Find("SCH_MainManager");
            SCH_MainManager mainManagerScp = mainManagerObj.GetComponent<SCH_MainManager>();
            mainManagerScp.SCH_numOfNotes = noOfNotesSpawned;
            mainManagerScp.SCH_numOfStrings = noOfStringsSpawned;
            mainManagerScp.CollectScores();
            SceneManager.LoadScene("SCH_GameOverScene");
        }
    }
}

/*Reference:
Edvard Grieg(1875) In the Hall of the Mountain King. Czech National Symphony Orchestra [electronic download] Musopen.
Available through: https://musopen.org/music/777-peer-gynt-suite-no-1-op-46/#google_vignette [Accessed 3 November 2024]
*/