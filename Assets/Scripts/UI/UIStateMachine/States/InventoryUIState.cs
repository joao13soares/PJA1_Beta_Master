using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIState : State
{
    [SerializeField]private GameObject inventoryUI;


    public override void Start()
    {
        inventoryUI.SetActive(false);
        CreateActions();
    }

    protected override void CreateActions()
    {
        onEnterActions.Add(OnEnterActions);
        onExitActions.Add(OnExitActions);
    }


    private void OnEnterActions()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inventoryUI.SetActive(true);
    }

    private void OnExitActions()
    {
        inventoryUI.SetActive(false);
    }
    
}
