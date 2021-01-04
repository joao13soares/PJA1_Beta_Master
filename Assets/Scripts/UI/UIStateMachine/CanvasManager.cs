using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private IAIControlable AIComponent;


    private void Awake()
    {
        AIComponent = GetComponent<IAIControlable>();
    }

    // Update is called once per frame
    void Update()
    {
        AIComponent.ExecuteAIControl();
    }
}
