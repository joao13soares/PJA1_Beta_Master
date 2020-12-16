using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] protected State target;

    public void OnTransition(State origin)
    {
        origin.OnExit();
        target.OnEnter();
    }

    // Each child implements their own logic for the transition
    public abstract bool CanTransition();
}
