using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMachine : PlayerSubscriber
{
    [SerializeField] private ArrowMovingHandler arrowMovement;
    [SerializeField] private BarrierSpawner barrierSpawner;
    [SerializeField] private InstructionsWindow instructionsWindow;
    [SerializeField] private DelayScreen delayScreen;
    [SerializeField] private ProgressScreen progressScreen;
    [SerializeField] private Player player;
    [SerializeField] private DeathScreen deathScreen;
    
    private int maxProgression;
    private int maxCoins;
	
    private int MaxProgression => (int)(4 * Mathf.Log(PlayerSavesLoad.currentPlayerProgress) + 4);
    private int MaxCoins => (int)(20 * Mathf.Log(PlayerSavesLoad.currentPlayerProgress) + 20);
    
    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        PlayerSavesLoad.Load();
        maxProgression = MaxProgression;
        maxCoins = MaxCoins;
        
        progressScreen.Restart(PlayerSavesLoad.playerMaxLifes, PlayerSavesLoad.currentPlayerProgress);
        barrierSpawner.ClearContainer();
        arrowMovement.Restart();
        barrierSpawner.Initialize();
        player.Restart();

        int instructions = PlayerSavesLoad.isFirstTimePlaying;

        if (instructions == 1)
        {
            PlayerSavesLoad.isFirstTimePlaying = 0;
            PlayerSavesLoad.Save();
            
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
        delayScreen.Play(PlayerSavesLoad.currentPlayerProgress);
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
            PlayerSavesLoad.currentPlayerProgress++;
            PlayerSavesLoad.currentShopPoints += maxCoins;
            PlayerSavesLoad.Save();
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
