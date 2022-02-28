using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMute : MonoBehaviour
{
    public AudioSource[] soundEffects;

    private void OnEnable()
    {
        foreach (AudioSource sounds in soundEffects)
            sounds.mute = true;
    }

    private void OnDisable()
    {
        foreach (AudioSource sounds in soundEffects)
            sounds.mute = false;
    }
}
