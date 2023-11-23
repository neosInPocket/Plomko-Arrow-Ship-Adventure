using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMachine : PlayerSubscriber
{
    [SerializeField] private PlayerSavesLoad playerSavesLoad;
    [SerializeField] private ArrowMovingHandler arrowMovement;
    [SerializeField] private BarrierSpawner barrierSpawner;
    [SerializeField] private InstructionsWindow instructionsWindow;
    [SerializeField] private DelayScreen delayScreen;
    [SerializeField] private ProgressScreen progressScreen;
    [SerializeField] private Player player;
    [SerializeField] private DeathScreen deathScreen;
    
    private int maxProgression;
    private int maxCoins;
	
    private int MaxProgression => (int)(4 * Mathf.Log(playerSavesLoad.Data.playerLevel) + 4);
    private int MaxCoins => (int)(20 * Mathf.Log(playerSavesLoad.Data.playerLevel) + 20);
    
    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        maxProgression = MaxProgression;
        maxCoins = MaxCoins;
        
        progressScreen.Restart(playerSavesLoad.Data.playerMaxLifes, playerSavesLoad.Data.playerLevel);
        barrierSpawner.ClearContainer();
        arrowMovement.Restart();
        barrierSpawner.Initialize();
        player.Restart();

        bool instructions = playerSavesLoad.Data.isFirstTimePlaying;

        if (instructions)
        {
            playerSavesLoad.Data.isFirstTimePlaying = false;
            playerSavesLoad.SaveData();
            
            instructionsWindow.InstructionsEnded += InstructionsEnded;
            instructionsWindow.Play();
        }
        else
        {
            PlayDelayScreen();
        }
    }

    private void InstructionsEnded()
    {
        instructionsWindow.InstructionsEnded -= InstructionsEnded;
        PlayDelayScreen();
    }

    private void PlayDelayScreen()
    {
        delayScreen.DelayEnded += DelayEnded;
        delayScreen.Play(playerSavesLoad.Data.playerLevel);
    }
    
    private void DelayEnded()
    {
        delayScreen.DelayEnded -= DelayEnded;
        arrowMovement.StartMove();
        barrierSpawner.Enable();
    }

    public override void OnPlayerGold(int allGold)
    {
        if (allGold >= maxProgression)
        {
            arrowMovement.StopMove();
            barrierSpawner.Disable();
            deathScreen.ShowDeathScreen(false, maxCoins);
            playerSavesLoad.Data.playerLevel++;
            playerSavesLoad.Data.playerShopPoints += maxCoins;
            playerSavesLoad.SaveData();
        }
        
        progressScreen.UpdateSliderValue((float)allGold / (float)maxProgression);
    }

    public override void OnPlayerDamaged(int lifeCount)
    {
        if (lifeCount <= 0)
        {
            barrierSpawner.Disable();
            arrowMovement.StopMove();
            deathScreen.ShowDeathScreen(true, 0);
        }
        
        progressScreen.UpdateLifeAmount(lifeCount);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
