using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Image slider;
    [SerializeField] private Image imageLeft;
    [SerializeField] private Image imageMiddle;
    [SerializeField] private Image imageRight;

    public void Restart(int lifesAmount, int levelNumber)
    {
        UpdateLifeAmount(lifesAmount);
        UpdateSliderValue(0);
        UpdateLevelText(levelNumber);
    }
    
    public void UpdateLifeAmount(int lifeAmount)
    {
        if (lifeAmount == 1)
        {
            imageLeft.enabled = false;
            imageMiddle.enabled = true;
            imageRight.enabled = false;
        }
        
        if (lifeAmount == 2)
        {
            imageLeft.enabled = true;
            imageMiddle.enabled = true;
            imageRight.enabled = false;
        }
        
        if (lifeAmount == 3)
        {
            imageLeft.enabled = true;
            imageMiddle.enabled = true;
            imageRight.enabled = true;
        }
        
        if (lifeAmount == 0)
        {
            imageLeft.enabled = false;
            imageMiddle.enabled = false;
            imageRight.enabled = false;
        }
    }

    public void UpdateSliderValue(float value)
    {
        slider.fillAmount = value;
    }

    public void UpdateLevelText(int levelNumber)
    {
        if (levelNumber > 9)
        {
            levelText.text = "0" + levelNumber;
            return;
        }

        levelText.text = levelNumber.ToString();
    }
}
