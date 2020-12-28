using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BehaviourTree))]
public abstract class BTNode : MonoBehaviour
{
    [SerializeField]private BehaviourTree behaviourTree;

    private void Awake()
    {
        behaviourTree = GetComponent<BehaviourTree>();
    }

    public enum Result
    {
        Failure,
        Running,
        Success
    }


    public virtual Result Execute()
    {
        return Result.Failure;
    }
   
}
