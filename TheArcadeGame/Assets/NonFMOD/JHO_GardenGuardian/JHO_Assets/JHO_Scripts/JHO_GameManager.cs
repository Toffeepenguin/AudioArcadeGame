using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JHO_GameManager : MonoBehaviour
{

    InputSubscription JHO_input;

    public Animator JHO_anima;

    [SerializeField] public JHO_Prefab JHO_G_Items;
    
    [SerializeField] public JHO_Prefab JHO_B_Items;

    private float JHO_G_timer;
    private float JHO_B_timer;

    private float baseSpawnTime = 3f;
    private float minSpawnTime = 1f;

    private int JHO_maxItemsToSpawn = 4;
    public int JHO_Numberofitems = 2;
    public int JHO_G_MaxNumitems = 0;
    public int JHO_B_MaxNumitems = 0;

    private void Awake()
    {
        JHO_input = GetComponent<InputSubscription>();

    }

    void Update()
    {
        //////////////added by Izzy///////////////////
        if (JHO_input.MenuInput)
        {
            SceneManager.LoadScene(0);
        }
        /////////////////////////////////////////////
        float spawnInterval = Mathf.Max(baseSpawnTime - JHO_ScoreManager.Instance.GetScore() * 0.1f, minSpawnTime);

        JHO_Numberofitems = Mathf.Min(JHO_maxItemsToSpawn, 2 + JHO_ScoreManager.Instance.GetScore() / 10);

        JHO_G_timer += Time.deltaTime;

        if (JHO_G_timer > spawnInterval)
        {
            JHO_G_timer = 0;

            if (JHO_G_MaxNumitems < JHO_maxItemsToSpawn)
            {
                for (int i = 0; i < JHO_Numberofitems; i++)
                {
                    G_Spawning(i);
                    JHO_G_MaxNumitems++;
                    //Debug.Log("Good Item Spawned");
                }
            }

        }

        JHO_B_timer += Time.deltaTime;

        if (JHO_B_timer > spawnInterval)
        {
            JHO_B_timer = 0;

            // Only spawn if we have less than 4 bad items on screen
            if (JHO_B_MaxNumitems < JHO_maxItemsToSpawn)
            {
                for (int i = 0; i < JHO_Numberofitems; i++)
                {
                    B_Spawning(i);
                    JHO_B_MaxNumitems++;

                }
            }
        }
    }

    void G_Spawning(int index)
    {

        Vector2 SpawnPosition = new Vector2(Random.Range(-10f, 10f), 7f);
        SpawnPosition.x += index * 1.5f;

        JHO_Prefab JHO_Prefab = Instantiate(JHO_G_Items, SpawnPosition, Quaternion.identity);

        JHO_Prefab.JHO_Spawner = this;

        JHO_Prefab.JHO_IsGoodItem = true;

        JHO_Prefab.SetFallSpeed(2f, 5f);
    }

    void B_Spawning(int index)
    {
        Vector2 SpawnPosition = new Vector2(Random.Range(-10f, 10f), 7f);
        SpawnPosition.x += index * 1.5f;

        JHO_Prefab JHO_Prefab = Instantiate(JHO_B_Items, SpawnPosition, Quaternion.identity);

        JHO_Prefab.JHO_Spawner= this;

        JHO_Prefab.JHO_IsGoodItem = false;

        JHO_Prefab.SetFallSpeed(3f, 6f);
    }

    public void DecrementItemCount(bool isGoodItem)
    {
        if (isGoodItem)
        {
            JHO_G_MaxNumitems--;
        }
        else
        {
            JHO_B_MaxNumitems--;
        }
    }
}
