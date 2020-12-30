using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseToAttackTransition : Transition
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Enemy enemyScript;
    
    public override bool CanTransition()
    {
        return Vector3.Distance(playerTransform.position, transform.position) < enemyScript.RangedAttackRange;
    }
}
