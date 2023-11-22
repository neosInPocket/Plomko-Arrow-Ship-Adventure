using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Barrier : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float inactiveAlpha; 
    private int rotationDirection;
    private bool isActive;
    
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            rotationDirection = 1;
            SetActiveState(false);
        }
        else
        {
            rotationDirection = -1;
            SetActiveState(true);
        }
    }
    
    private void FixedUpdate()
    {
        var angles = transform.rotation.eulerAngles;
        angles.z += rotationDirection * rotationSpeed;
    }

    public void ToggleActiveState()
    {
        SetActiveState(!isActive);
    }

    private void SetActiveState(bool value)
    {
        var color = spriteRenderer.color;
        if (value)
        {
            color.a = 1;
            isActive = true;
        }
        else
        {
            color.a = inactiveAlpha;
            isActive = false;
        }

        spriteRenderer.color = color;
    }
}
