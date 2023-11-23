using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BarrierSpawner : MonoBehaviour
{
    [SerializeField] private Barrier barrierPrefab;
    [SerializeField] private GoldCoin coinPrefab;
    [SerializeField] private PlayerSavesLoad playerSavesLoad;
    [SerializeField] private ArrowMovingHandler arrowMovement;
    [SerializeField] private Barrier[] initialBarriers;
    [SerializeField] private float spawnDelta;
    [SerializeField] private float initialSpawnDelta;

    private float[] barrierSpawnChances = { 0.7f, 0.6f, 0.5f, 0.4f };
    private float barrierSpawnChance;
    private Spawnable lastBarrier;
    private float playerSpeedX;
    private float playerSpeedY;
    private float moveTime;
    private float period;
    private Vector2 screenSize;
    private bool isTouchEnabled;
    private List<Spawnable> spawnables;
    
    private void Start()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        Touch.onFingerDown += OnFingerDown;
    }

    private void OnFingerDown(Finger obj)
    {
        if (!isTouchEnabled) return;
        foreach (var spawnable in spawnables)
        {
            if (spawnable == null) continue;
            spawnable.ToggleActiveState();
        }
    }

    private void FillInitialSpawnables()
    {
        spawnables.AddRange(initialBarriers);
    }

    public void Enable()
    {
        isTouchEnabled = true;
    }

    public void Disable()
    {
        isTouchEnabled = false;
    }

    private void Update()
    {
        if (lastBarrier == null) return;

        Spawnable obj = null;
        if (Random.Range(0, 1f) < barrierSpawnChance)
        {
            obj = barrierPrefab;
        }
        else
        {
            obj = coinPrefab;
        }
        
        if (arrowMovement.transform.position.y + spawnDelta * initialBarriers.Length > lastBarrier.transform.position.y)
        {
            var y = Random.Range(lastBarrier.transform.position.y + spawnDelta, lastBarrier.transform.position.y + 1.5f * spawnDelta);
            
            var position = new Vector2(
                Random.Range(-screenSize.x + 0.1f, screenSize.x - 0.1f), 
                y
                );

            var barrier = Instantiate(obj, position, Quaternion.identity, transform);
            spawnables.Add(barrier);
            lastBarrier = barrier;
        }
    }

    public void Initialize()
    {
        spawnables = new List<Spawnable>();
        FillInitialSpawnables();
        
        barrierSpawnChance = barrierSpawnChances[playerSavesLoad.Data.playerCoinSpawnChance];
        screenSize = playerSavesLoad.Data.screenSize;
        lastBarrier = initialBarriers[initialBarriers.Length - 1];
        var currentSpawnDelta = arrowMovement.transform.position.y + initialSpawnDelta;
        
        foreach (var barrier in initialBarriers)
        {
            var position = new Vector2(Random.Range(-screenSize.x + 0.1f, screenSize.x - 0.1f), currentSpawnDelta);
            
            barrier.transform.position = position;
            currentSpawnDelta += spawnDelta;
        }
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= OnFingerDown;
    }

    public void ClearContainer()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
