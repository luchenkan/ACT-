using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio
{
    public AudioSource audioSource;

    public Player_Audio(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    /// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
