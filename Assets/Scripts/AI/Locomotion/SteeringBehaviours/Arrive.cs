using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Behaviour/Arrive")]
public class Arrive : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f, slowRadius = 0.05f, targetRadius = 0.01f, timeToTarget = 5f;
    [SerializeField]
    float maxSpeed = 10f;

     public override Steering GetSteering(MovementInfo origin, Vector3 target) {
         
         
        Steering steering = new Steering();
        // vector from npc.position to target.position
        
        
        Vector3 convertOriginTo2D = new Vector3(origin.position.x, 0f, origin.position.z);
        Vector3 convertTargetTo2D= new Vector3(target.x, 0f, target.z);
        
        Vector3 direction = convertTargetTo2D - convertOriginTo2D;
        // distance to target
        float distance = direction.magnitude;
        
        // Are we there yet? (Shrek, 2001)
        if (distance < targetRadius) return steering;
        float targetSpeed;
        if (distance > slowRadius) targetSpeed = maxSpeed;
        //    slowRadius --- maxSpeed
        //    distance   --- targetSpeed
        else targetSpeed = maxSpeed * distance / slowRadius;

        Vector3 targetVelocity = direction.normalized;
        targetVelocity *= targetSpeed;
        steering.linear = targetVelocity;
        steering.linear /= timeToTarget;

        steering.linear = Vector3.ClampMagnitude(steering.linear, maxAccel);

        return steering;
    }

    
}
