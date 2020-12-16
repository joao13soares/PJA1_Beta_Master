using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolToPatrol : Transition
{
    [SerializeField]private PatrolState PatrolState;

    private int currentTargetIndex;
    private int lastCellIndex;
    
    public override bool CanTransition()
    {
        currentTargetIndex = PatrolState.CurrentTargetIndex;
        lastCellIndex = PatrolState.GetLastCellIndex();

        return currentTargetIndex == lastCellIndex;

    }
}
