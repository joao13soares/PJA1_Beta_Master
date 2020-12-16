using UnityEngine;

// This class is returned by the steering behavior algorithms
public class Steering 
{
    // linear force
    public Vector3 linear;
    // angular force
    public Vector2 angular;

    public Steering()
    {
        linear = Vector3.zero;
        angular = Vector2.zero;
    }
        
    
    
}
