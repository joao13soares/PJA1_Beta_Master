using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectToInventoryTransition : Transition
{
    public override bool CanTransition()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
