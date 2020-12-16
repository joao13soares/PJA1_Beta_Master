using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Behaviour/LookWhereYouGo")]
public class LookWhereYouGo : SteeringBehaviour
{
    [SerializeField]
    float maxAngAccel = Mathf.PI / 8f, // 22.5 graus
          targetRadius = 0.3f,
          slowRadius = Mathf.PI/4f,  // 45 graus
          timeToTarget = 5f;
    
    
    [SerializeField]
    float maxAngSpeed = Mathf.PI ;// 4f; // 45 graus

    float maxRotation;
    float epsilon = 0.0001f;


   

     public override Steering GetSteering(MovementInfo origin, Vector3 targetPosition)
    {
        Steering steering = new Steering();

        Vector3 desiredOrientation = targetPosition - origin.position;


        float atan2 = -Mathf.Atan2(-desiredOrientation.x, desiredOrientation.z);
        
       

        float rotation =  atan2 - origin.orientation.x ;
        

        rotation = AuxMethods.NormAngle(rotation);

        float rotationSize = Mathf.Abs(rotation);
        
        
        if (rotationSize < targetRadius ) return steering;

        
        float targetSpeed;
        
        if (rotationSize > slowRadius) 
            targetSpeed = maxAngSpeed;
        else 
           targetSpeed = maxAngSpeed * rotationSize / slowRadius;

        targetSpeed *= Mathf.Sign(rotation);

        
        
        steering.angular.x = targetSpeed;
        steering.angular /= timeToTarget;

        steering.angular.x = Mathf.Clamp(steering.angular.x, -maxAngAccel, maxAngAccel);
        return steering;
    }
}




