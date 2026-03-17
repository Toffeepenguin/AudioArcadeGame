using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JRO_Audio : MonoBehaviour
{
    public AudioSource myAudioSource;
    public AudioClip myClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAudioSource.PlayOneShot(myClip);
    }
}
