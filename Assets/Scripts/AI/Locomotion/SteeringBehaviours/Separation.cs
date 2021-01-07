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

        float sphereRadius = 0.3f;

        Ray ray = new Ray(origin.position, origin.orientation);


        Collider[] collidersHit = Physics.OverlapSphere(origin.position, sphereRadius, enemyLayerMask);


        foreach (Collider hit in collidersHit)
        {
            if (hit.gameObject.transform.position != origin.position)
            {

                steering.linear += origin.position - hit.transform.position;
                steering.linear.y *= 0f;
            }


        }

        return steering;
    }
}