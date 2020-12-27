using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolToChaseTransition : Transition
{
    [SerializeField]private FieldOfViewDetection fovDetection;
    
    public override bool CanTransition()
    {
        return fovDetection.isPlayerDetected;
    }
}
