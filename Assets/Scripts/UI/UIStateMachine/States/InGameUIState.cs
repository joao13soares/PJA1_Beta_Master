using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIState : State
{
    [SerializeField]private WeaponManager weaponManager;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private FlashlightOrientation flashlightOrientation;

    [SerializeField] private GameObject inGameUI;
    
    protected override void CreateActions()
    {
        onEnterActions.Add(EnterActions);
        onExitActions.Add(ExitActions);
    }


    private void EnterActions()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        inGameUI.SetActive(true);
        
        weaponManager.enabled = true;
        playerMovement.enabled = true;
        flashlightOrientation.enabled = true;

    }

    private void ExitActions()
    {
        inGameUI.SetActive(false);
        
        weaponManager.enabled = false;
        playerMovement.enabled = false;
        flashlightOrientation.enabled = false;
    }
}
