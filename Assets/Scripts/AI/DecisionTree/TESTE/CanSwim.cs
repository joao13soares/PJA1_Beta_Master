using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DecisionTree/Node/CanSwimDecisionNode")]

public class CanSwim : DecisionNode
{

    [SerializeField] private bool canSwim;
    protected override bool NodeCondition()
    {
        return canSwim;
    }
}
