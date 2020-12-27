using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolToPatrol : Transition
{
    [SerializeField]private Movement movement;

    private int currentTargetIndex;
    private int lastCellIndex;
    
    public override bool CanTransition()
    {
        // currentTargetIndex = PatrolState.CurrentTargetIndex;
        // lastCellIndex = PatrolState.GetLastCellIndex();

        currentTargetIndex = movement.CurrentTargetIndex;

        if (movement.currentPath == null) return false;
        
        
        lastCellIndex = movement.currentPath.Count -1;

        
        Debug.Log(currentTargetIndex == lastCellIndex);
        return currentTargetIndex == lastCellIndex;

    }
}
