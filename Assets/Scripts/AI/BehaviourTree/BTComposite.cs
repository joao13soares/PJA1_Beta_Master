using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTComposite : BTNode
{
    [SerializeField] protected List<BTNode> children;
    protected int currentChild;


    public override void OnInterrupt()
    {
        currentChild = 0;
    }
}
