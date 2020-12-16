using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DecisionTree/Node/CanFlyDecisionNode")]

public class CanFly : DecisionNode
{
    [SerializeField] private bool canFly;
    protected override bool NodeCondition()
    {
        return canFly;
    }
}
