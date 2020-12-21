using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInputTransition : Transition
{
    public override bool CanTransition()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
}
