using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequencer : BTComposite
{
    private int currentChild;

    public override Result Execute()
    {
        
        Result result = children[currentChild].Execute();

        if (result == Result.Running) return result;

        if (result == Result.Failure)
        {
            ResetChildIndex();
            return result;
        }


        currentChild++;
        if (currentChild < children.Count) return Result.Running;

        ResetChildIndex();
        return Result.Success;
    }


    private void ResetChildIndex() => currentChild = 0;
}