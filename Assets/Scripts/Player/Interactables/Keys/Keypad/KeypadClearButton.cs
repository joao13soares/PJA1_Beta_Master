using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadClearButton : MonoBehaviour, IInteractable
{
    [SerializeField] private KeypadUnlock associatedKeypad;
    public void OnRaycastSelect()
    {
        associatedKeypad.ClearKey();
    }
}
