using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : ScriptableObject
{
    
    public virtual SteeringBehaviour Init()
    {
        return Instantiate(this);
    }
    public abstract Steering GetSteering(MovementInfo origin, Vector3 target);
}
