using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaves : MonoBehaviour
{
    [SerializeField] private bool clearSaves;

    private void Awake()
    {
        if (clearSaves)
        {
            PlayerSavesLoad.ClearData();
        }
    }
}
