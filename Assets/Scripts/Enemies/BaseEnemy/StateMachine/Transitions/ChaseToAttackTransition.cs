using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseToAttackTransition : Transition
{
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float attackRange = 6f;
    
    public override bool CanTransition()
    {
        return Vector3.Distance(playerTransform.position, transform.position) < attackRange;
    }
}
