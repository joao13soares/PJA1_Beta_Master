using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(menuName="Behaviour/WeightedSteeringBehaviour")]

public class WeightedSteeringBehaviour : ScriptableObject
{
   public float weight;
   public SteeringBehaviour behaviour;

   public void Init()
   {
       behaviour = Instantiate(behaviour);
   }
}
