using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    IPickUpable StoredItem { get; set; }
    int Quantity { get; set; }
    string GetType { get; }
    Sprite GetIcon { get; }
    
}