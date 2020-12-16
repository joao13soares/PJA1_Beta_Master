using System;
using UnityEngine;

[Serializable]
public class MovementInfo
{

    // Stores tranform position
    public Vector3 position;
    
    // Movement direction and speed
    public Vector3 velocity;

    // Stores orientation from transform so we can clamp the Y axis
    public Vector2 orientation;

    // Rotation direction and speed
    public Vector2 rotation;
    
  
    
   
}