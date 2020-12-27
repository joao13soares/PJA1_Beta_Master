using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/State/AttackState")]

public class AttackState : State
{
    [SerializeField]private DecisionTree attackDecisionTree;
    
    
    protected override void CreateActions()
    {
        onEnterActions.Add(AttackEnterAction);
        onExitActions.Add(AttackExitAction);
        
    }


    void AttackEnterAction()
    {
        attackDecisionTree.enabled = true;
    }

    void AttackExitAction()
    {
        attackDecisionTree.enabled = false;

    }
   
}
