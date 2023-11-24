using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVolumeSaves : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    
    private void Start()
    {
        PlayerSavesLoad.Load();

        var musicEnabled = PlayerSavesLoad.playerMusicEnabled;
        if (musicEnabled == 1)
        {
            music.enabled = true;
        }
        else
        {
            music.enabled = false;
        }
        
        music.volume = PlayerSavesLoad.playerMusicVolume;
    }
}
