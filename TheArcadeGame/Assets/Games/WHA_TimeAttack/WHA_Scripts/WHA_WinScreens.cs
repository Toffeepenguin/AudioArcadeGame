using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WHA_WinScreens : MonoBehaviour
{
    private InputSubscription _input;

    private void Start()
    {
        _input = GetComponent<InputSubscription>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.MenuInput)
        {
            SceneManager.LoadScene("WHA_ChristmasTrack");
        }
    }
}
