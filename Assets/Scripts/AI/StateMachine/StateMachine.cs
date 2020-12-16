using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class StateMachine : MonoBehaviour
{
    private State currentState;
    [SerializeField] private List<State> allStates;

    private void Awake()
    {
        StateEventSubscription();
        currentState = allStates.ElementAt(0);
        currentState.OnEnter();
    }
    
    // protected abstract void  StateCreation();
    void StateEventSubscription()
    {
        foreach (State state in allStates)
        {
            state.StateChanged += ChangeState;
        }
    }
    
    void Update()
    {
        currentState.OnStay();
    }
    private void ChangeState(State newState)
    {
        Debug.Log("new state:" + newState);
        currentState = newState;
    }
}
