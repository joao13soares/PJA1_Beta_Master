using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DecisionNode : Node
{
    [SerializeField] private Node trueChild;
    [SerializeField] private Node falseChild;

    
    public override void Execute()
    {
        if (NodeCondition()) trueChild.Execute();
        else falseChild.Execute();
    }

    protected abstract bool NodeCondition();
   
    
}
