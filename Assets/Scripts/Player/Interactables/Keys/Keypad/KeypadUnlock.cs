using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeypadUnlock : MonoBehaviour
{
    [SerializeField] private int[] correctCode;
    public int[] currentCode;

    private int currentIndex;


    [SerializeField] private DragDoor dragDoor;


    private AudioSource beepAudioSource;


    public delegate void CodeChange();

    public event CodeChange CodeChanged;


    private bool CanUnlock()
    {
        for (int i = 0; i < currentCode.Length; i++)
        {
            if (currentCode[i] != correctCode[i]) return false;
        }

        return true;
    }


    public void AddNumber(int numberToAdd)
    {
        if (currentIndex >= correctCode.Length) return;

        currentCode[currentIndex++] = numberToAdd;
        CodeChanged?.Invoke();
    }

    public void ClearKey()
    {
        ClearCode();

        CodeChanged?.Invoke();

        currentIndex = 0;
    }

    public void EnterKey()
    {
        if (CanUnlock()) dragDoor.UnlockDoor();
        else ClearKey();
    }

    private void ClearCode()
    {
        for (int i = 0; i < currentCode.Length; i++)
        {
            currentCode[i] = 0;
        }
    }
}