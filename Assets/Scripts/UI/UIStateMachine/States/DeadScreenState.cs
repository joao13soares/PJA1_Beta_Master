using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreenState : State
{
    [SerializeField] private GameObject deadScreenUI;


    public override void OnEnable()
    {
        deadScreenUI.SetActive(false);
        base.OnEnable();
    }

    protected override void CreateActions()
    {
        onEnterActions.Add(EnterActions);
    }


    void EnterActions()
    {
        Cursor.lockState = CursorLockMode.None;
            deadScreenUI.SetActive(true);
        
        
    }
}
