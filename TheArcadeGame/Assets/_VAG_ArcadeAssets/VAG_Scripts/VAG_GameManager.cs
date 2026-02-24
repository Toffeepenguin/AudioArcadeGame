using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VAG_GameManager : MonoBehaviour
{
    InputSubscription GetInput;


    



    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();



    }

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }



    private void Update()
    {
       

    }


    public void PlayButton(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }





}
