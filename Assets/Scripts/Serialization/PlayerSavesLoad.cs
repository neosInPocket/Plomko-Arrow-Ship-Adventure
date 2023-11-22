using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerSavesLoad : MonoBehaviour
{
    [SerializeField] private bool isCaptureScreenSize;
    [SerializeField] private bool isClearData;
    
    private GameData gameData;
    public GameData Data => gameData;

    private void Awake()
    {
        gameData = LoadData();

        if (isCaptureScreenSize)
        {
            gameData.screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            SaveData();
        }
        
        if (gameData == null)
        {
            SaveDefaultData();
        }

        if (isClearData)
        {
            Debug.LogWarning("Data is cleared on awake!");
            SaveDefaultData();
        }
    }
    
    private GameData LoadData()
    {
        string jsonData = string.Empty;
        
        using (StreamReader reader =
               new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "saves.json"))
        {
            jsonData = reader.ReadToEnd();
        }
        
        var data = JsonUtility.FromJson<GameData>(jsonData);
        return data;
    }

    private void SaveDefaultData()
    {
        gameData = new GameData();

        gameData.isFirstTimePlaying = true;
        
        gameData.playerMaxSpeedPoints = 0;
        gameData.playerMaxLifes = 1;
        gameData.playerMaxDistancePoints = 0;
        
        gameData.playerSfxVolume = 1f;
        gameData.playerMusicVolume = 1f;

        gameData.playerLevel = 1;
        gameData.playerShopPoints = 100;

        gameData.playerMusicEnabled = true;
        gameData.playerSFXEnabled = true;
        
        SaveData();
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(gameData, true);
        using (StreamWriter writer =
               new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "saves.json"))
        {
            writer.Write(jsonData);
        }
    }
}
