using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SWA_AudioManager : MonoBehaviour
{

    [SerializeField] SWA_Player player;
    [SerializeField] AudioSource audioSource;

    private bool stopAudio = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SWA_Player>();
        audioSource = GetComponent<AudioSource>();

        // double check if player is in scene.
        if (player == null)
        {
            Debug.LogError("Not find in scene.");
        }    

        //if (player != null)
        //{
        //    audioSource.Play();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.isAlive == false && !stopAudio)
        {
            stopAudio = true;
            audioSource.Pause();
        }
    }
}
