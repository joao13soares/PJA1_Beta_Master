using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadUI : MonoBehaviour
{
   [SerializeField]private KeypadUnlock correspondentKeypad;

   private Text currentCodeText;

   void Awake()
   {
      currentCodeText = GetComponentInChildren<Text>();
      correspondentKeypad.CodeChanged += UpdateText;

   }

   private void UpdateText()
   {
      
      currentCodeText.text = "";
      foreach (int i in correspondentKeypad.currentCode)
      {
         currentCodeText.text += i.ToString();
      }
   }
   
   
}
