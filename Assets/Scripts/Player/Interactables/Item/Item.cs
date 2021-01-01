using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(menuName ="Interactable/Item")]

public class Item : MonoBehaviour, IPickUpable, IInteractable
{
	// -------------------------------------IPickUpable---------------------------------------------//

	// InventorySlot manager
	[SerializeField] private Inventory inventory;

	// List of actions for the item
	[SerializeField] private List<Action> itemActions;

	// Item for inspect item menu and inventory icon
	[SerializeField] private GameObject itemGameObjectForInspect;
	[SerializeField] private Sprite icon;
	[SerializeField] private string type = "Item";
	[SerializeField] private bool isPermanent = false ;


	
	//ACESSORS
	public Inventory Inventory => inventory;
	public List<Action> ItemActions => itemActions;
	public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
	public Sprite Icon => icon;
	public string Type => type;

	public bool IsPermanent => isPermanent;
	public Inventory InventoryToStore { get; }


	// -------------------------------------------------------------------------------------------//
	
	
	
    public void StoreItem()
    {
        inventory.AddItemSlot(this.gameObject);
        Destroy(this.gameObject);
    }
    
    public void RemoveItem()
    {
	    inventory.RemoveSlot(type);
    }
    


    public void OnRaycastSelect()
    {
        StoreItem();
    }

    public void OnRaycastDiselect()
    {
        //pra nao dar erro XD
    }

 


    
    
}