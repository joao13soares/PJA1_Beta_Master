using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DecisionTree/Node/IsBipedeDecisionNode")]

public class IsBipede : DecisionNode
{

    
    
    [SerializeField] private bool isBipede;
    protected override bool NodeCondition()
    {
        return isBipede;
    }
    
    
    
    
}
