using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(MusicType musicType)
    {
        var audio = Resources.Load<AudioClip>(path: $"Music/{musicType.ToString()}");
        audioSource.PlayOneShot(audio);
    }
    public void StopMusic()
    {
        audioSource.Stop();
    }
} 
