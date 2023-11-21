using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class StoreSlider : MonoBehaviour
{
    [SerializeField] private Image healthIcon;
    [SerializeField] private Image speedIcon;
    [SerializeField] private Image distanceIcon;

    [Header("Distribution parameters")] [SerializeField]
    private float cDistributionConstant;

    [SerializeField] private float minValue;

    [Header("Fade parameters")] 
    [SerializeField] private float fadeSpeed;

    [SerializeField] private ScrollRect scrollRect;


    private void Start()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        
        Touch.onFingerUp += OnFingerUp;
        SetIcons(scrollRect.normalizedPosition);
    }

    private void OnFingerUp(Finger obj)
    {
        CheckCurrentPosition(scrollRect.horizontalNormalizedPosition);
    }

    public void SetIcons(Vector2 sliderValue)
    {
        float currentXPosition = sliderValue.x;
        
        var healthIconScale = Distribution(minValue, currentXPosition, 0);
        var healthColor = healthIcon.color;
        healthColor.a = healthIconScale;
        healthIcon.color = healthColor;
        healthIcon.transform.localScale = new Vector3(healthIconScale, healthIconScale, 1);
        
        var speedIconScale = Distribution(minValue, currentXPosition, 0.5f);
        var speedColor = speedIcon.color;
        speedColor.a = speedIconScale;
        speedIcon.color = speedColor;
        speedIcon.transform.localScale = new Vector3(speedIconScale, speedIconScale, 1);
        
        var distanceIconScale = Distribution(minValue, currentXPosition, 1);
        var distanceColor = distanceIcon.color;
        distanceColor.a = distanceIconScale;
        distanceIcon.color = distanceColor;
        distanceIcon.transform.localScale = new Vector3(distanceIconScale, distanceIconScale, 1);
    }

    private void CheckCurrentPosition(float xPosition)
    {
        if (xPosition < 0.4f)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToDestination(0));
        }

        if (xPosition is >= 0.4f and < 0.6f)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToDestination(0.5f));
        }
        
        if (xPosition >= 0.6f)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToDestination(1));
        }
    }

    private IEnumerator FadeToDestination(float destination)
    {
        var distance = destination - scrollRect.horizontalNormalizedPosition;
        var magnitude = Mathf.Abs(distance);
        int direction = (int)(distance / magnitude);

        if (direction < 0)
        {
            while (scrollRect.horizontalNormalizedPosition > destination)
            {
                magnitude = Mathf.Abs(destination - scrollRect.horizontalNormalizedPosition);
                scrollRect.horizontalNormalizedPosition -= fadeSpeed * (magnitude + 0.01f);
                SetIcons(scrollRect.normalizedPosition);
                yield return new WaitForFixedUpdate();
            }

            scrollRect.horizontalNormalizedPosition = destination;
        }
        else
        {
            while (scrollRect.horizontalNormalizedPosition < destination)
            {
                magnitude = Mathf.Abs(destination - scrollRect.horizontalNormalizedPosition);
                scrollRect.horizontalNormalizedPosition += fadeSpeed * (magnitude + 0.01f);
                SetIcons(scrollRect.normalizedPosition);
                yield return new WaitForFixedUpdate();
            }
            
            scrollRect.horizontalNormalizedPosition = destination;
        }
    }
    
    private float Distribution(float minValue, float x, float xPos)
    {
        return (1 - minValue) * Mathf.Exp(-Mathf.Pow(x - xPos, 2) / Mathf.Pow(cDistributionConstant, 2)) + minValue;
    }

    private void OnDisable()
    {
        Touch.onFingerUp -= OnFingerUp;
    }
}
