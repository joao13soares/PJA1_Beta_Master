using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnlockManager : MonoBehaviour, IRaycastResponse
{

    [SerializeField] private DragDoor dragDoor;
    public void OnRaycastSelect()
    {
        // if(CanUnlock()) dragDoor.UnlockDoor();

        dragDoor.isObjectHeld = true;

    } 
    

    protected abstract bool UnlockDoor();
}
