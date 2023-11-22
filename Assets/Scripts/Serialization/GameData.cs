using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameData
{
    public int playerLevel;
    public int playerShopPoints;
    public int playerMaxLifes;
    public int playerMaxSpeedPoints;
    public int playerMaxDistancePoints;
    public bool isFirstTimePlaying;

    public float playerMusicVolume;
    public float playerSfxVolume;
    public bool playerMusicEnabled;
    public bool playerSFXEnabled;

    public Vector2 screenSize;
}
