using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BehaviourTree))]
public abstract class BTNode : MonoBehaviour
{
   protected BehaviourTree behaviourTree;

    protected  virtual void Awake()
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

    public virtual void OnInterrupt()
    {
        // Debug.Log("NODE INTERRUPTED");;
    }
}
