using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBR_BackgroundMusic : MonoBehaviour
{
    private VBR_PauseMenu vBR_PauseMenu;
    private VBR_UiManager vBR_UiManager;

    [Header("Sound Effects Parameters")]
    [SerializeField] private AudioClip BGM;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        vBR_PauseMenu = GameObject.Find("GameManager").GetComponent<VBR_PauseMenu>();
        vBR_UiManager = GameObject.Find("Ui").GetComponent<VBR_UiManager>();
        audioSource.PlayOneShot(BGM, 1f);
    }
    void Update()
    {
        if (vBR_UiManager.healthAmount <= 0f)
        {
            audioSource.mute = true;
        }
        else 
        {
            switch (vBR_PauseMenu.isPaused)
            {
                case true:
                    {
                        audioSource.volume = 0.025f;
                        break;
                    }
                case false: 
                    {
                        audioSource.volume = 0.05f;
                        break;
                    }
            } 
        }
    }
}
