using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private PlayerSavesLoad saves;
    
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    [SerializeField] private Image musicCheckmark;
    [SerializeField] private Image sfxCheckmark;

    private void Start()
    {
        SetSliderValue(musicSlider, saves.Data.playerMusicVolume);
        SetSliderValue(sfxSlider, saves.Data.playerSfxVolume);

        music.enabled = saves.Data.playerMusicEnabled;
        music.volume = saves.Data.playerMusicVolume;
        musicCheckmark.enabled = saves.Data.playerMusicEnabled;
        sfxCheckmark.enabled = saves.Data.playerSFXEnabled;
    }

    private void SetSliderValue(Slider slider, float value)
    {
        slider.value = value;
    }

    public void ChangeMusicVolume(float volume)
    {
        music.volume = volume;
        saves.Data.playerMusicVolume = volume;
        saves.SaveData();
    }
    
    public void ChangeSFXVolume(float volume)
    {
        saves.Data.playerSfxVolume = volume;
        saves.SaveData();
    }

    public void ToggleMusicVolume()
    {
        bool value = !musicCheckmark.enabled;
        musicCheckmark.enabled = value;

        if (value)
        {
            music.enabled = true;
            saves.Data.playerMusicEnabled = true;
        }
        else
        {
            music.enabled = false;
            saves.Data.playerMusicEnabled = false;
        }
        
        saves.SaveData();
    }
    
    public void ToggleSFXVolume()
    {
        bool value = !sfxCheckmark.enabled;
        sfxCheckmark.enabled = value;

        if (value)
        {
            saves.Data.playerSFXEnabled = true;
        }
        else
        {
            saves.Data.playerSFXEnabled = false;
        }
        
        saves.SaveData();
    }
}
