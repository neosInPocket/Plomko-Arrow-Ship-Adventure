
using System.IO;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

public class PlayerSavesLoad
{
    public static int currentPlayerProgress;
    public static int currentShopPoints;
    public static int playerMaxLifes;
    public static int playerMaxSpeedPoints;
    public static int playerMusicEnabled;
    public static int playerSFXEnabled;
    public static int playerCoinSpawnChance;
    public static int isFirstTimePlaying;
    public static float playerMusicVolume;
    public static float playerSfxVolume;
	
    
    public static void Save()
    {
        PlayerPrefs.SetInt("currentShopPoints", currentShopPoints);
        PlayerPrefs.SetInt("currentPlayerProgress", currentPlayerProgress);
        PlayerPrefs.SetInt("playerMaxLifes", playerMaxLifes);
        PlayerPrefs.SetInt("playerMaxSpeedPoints", playerMaxSpeedPoints);
        PlayerPrefs.SetInt("playerCoinSpawnChance", playerCoinSpawnChance);
        PlayerPrefs.SetInt("playerMusicEnabled", playerMusicEnabled);
        PlayerPrefs.SetInt("playerSFXEnabled", playerSFXEnabled);
        PlayerPrefs.SetFloat("playerMusicVolume", playerMusicVolume);
        PlayerPrefs.SetFloat("playerSfxVolume", playerSfxVolume);
        PlayerPrefs.SetInt("isFirstTimePlaying", isFirstTimePlaying);
        
        PlayerPrefs.Save();
    }
	
    public static void Load()
    {
        currentShopPoints = PlayerPrefs.GetInt("currentShopPoints", 100);
        currentPlayerProgress = PlayerPrefs.GetInt("currentPlayerProgress", 1);
        playerMaxLifes = PlayerPrefs.GetInt("playerMaxLifes", 1);
        playerMaxSpeedPoints = PlayerPrefs.GetInt("playerMaxSpeedPoints", 0);
        playerCoinSpawnChance = PlayerPrefs.GetInt("playerMaxSpeedPoints", 0);
        playerMusicEnabled = PlayerPrefs.GetInt("playerMusicEnabled", 1);
        playerSFXEnabled = PlayerPrefs.GetInt("playerSFXEnabled", 1);
        playerMusicVolume = PlayerPrefs.GetFloat("playerMusicVolume", 1f);
        playerSfxVolume = PlayerPrefs.GetFloat("playerSfxVolume", 1f);
        isFirstTimePlaying = PlayerPrefs.GetInt("isFirstTimePlaying", 1);
    }

    public static void ClearData()
    {
        currentPlayerProgress = 1;
        currentShopPoints = 100;
        playerMaxLifes = 1;
        playerMaxSpeedPoints = 0;
        playerCoinSpawnChance = 0;
        playerMusicEnabled = 1;
        playerSFXEnabled = 1;
        playerMusicVolume = 1f;
        playerSfxVolume = 1f;
        isFirstTimePlaying = 1;
        Save();
    }

    public static Vector2 ScreenSize()
    {
        return Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}
