using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource musicSource;
    public AudioClip[] musicClips;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Stage"))
        {
            musicSource = GetComponent<AudioSource>();
            musicSource.clip = musicClips[1];
            musicSource.Play();
        }
    }

    
}
