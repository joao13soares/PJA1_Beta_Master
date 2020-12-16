using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStateManager : MonoBehaviour
{
    public GameObject inventory;
    private bool inventoryEnabled = false;

    public delegate void ChangeUI();
    public event ChangeUI InventoryOpened;
    public event ChangeUI InventoryClosed;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) )
        {
            inventoryEnabled = !inventoryEnabled;
            OpenCloseInventory();
        }

    }

    void OpenCloseInventory()
    {
        if (inventoryEnabled)
            InventoryOpened?.Invoke();
        else
        {
            InventoryClosed?.Invoke();
        }
    }
}
