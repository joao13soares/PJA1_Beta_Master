using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour,IAIControlable
{
    [SerializeField]private BTNode root;
    private bool behaviourTreeRunning;

    
    // Start is called before the first frame update
    void Start()
    {
        behaviourTreeRunning = false;
    }




    

    private IEnumerator StartBehaviourTree()
    {
        BTNode.Result result = root.Execute();

        while (result == BTNode.Result.Running)
        {
            yield return null;
            result = root.Execute();
        }
        
        
        behaviourTreeRunning = false;



    }

    public void ExecuteAIControl()
    {
        if (!behaviourTreeRunning)
        {
            behaviourTreeRunning = true;
            StartCoroutine(StartBehaviourTree());
        }
    }
}
