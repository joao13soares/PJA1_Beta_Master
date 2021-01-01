using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadEnterButton : MonoBehaviour,IInteractable
{
    [SerializeField] private KeypadUnlock associatedKeypad;
    public void OnRaycastSelect()
    {
        associatedKeypad.EnterKey();
    }
}
