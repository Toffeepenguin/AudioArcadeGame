using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WHA_StartMenu : MonoBehaviour
{
    [Header("Setup Stuff")]
    private InputSubscription _input;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _input = GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.SpaceInput)
        {
            SceneManager.LoadScene("WHA_ChristmasTrack");
        }
    }
}
