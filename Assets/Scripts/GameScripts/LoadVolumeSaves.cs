using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVolumeSaves : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private PlayerSavesLoad playerSavesLoad;
    
    private void Start()
    {
        music.enabled = playerSavesLoad.Data.playerMusicEnabled;
        music.volume = playerSavesLoad.Data.playerMusicVolume;
    }
}
