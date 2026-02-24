using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QME_SoundManager : MonoBehaviour
{
    // Define a public static instance of QME_SoundManager to allow global access
    public static QME_SoundManager instance { get; private set; }

    // Declare a private variable to hold the AudioSource component
    private AudioSource source;

    private void Awake()
    {
        // Initialize the instance of QME_SoundManager to this instance of the class
        instance = this;

        // Get the AudioSource component attached to the GameObject and assign it to the source variable
        source = GetComponent<AudioSource>();
    }

    // Method to play a sound using the AudioSource component
    public void PlaySound(AudioClip _sound)
    {
        // Play the provided AudioClip once using the PlayOneShot method
        source.PlayOneShot(_sound);
    }
}
