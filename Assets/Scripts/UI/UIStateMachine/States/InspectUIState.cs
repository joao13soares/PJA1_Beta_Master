using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectUIState : State
{
    [SerializeField] private GameObject inspectUI;
    [SerializeField] private Camera inspectCamera;
    [SerializeField] private InspectItemRender itemRender;
     
    protected override void CreateActions()
    {
        onEnterActions.Add(OnEnterActions);
        onExitActions.Add(OnExitActions);
    }


    private void OnEnterActions()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inspectCamera.depth = 2;
        
        inspectUI.SetActive(true);
    }

    private void OnExitActions()
    {
        inspectCamera.depth = 0;
        
        //DELETE OBJECT
        itemRender.ItemDestroy();

        inspectUI.SetActive(false);
        
    }
}
