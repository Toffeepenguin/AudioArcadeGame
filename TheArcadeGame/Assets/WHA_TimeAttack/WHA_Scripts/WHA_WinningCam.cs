using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;
using UnityEngine.Windows;

public class WHA_WinningCam : MonoBehaviour
{
    [Header("Setup Stuff")]
    private GameObject gameMan;
    private WHA_RaceManager _raceManager;

    [Header("Cinemachine")]
    //private CinemachineFreeLook freelookCam;
    //private CinemachineCollider cinemachineCol;
    public float midHeight;
    public float midRadius;

    // Start is called before the first frame update
    void Start()
    {
        gameMan = GameObject.FindGameObjectWithTag("GameController");
        _raceManager = gameMan.GetComponent<WHA_RaceManager>();

        //freelookCam = this.gameObject.GetComponent<CinemachineFreeLook>();
        //cinemachineCol = this.gameObject.GetComponent<CinemachineCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_raceManager.hasPlayerFinished)
        {
            //cinemachineCol.enabled = false;
            //freelookCam.m_Orbits[1].m_Height = midHeight;
            //freelookCam.m_Orbits[1].m_Radius = midRadius;
        }
    }
}
