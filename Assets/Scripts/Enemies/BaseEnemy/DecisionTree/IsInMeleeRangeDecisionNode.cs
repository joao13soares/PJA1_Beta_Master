using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInMeleeRangeDecisionNode : DecisionNode
{
    [SerializeField] private Transform playerTransform;
    
    
    private float meleeRange = 3f;
    protected override bool NodeCondition()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= meleeRange;
    }
}
