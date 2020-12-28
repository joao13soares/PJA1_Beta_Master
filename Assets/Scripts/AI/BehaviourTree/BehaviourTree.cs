using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    [SerializeField]private BTNode root;
    private bool behaviourTreeRunning;
    private Coroutine behaviourTreeExecution;

    public Dictionary<string, GameObject> Blackboard;
    
    // Start is called before the first frame update
    void Start()
    {
        behaviourTreeRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!behaviourTreeRunning)
        {
            behaviourTreeExecution = StartCoroutine(StartBehaviourTree());
            behaviourTreeRunning = true;
        }
    }



    private IEnumerator StartBehaviourTree()
    {
        BTNode.Result result = root.Execute();

        while (result == BTNode.Result.Running)
        {
            yield return null;
            result = root.Execute();
        }
        
        Debug.Log("ENDED");
        behaviourTreeRunning = false;



    }
}
