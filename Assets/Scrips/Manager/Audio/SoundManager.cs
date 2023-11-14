using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundType)
    {
        var audio = Resources.Load<AudioClip>(path: $"Sounds/{soundType.ToString()}");
        audioSource.PlayOneShot(audio);
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
} 
