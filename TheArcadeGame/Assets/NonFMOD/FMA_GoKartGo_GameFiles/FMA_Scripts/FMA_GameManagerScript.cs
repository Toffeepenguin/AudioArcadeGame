using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManagerScript : MonoBehaviour
{
    InputSubscription GetInput;


    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
    }

}
