using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATA_ObsticleColision : MonoBehaviour
{
    // add sound effects
    public GameObject thePlayer;
    public GameObject levelControl;
    //public GameObject charModel;
    //public AudioSource deathSound;
    //public GameObject mainCamera;

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        thePlayer.GetComponent<ATA_Bounce_n_Go_PlayerMovementScript>().enabled = false;
        levelControl.GetComponent<ATA_Score>().enabled = false;
        //mainCamera.GetComponent<Animator>().enabled = true;
        // add animations and sound
        levelControl.GetComponent<ATA_EndRun>().enabled = true;
    }
}
