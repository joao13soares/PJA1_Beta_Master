using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTComposite
{

    public override Result Execute()
    {
        Result result = children[currentChild].Execute();

        if (result == Result.Running) return result;

        if (result == Result.Success)
        {
            ResetChildIndex();
            return result;
        }


        currentChild++;
        if (currentChild < children.Count) return Result.Running;


        ResetChildIndex();
        return Result.Failure;
    }

    private void ResetChildIndex() => currentChild = 0;
    
  
}