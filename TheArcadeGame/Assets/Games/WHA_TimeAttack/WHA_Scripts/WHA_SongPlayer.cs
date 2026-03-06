using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHA_SongPlayer : MonoBehaviour
{
    // Array to store audio clips
    public AudioClip[] songs;
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Ensure AudioSource exists
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing!");
            return;
        }

        // Play a random song at the start
        PlayRandomSong();
    }

    public void PlayRandomSong()
    {
        if (songs.Length == 0)
        {
            Debug.LogWarning("No songs available in the array!");
            return;
        }

        // Pick a random song
        int randomIndex = Random.Range(0, songs.Length);
        AudioClip randomSong = songs[randomIndex];

        // Play the selected song
        audioSource.clip = randomSong;
        audioSource.Play();
    }
}
