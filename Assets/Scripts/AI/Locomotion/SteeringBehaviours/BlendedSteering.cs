using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(menuName="Behaviour/WeightedBlending")]

public class BlendedSteering : SteeringBehaviour
{
    [SerializeField] private List<WeightedSteeringBehaviour> behavioursToBlend;
    
    [SerializeField] private float maxAcceleration;
    [SerializeField] private float maxRotation;

    private void Awake()
    {
        foreach (WeightedSteeringBehaviour wsb in behavioursToBlend.ToArray())
        {
            // wsb = Instantiate(wsb);
            wsb.Init();
        }
    }

    public override Steering GetSteering(MovementInfo origin, Vector3 target)
    {
        Steering steering = new Steering();
        float totalWeights = 0f;

        foreach (WeightedSteeringBehaviour behaviour in behavioursToBlend)
        {
            Steering tmp = behaviour.behaviour.GetSteering(origin, target);
            steering.linear  += tmp.linear * behaviour.weight;
            steering.angular += tmp.angular * behaviour.weight;
            totalWeights += behaviour.weight;
        }
        
        // Media pesada
        steering.angular /= totalWeights;
        steering.linear  /= totalWeights;
        
        // Nao ultrapassar limites de aceleração
        steering.linear  = Vector3.ClampMagnitude(steering.linear, maxAcceleration);

        steering.angular = Vector2.ClampMagnitude(steering.angular, maxRotation);
        
        
        return steering;
    }
}
