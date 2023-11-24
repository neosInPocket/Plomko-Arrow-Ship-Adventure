using System;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSFXSaves : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;
    
    private void Start()
    {
        PlayerSavesLoad.Load();
        sfx.volume = PlayerSavesLoad.playerSfxVolume;

        var sfxEnabled = PlayerSavesLoad.playerSFXEnabled;

        if (sfxEnabled == 1)
        {
            sfx.enabled = true;
        }
        else
        {
            sfx.enabled = false;
        }
        
    }
}
