using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInspectTransition : Transition
{
   private bool canTransition;

   [SerializeField] private DisplayPanel displayPanel;

   private void Awake()
   {
      canTransition = false;
      displayPanel.inspectOpen += ChangeBool;
   }

   public override bool CanTransition()
   {
      if (canTransition)
      {
         canTransition = false;
         return !canTransition;
      }

      return canTransition;
   }


   private void ChangeBool() => canTransition = !canTransition;
  
}
