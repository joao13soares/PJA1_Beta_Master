using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviour : MonoBehaviour
{
    
  
    public abstract Steering GetSteering(MovementInfo origin, Vector3 target);
}
