using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/State/LookForState")]

public class LookForState : State
{
    protected override void CreateActions()
    {
        onEnterActions.Add(EnterLookState);
        onStayActions.Add(LookAction);
        onExitActions.Add(ExitLookState);
    }

    void LookAction()
    {
        Debug.Log("STAY looking for");
    }

    void EnterLookState()
    {
        Debug.Log("ENTER looking for");

    }

    void ExitLookState()
    {
        Debug.Log(" EXIT looking for");

    }
}
