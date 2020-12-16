using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionTree : MonoBehaviour
{
    [SerializeField] private Node root;
    
    void Update()
    {
        root.Execute();
    }
}
