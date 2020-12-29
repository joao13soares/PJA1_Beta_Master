using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParallel : BTComposite
{
    public override Result Execute()
    {
        foreach (BTNode childNode in children)
        {
            Result childResult = childNode.Execute();

            if (childResult == Result.Failure)
            {
                OnChildResultFailure();
                return Result.Failure;
            }

            if (childResult == Result.Running)
                return childResult;
        }

        return Result.Success;
    }


    void OnChildResultFailure()
    {
        foreach (BTNode childNode in children)
        {
            childNode.OnInterrupt();
        }
    }


    public override void OnInterrupt()
    {
        OnChildResultFailure();
    }
}