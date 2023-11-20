using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSlider : MonoBehaviour
{
    [SerializeField] private Transform healthIcon;
    [SerializeField] private Transform speedIcon;
    [SerializeField] private Transform distanceIcon;

    [Header("Parameters")] 
    [SerializeField] private float minIconSize;
    [SerializeField] private float minIconAlpha;
    
    public void SetIcons()
    {
        
    }
}
