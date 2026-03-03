using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HKN_GameManager : MonoBehaviour
{
    public static int playerHealth = 10;
    public static int MonstersKilled = 0;
    public static int Score = 0;

    InputSubscription GetInput;

    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
    }




    // Start is called before the first frame update
    void Start()
    {
        // make a spawn for the enemy (wed night project)
        playerHealth = 10;
        Score = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
       
    }

    }
