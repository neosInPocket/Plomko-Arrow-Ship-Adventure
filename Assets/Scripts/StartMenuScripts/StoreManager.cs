using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button healthButton;
    [SerializeField] private Button speedButton;
    [SerializeField] private Button distanceButton;

    [Header("Main")] 
    [SerializeField] private TMP_Text playerCoinsAmount;

    [Header("Points")] 
    [SerializeField] private Image[] healthPoints;
    [SerializeField] private Image[] speedPoints;
    [SerializeField] private Image[] distancePoints;
    
    private void Start()
    {
        PlayerSavesLoad.Load();
        
        LoadShop();
    }

    public void PurchaseHealth()
    {
        PlayerSavesLoad.currentShopPoints -= 100;
        PlayerSavesLoad.playerMaxLifes++;
        PlayerSavesLoad.Save();
        LoadShop();
    }
    
    public void PurchaseSpeed()
    {
        PlayerSavesLoad.currentShopPoints -= 50;
        PlayerSavesLoad.playerMaxSpeedPoints++;
        PlayerSavesLoad.Save();
        LoadShop();
    }
    
    public void PurchaseDistance()
    {
        PlayerSavesLoad.currentShopPoints -= 50;
        PlayerSavesLoad.playerCoinSpawnChance++;
        PlayerSavesLoad.Save();
        LoadShop();
    }
    
    private void LoadShop()
    {
        EnableButton(healthButton, 3, PlayerSavesLoad.playerMaxLifes, 100);
        EnableButton(speedButton, 3, PlayerSavesLoad.playerMaxSpeedPoints, 50);
        EnableButton(distanceButton, 3, PlayerSavesLoad.playerCoinSpawnChance, 50);
        
        RefreshPoints(healthPoints, PlayerSavesLoad.playerMaxLifes);
        RefreshPoints(speedPoints, PlayerSavesLoad.playerMaxSpeedPoints);
        RefreshPoints(distancePoints, PlayerSavesLoad.playerCoinSpawnChance);

        SetCoinsText();
    }

    private void EnableButton(Button button, float maxUpgradeValue, float currentUpgradeValue, int cost)
    {
        bool isEnabled = currentUpgradeValue < maxUpgradeValue && PlayerSavesLoad.currentShopPoints - cost >= 0;
        button.interactable = isEnabled;
    }

    private void RefreshPoints(Image[] images, int points)
    {
        foreach (var image in images)
        {
            image.enabled = false;
        }

        for (int i = 0; i < points; i++)
        {
            images[i].enabled = true;
        }
    }

    private void SetCoinsText()
    {
        playerCoinsAmount.text = PlayerSavesLoad.currentShopPoints.ToString();
    }
}
