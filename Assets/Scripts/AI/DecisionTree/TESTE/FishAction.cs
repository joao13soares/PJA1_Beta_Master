using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DecisionTree/Node/FishActionNode")]

public class FishAction : ActionNode
{
    protected override void ExecuteAction()
    {
        Debug.Log("Is Fish");
    }
}
