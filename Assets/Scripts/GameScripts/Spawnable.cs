using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    [SerializeField] private bool isInitial;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] private float inactiveAlpha;
    private bool isActive;
    public bool IsActive => isActive;
    public bool IsInitial => isInitial;
    
    private void Start()
    {
        Initialize();
        CustomInitialize();
    }

    private void Initialize()
    {
        var rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            SetActiveState(false);
        }
        else
        {
            SetActiveState(true);
        }
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

        CustomActiveState(value);
        spriteRenderer.color = color;
    }

    protected virtual void CustomInitialize()
    {
        
    }
    
    protected virtual void CustomActiveState(bool state)
    {
        
    }
}
