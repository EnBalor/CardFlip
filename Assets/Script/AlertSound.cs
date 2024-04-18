using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSound : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip alertSound;

    private bool audioPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.alertSound;
    }

    private void Update()
    {
        if (GameManager.instance.time <= 10.0f)
        {
            StartCoroutine(PlayAudio());
        }
    }

    private IEnumerator PlayAudio()
    {
        if(!audioPlayed)
        {
            audioSource.Play();
            audioPlayed = true;
        }
        yield return null;
    }
}
