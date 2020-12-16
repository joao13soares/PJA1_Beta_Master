using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DecisionTree/Node/BirdActionNode")]

public class BirdAction : ActionNode
{
  
    protected override void ExecuteAction()
    {
        Debug.Log("Is bird");
    }
}
