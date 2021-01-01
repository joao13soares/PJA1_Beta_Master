using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnlock : MonoBehaviour, IInteractable
{

    [SerializeField] private Inventory inventoryToCheck;
    [SerializeField] private string keyToUnlock;
    [SerializeField] private int quantityItemToUnlock;
    
    [SerializeField] private DragDoor doorToUnlock;
    
    
    
    private bool CanUnlock()
    {
        return inventoryToCheck.IsStored(keyToUnlock) &&
               inventoryToCheck.GetQuantity(keyToUnlock) == quantityItemToUnlock;

    }

    public void OnRaycastSelect()
    {
        if(CanUnlock()) doorToUnlock.UnlockDoor();

        doorToUnlock.isObjectHeld = true;
    }
}
