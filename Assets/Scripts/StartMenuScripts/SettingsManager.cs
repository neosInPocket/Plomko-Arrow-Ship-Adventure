using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    [SerializeField] private Image musicCheckmark;
    [SerializeField] private Image sfxCheckmark;

    private void Start()
    {
        PlayerSavesLoad.Load();
        SetSliderValue(musicSlider, PlayerSavesLoad.playerMusicVolume);
        SetSliderValue(sfxSlider, PlayerSavesLoad.playerSfxVolume);

        var musicEnabled = PlayerSavesLoad.playerMusicEnabled;
        if (musicEnabled == 1)
        {
            music.enabled = true;
            musicCheckmark.enabled = true;
        }
        else
        {
            music.enabled = false;
            musicCheckmark.enabled = false;
        }
        
        music.volume = PlayerSavesLoad.playerMusicVolume;
        
        var sfxEnabled = PlayerSavesLoad.playerSFXEnabled;
        if (sfxEnabled == 1)
        {
            sfxCheckmark.enabled = true;
        }
        else
        {
            sfxCheckmark.enabled = false;
        }
        
        
    }

    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = value;
    }

    public void ChangeMusicVolume(float volume)
    {
        music.volume = volume;
        PlayerSavesLoad.playerMusicVolume = volume;
        PlayerSavesLoad.Save();
    }
    
    public void ChangeSFXVolume(float volume)
    {
        PlayerSavesLoad.playerSfxVolume = volume;
        PlayerSavesLoad.Save();
    }

    public void ToggleMusicVolume()
    {
        bool value = !musicCheckmark.enabled;
        musicCheckmark.enabled = value;

        if (value)
        {
            music.enabled = true;
            PlayerSavesLoad.playerMusicEnabled = 1;
        }
        else
        {
            music.enabled = false;
            PlayerSavesLoad.playerMusicEnabled = 0;
        }
        
        PlayerSavesLoad.Save();
    }
    
    public void ToggleSFXVolume()
    {
        bool value = !sfxCheckmark.enabled;
        sfxCheckmark.enabled = value;

        if (value)
        {
            PlayerSavesLoad.playerSFXEnabled = 1;
        }
        else
        {
            PlayerSavesLoad.playerSFXEnabled = 0;
        }
        
        PlayerSavesLoad.Save();
    }
}
