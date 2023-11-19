using System;
using Unity.VisualScripting;
using UnityEditor;

[Serializable]
public class GameData
{
    public int playerLevel;
    public int playerShopPoints;
    public int playerMaxLifes;
    public int playerMaxSpeedPoints;
    public bool isFirstTimePlaying;

    public float playerMusicVolume;
    public float playerSfxVolume;
}