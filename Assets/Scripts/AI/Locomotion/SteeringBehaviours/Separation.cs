using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Separation")]
public class Separation : SteeringBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask;

    public override Steering GetSteering(MovementInfo origin, Vector3 target)
    {
        Steering steering = new Steering();

        float sphereRadius = 0.7f;

        Ray ray = new Ray(origin.position, origin.orientation);


        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereRadius,sphereRadius,enemyLayerMask);
        
      

        
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.transform.position != origin.position)
            {
                
                steering.linear += hit.point - origin.position;
                steering.linear.y *= 0f;
            }
            
               
        }

        return steering;
    }
}