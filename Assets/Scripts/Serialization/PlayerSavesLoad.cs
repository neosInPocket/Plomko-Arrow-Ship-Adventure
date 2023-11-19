using System;
using System.IO;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class PlayerSavesLoad : MonoBehaviour
{
    [SerializeField] private bool isClearData;
    
    private GameData gameData;
    public GameData Data => gameData;

    private void Awake()
    {
        gameData = LoadData();
        
        if (gameData == null)
        {
            SaveDefaultData();
        }

        if (isClearData)
        {
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
        var data = new GameData();

        data.isFirstTimePlaying = true;
        data.playerMaxSpeedPoints = 0;
        data.playerSfxVolume = 1f;
        data.playerLevel = 1;
        data.playerMaxLifes = 1;
        data.playerMusicVolume = 1f;
        data.playerShopPoints = 100;

        SaveData(data);
    }

    public void SaveData(GameData data)
    {
        string jsonData = JsonUtility.ToJson(data, true);
        using (StreamWriter writer =
               new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "saves.json"))
        {
            writer.Write(jsonData);
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("increase level", GUILayout.Height(400), GUILayout.Height(400)))
        {
            Data.playerLevel++;
        }
        
        if (GUILayout.Button("save chahges", GUILayout.Height(400), GUILayout.Height(400)))
        {
            SaveData(gameData);
        }
    }
}
