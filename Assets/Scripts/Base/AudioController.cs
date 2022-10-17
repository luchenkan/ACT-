using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController
{
    public AudioSource audioSource;

    public AudioController(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    /// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    public void PlayOneShot(AudioClip audioClip)
    {
        if (audioClip == null)
            return;

        audioSource.PlayOneShot(audioClip);
    }
}
