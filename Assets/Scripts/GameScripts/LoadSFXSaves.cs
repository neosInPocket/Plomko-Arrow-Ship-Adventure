using System;
using Unity.VisualScripting;
using UnityEngine;

public class LoadSFXSaves : MonoBehaviour
{
    [SerializeField] private AudioSource sfx;
    
    private void Start()
    {
        PlayerSavesLoad playerSavesLoad = GameObject.FindObjectOfType<PlayerSavesLoad>();
        sfx.volume = playerSavesLoad.Data.playerSfxVolume;
        sfx.enabled = playerSavesLoad.Data.playerSFXEnabled;
    }
}
