using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Camera inspectCamera;

    // [SerializeField] GameObject backButton;

    // Scripts with events for changing UI
    [SerializeField] private UiStateManager uiStateManager;
    [SerializeField] private DisplayPanel displayPanel;
    [SerializeField] private InspectItemRender inspectItemRender;


    // Canvas Sections 
    [SerializeField] private GameObject inventoryUI;

    [SerializeField] private GameObject inGameUI;

    [SerializeField] private GameObject inspectUI; // NOT BEING USED FOR NOW

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private FlashlightOrientation flashlightOrientation;

    void Awake()
    {
        uiStateManager.InventoryOpened += ToInventory;
        uiStateManager.InventoryClosed += ToInGame;
        // inspectMode.InspectOpened += ToInspect;
        // inspectMode.InspectClosed += ToInventory;

        displayPanel.inspectOpen += ToInspect;

        inspectItemRender.InspectClosed += ToInventory;
    }

    // Inicial State do caralho que o foda
    void Start()
    {
        ToInGame();
    }

    public void ToInspect()
    {
        EnableMouse(); // Enables mouse cursor
        DisablePlayerMovement(); // Disables player movement Script
        
        inspectCamera.depth = 2; // Changes Camera to Inspect Camera

        inspectUI.SetActive(true);    // Turns ON Inspect UI
        inventoryUI.SetActive(false); // Turns OFF Inventory UI
        inGameUI.SetActive(false);    // Turns OFF InGame UI
    }

    public void ToInventory()
    {
        EnableMouse(); // Enables mouse cursor
        DisablePlayerMovement(); // Disables player movement Script

        inspectCamera.depth = 0; // Changes Camera to Main Camera

        inventoryUI.SetActive(true); // Turns ON Inventory UI
        inGameUI.SetActive(false);   // Turns OFF InGame UI
        inspectUI.SetActive(false);  // Turns OFF Inspect UI
    }

    public void ToInGame()
    {
        
        DisableMouse(); // Disables mouse cursor
        EnablePlayerMovement(); // Enables player movement Script

        inspectCamera.depth = 0; // Changes Camera to Main Camera(in case of direct inspect from inGame)

        inGameUI.SetActive(true);     // Turns ON InGame UI
        inventoryUI.SetActive(false); // Turns OFF Inventory UI
        inspectUI.SetActive(false);   // Turns OFF Inspect UI
    }

    private void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    private void EnablePlayerMovement()
    {
        playerMovement.enabled = true;
        flashlightOrientation.enabled = true;
    }
    
    private void DisablePlayerMovement()
    {
        playerMovement.enabled = false;
        flashlightOrientation.enabled = false;
    }
}