using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsInAttackRangeDecisionNode : DecisionNode
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float attackRange = 6f;
    protected override bool NodeCondition()
    {
        return Vector3.Distance(playerTransform.position, transform.position) < attackRange;
    }
}
