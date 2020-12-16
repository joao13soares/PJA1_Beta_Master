using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class InventorySlot : MonoBehaviour, ISlot
{
    private IPickUpable storedItem;
    private int quantity = 1;

    //ACESSORS
    public IPickUpable StoredItem { get => storedItem; set => storedItem = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public Sprite GetIcon => storedItem.Icon;
   

    public string GetType => storedItem.Type;
    
    public GameObject GetGameObjectForInspect => storedItem.ItemGameObjectForInspect;


}