using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInputTransition : Transition
{
    public override bool CanTransition()
    {
        Debug.Log("CLICOU NO TAB? " + Input.GetKeyDown(KeyCode.Tab));
        return Input.GetKeyDown(KeyCode.Tab);
    }
}
