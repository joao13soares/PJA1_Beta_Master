using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class IsInAttackRangeDecisionNode : DecisionNode
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Enemy enemyScript ;

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
    }

    protected override bool NodeCondition()
    {
        return Vector3.Distance(playerTransform.position, transform.position) < enemyScript.RangedAttackRange;
    }
}
