using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DecisionTree/Node/HumanActionNode")]

public class HumanAction : ActionNode
{
   

    protected override void ExecuteAction()
    {
        Debug.Log("Is Human!");
    }
}
