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

    [SerializeField] private PlayerSavesLoad playerSavesLoad;

    [Header("Points")] 
    [SerializeField] private Image[] healthPoints;
    [SerializeField] private Image[] speedPoints;
    [SerializeField] private Image[] distancePoints;
    
    private void Start()
    {
        LoadShop();
    }

    public void PurchaseHealth()
    {
        playerSavesLoad.Data.playerShopPoints -= 100;
        playerSavesLoad.Data.playerMaxLifes++;
        playerSavesLoad.SaveData();
        LoadShop();
    }
    
    public void PurchaseSpeed()
    {
        playerSavesLoad.Data.playerShopPoints -= 50;
        playerSavesLoad.Data.playerMaxSpeedPoints++;
        playerSavesLoad.SaveData();
        LoadShop();
    }
    
    public void PurchaseDistance()
    {
        playerSavesLoad.Data.playerShopPoints -= 50;
        playerSavesLoad.Data.playerMaxDistancePoints++;
        playerSavesLoad.SaveData();
        LoadShop();
    }
    
    private void LoadShop()
    {
        EnableButton(healthButton, 3, playerSavesLoad.Data.playerMaxLifes, 100);
        EnableButton(speedButton, 3, playerSavesLoad.Data.playerMaxSpeedPoints, 50);
        EnableButton(distanceButton, 3, playerSavesLoad.Data.playerMaxDistancePoints, 50);
        
        RefreshPoints(healthPoints, playerSavesLoad.Data.playerMaxLifes);
        RefreshPoints(speedPoints, playerSavesLoad.Data.playerMaxSpeedPoints);
        RefreshPoints(distancePoints, playerSavesLoad.Data.playerMaxDistancePoints);

        SetCoinsText();
    }

    private void EnableButton(Button button, float maxUpgradeValue, float currentUpgradeValue, int cost)
    {
        bool isEnabled = currentUpgradeValue < maxUpgradeValue && playerSavesLoad.Data.playerShopPoints - cost >= 0;
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
        playerCoinsAmount.text = playerSavesLoad.Data.playerShopPoints.ToString();
    }
}
