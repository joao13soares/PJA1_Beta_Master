
using UnityEngine;

public class IsPlayerDetectedBTNode : BTNode
{
    [SerializeField]private FieldOfViewDetection fovDetection;
    public override Result Execute()
    {
        if (fovDetection.isPlayerDetected) return Result.Failure;

        return Result.Success;
    }
}
