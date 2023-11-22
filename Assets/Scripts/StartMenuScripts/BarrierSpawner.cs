using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private PlayerSavesLoad playerSavesLoad;
    [SerializeField] private ArrowMovingHandler arrowMovement;
    [SerializeField] private Barrier[] initialBarriers;
    [SerializeField] private float spawnDelta;

    public void Initialize()
    {
        var screenSize = playerSavesLoad.Data.screenSize;
        screenSize.x -= arrowMovement.ColliderRadius; 
        
        var playerSpeedY = arrowMovement.ArrowSpeed * Mathf.Abs(Mathf.Cos(arrowMovement.ArrowAngle * Mathf.Deg2Rad));
        var playerSpeedX = arrowMovement.ArrowSpeed * Mathf.Abs(Mathf.Sin(arrowMovement.ArrowAngle * Mathf.Deg2Rad));
        var moveTime = screenSize.x * 2 / playerSpeedX;
        var period = playerSpeedY * moveTime;

        var currentSpawnDelta = arrowMovement.transform.position.y + spawnDelta;
        
        foreach (var barrier in initialBarriers)
        {
            var position = new Vector2(
                screenSize.x * 2 * Triangular(currentSpawnDelta, period / 4, period) - screenSize.x,
                currentSpawnDelta
                );
            
            barrier.transform.position = position;
            currentSpawnDelta += spawnDelta;
        }
    }
    
    private float Triangular(float y, float shift, float period)
    {
        return 2 * Mathf.Abs( (y + shift) / period - Mathf.Floor( (y + shift) / period + 0.5f ) );
    }
}
