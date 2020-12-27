using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackToChase : Transition
{
   [SerializeField]private ToChaseActionNode node;

   public override bool CanTransition()
   {
      if (!node.canReturnToChase) return false;
      
      
      node.ResetCondition();
      return true;


   }
}
