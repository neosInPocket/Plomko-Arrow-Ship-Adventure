using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DelayScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text delayText;
    private Action nextCaption;
    public Action DelayEnded;
    
    
    public void Play(int levelNumber)
    {
        delayText.text = "LEVEL " + levelNumber;
        delayText.gameObject.SetActive(true);
        nextCaption = Caption3;
    }

    public void RaiseCaption()
    {
        nextCaption();
    }

    private void Caption3()
    {
        delayText.gameObject.SetActive(false);
        delayText.text = "3";
        nextCaption = Caption2;
        delayText.gameObject.SetActive(true);
    }
    
    private void Caption2()
    {
        delayText.gameObject.SetActive(false);
        delayText.text = "2";
        nextCaption = Caption1;
        delayText.gameObject.SetActive(true);
    }
    
    private void Caption1()
    {
        delayText.gameObject.SetActive(false);
        delayText.text = "1";
        nextCaption = CaptionGO;
        delayText.gameObject.SetActive(true);
    }
    
    private void CaptionGO()
    {
        delayText.gameObject.SetActive(false);
        delayText.text = "GO";
        nextCaption = RaiseEndEvent;
        delayText.gameObject.SetActive(true);
    }

    private void RaiseEndEvent()
    {
        DelayEnded?.Invoke();
        gameObject.SetActive(false);
    }
}
