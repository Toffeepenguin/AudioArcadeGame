using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ----------")]
    [SerializeField] AudioSource musicSource; 
    [SerializeField] AudioSource SFXSource; 

    [Header("---- Audio Clip -")]
    public AudioClip background; 
    public AudioClip shoot; 
    public AudioClip collectables;

    private void Start()
    {
        if (musicSource != null) 
        {
            musicSource.clip = background; 
            musicSource.loop = true; 
            musicSource.Play(); 
        }
    }

    
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (SFXSource != null)
        {
            SFXSource.volume = volume; 
            SFXSource.PlayOneShot(clip);
        }
    }
}
