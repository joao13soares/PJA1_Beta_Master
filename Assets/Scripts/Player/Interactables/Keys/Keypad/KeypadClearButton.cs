using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadClearButton : MonoBehaviour, IRaycastResponse
{
    [SerializeField] private KeypadUnlock associatedKeypad;
    public void OnRaycastSelect()
    {
        associatedKeypad.ClearKey();
    }
}
