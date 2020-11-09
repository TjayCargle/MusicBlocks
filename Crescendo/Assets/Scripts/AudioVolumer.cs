using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumer : MonoBehaviour
{
    AudioSource myAudio;
    private float trueVolume = 0.0f;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        if (myAudio != null)
        {
            trueVolume = OptionsManager.musicVolume / 100.0f;
            if (myAudio.volume != trueVolume)
                myAudio.volume = trueVolume;
        }
    }
    private void Awake()
    {
        if (myAudio != null)
        {
            trueVolume = OptionsManager.musicVolume / 100.0f;
            if (myAudio.volume != trueVolume)
                myAudio.volume = trueVolume;
        }
    }
    private void Update()
    {
        if(myAudio != null)
        {
            trueVolume = OptionsManager.musicVolume / 100.0f;
            if (myAudio.volume != trueVolume)
                myAudio.volume = trueVolume;
        }
    }
}
