using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InstructionsWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text instructions;
    
    public Action InstructionsEnded;
    private Action<Finger> nextInstruction;
    
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();

        nextInstruction = Instruction1;
        instructions.text = "WELCOME TO PINKO!";
        Touch.onFingerDown += NextInstruction;
    }

    public void Play()
    {
        gameObject.SetActive(true);
    }

    private void NextInstruction(Finger finger)
    {
        nextInstruction(finger);
    }

    private void Instruction1(Finger finger)
    {
        nextInstruction = Instruction2;
        instructions.text = "Don't let your arrow hit the spinning spikes!";
    }
    
    private void Instruction2(Finger finger)
    {
        nextInstruction = Instruction3;
        instructions.text = "To do this, switch all visible objects by tapping the screen";
    }
    
    private void Instruction3(Finger finger)
    {
        nextInstruction = Instruction4;
        instructions.text = "Note that some objects appear already turned off";
    }
    
    private void Instruction4(Finger finger)
    {
        nextInstruction = Instruction5;
        instructions.text = "Complete levels, get coins for them and buy various upgrades in the store!";
    }
    
    private void Instruction5(Finger finger)
    {
        nextInstruction = EndInstructions;
        instructions.text = "Good luck!";
    }
    
    private void EndInstructions(Finger finger)
    {
        Touch.onFingerDown -= NextInstruction;
        InstructionsEnded?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Touch.onFingerDown -= NextInstruction;
    }

    private void OnDestroy()
    {
        Touch.onFingerDown -= NextInstruction;
    }
}