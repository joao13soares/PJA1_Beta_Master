using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadNumberButton : MonoBehaviour, IRaycastResponse
{
   [SerializeField]private KeypadUnlock associatedKeypad;
   [SerializeField] private int number;


   public void OnRaycastSelect()
   {
      associatedKeypad.AddNumber(number);
   }
}
