using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveActionNode : ActionNode
{
    [SerializeField] private StateMachine enemyStateMachine;
    protected override void ExecuteAction()
    {
        if(!enemyStateMachine.enabled)
        TurnOnStateMachine();
    }

    void TurnOnStateMachine()
    {
        enemyStateMachine.enabled = true;
    }
    
}
