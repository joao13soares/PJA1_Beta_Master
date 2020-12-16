using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	
	// Prefab slot for instantiate
	[SerializeField] GameObject referenceSlot;
	
	// Inventory storage
	Dictionary<string, GameObject> inventory;


	private void Awake()
	{
		inventory = new Dictionary<string, GameObject>();

	}

	public void AddItemSlot(GameObject item)
	{
		// Gets script from itemPickedUp object
		IPickUpable itemInfo = item.GetComponent<IPickUpable>();

		Debug.Log(itemInfo.Type);

		// If item of same type is already stored stacks it and returns
		if (IsStored(itemInfo.Type))
		{
			Debug.Log("JA TEM GUARDADO");
			ISlot storedItem = inventory[itemInfo.Type].GetComponent<ISlot>();
			
			Debug.Log(storedItem.Quantity);

			storedItem.Quantity++;
			Debug.Log(storedItem.Quantity);

			UpdateQuantityUI(inventory[itemInfo.Type]);

			return;
		}

		// Creates new slot
		CreateNewSlot(itemInfo);




	}

   

	private void CreateNewSlot(IPickUpable itemPickedUp)
	{

		GameObject newSlotObject = Instantiate(referenceSlot, this.transform);
		ISlot newSlot = newSlotObject.GetComponent<ISlot>();
		
		newSlot.StoredItem = itemPickedUp;
		
		inventory.Add(newSlot.GetType, newSlotObject);
		
		newSlotObject.GetComponent<Image>().sprite = newSlot.GetIcon;
		
		UpdateQuantityUI(newSlotObject);
	

	}

	public void UpdateQuantityUI(GameObject currentItem)
	{
		ISlot currentSlot = currentItem.GetComponent<ISlot>();

		
		// Gets number for UI
		int quantityToUpdate;

		if (currentSlot.StoredItem.IsPermanent) quantityToUpdate = currentSlot.Quantity - 1;
		else quantityToUpdate = currentSlot.Quantity ;
		
		// Updates UI
		if(quantityToUpdate > 0)
		currentItem.GetComponentInChildren<Text>().text = "x" + quantityToUpdate;
		else 
			currentItem.GetComponentInChildren<Text>().text = "";


	}
	
	public bool IsStored(string key) => inventory.ContainsKey(key);

	public int GetQuantity(string key) => inventory[key].GetComponent<ISlot>().Quantity;
	
	public void RemoveSlot(string key)
	{
		
		
		
		ISlot slotToRemove = inventory[key].GetComponent<ISlot>();
		bool isPermanent = slotToRemove.StoredItem.IsPermanent;

		int permanentItemLimit = 1;
		int notPermanentItemLimit = 0;

		// Checks if item 
		if (isPermanent)
		{
			if (slotToRemove.Quantity > permanentItemLimit)
			{
				slotToRemove.Quantity--;
				UpdateQuantityUI(inventory[key]);

				
			}
		}
		else
		{
			if (slotToRemove.Quantity > notPermanentItemLimit)
				slotToRemove.Quantity--;
			

			// Checks if quantity is lower than the minimum amount (0)
			if (slotToRemove.Quantity == notPermanentItemLimit)
			{
				GameObject toDelete = inventory[key];
				inventory.Remove(key);
				Destroy(toDelete);

			}
			else 
				UpdateQuantityUI(inventory[key]);

		}
		

	}
	
}
