using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadEnterButton : MonoBehaviour,IRaycastResponse
{
    [SerializeField] private KeypadUnlock associatedKeypad;
    public void OnRaycastSelect()
    {
        associatedKeypad.EnterKey();
    }
}
