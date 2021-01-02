using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseToPatrolTransition : Transition
{
    [SerializeField] private FieldOfViewDetection fovDetection;
    [SerializeField] private Movement movementToCheckEnd;

    private float timeWithoutdetection = 10f;
    [SerializeField]private float currentUnknownTime;

    public override bool CanTransition()
    {
        if (movementToCheckEnd.currentPath == null) return false;
        
        if (!fovDetection.isPlayerDetected &&
            movementToCheckEnd.CurrentTargetIndex == movementToCheckEnd.currentPath.Count-1)
            currentUnknownTime += Time.deltaTime;


        if (currentUnknownTime >= timeWithoutdetection)
        {
            currentUnknownTime = 0f;
            return true;
        }

        return false;
    }
}