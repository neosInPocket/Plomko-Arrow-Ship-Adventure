using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainGameMachine : MonoBehaviour
{
    [SerializeField] private ArrowMovingHandler arrowMovement;
    [SerializeField] private BarrierSpawner barrierSpawner;

    private void Start()
    {
        arrowMovement.StartMove();
        barrierSpawner.Initialize();
    }
}
