using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public  class StateMachine : MonoBehaviour, IAIControlable
{
    private State currentState;
    [SerializeField] private List<State> allStates;

    private void Awake()
    {
        StateEventSubscription();
        currentState = allStates.ElementAt(0);
    }

    private void Start()
    {
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
    
 

    public void ExecuteStateMachine() => currentState.OnStay();
    
    private void ChangeState(State newState)
    {
        currentState = newState;
    }

    public void ExecuteAIControl()
    {
        ExecuteStateMachine();
    }
}
