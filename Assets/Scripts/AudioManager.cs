using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //public AudioClip[] sounds;
    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.isFacingRight)
        {
            soundEffects[3].panStereo = 0.5f;
        }
        else if (!PlayerController.instance.isFacingRight)
        {
            soundEffects[3].panStereo = -0.5f;
        }
    }

    public void PlaySFX(int sound)
    {
        soundEffects[sound].Stop();
        soundEffects[sound].Play();
    }
    
    
}
