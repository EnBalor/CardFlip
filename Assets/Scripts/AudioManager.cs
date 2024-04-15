using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundSound;

    void Start()
    {
        audioSource.clip = backgroundSound;
        audioSource.Play();
    }

    void Update()
    {
        
    }
}
