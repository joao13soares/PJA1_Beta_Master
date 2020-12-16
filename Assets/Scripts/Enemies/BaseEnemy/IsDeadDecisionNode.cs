using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDeadDecisionNode : DecisionNode
{
    [SerializeField]private Health enemyHealth;
    
    protected override bool NodeCondition()
    {
        return enemyHealth.GetHealth() <= 0;
    }
}
