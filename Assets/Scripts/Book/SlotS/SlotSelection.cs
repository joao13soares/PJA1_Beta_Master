using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SlotSelection : MonoBehaviour 
{

	[SerializeField] private DisplayPanel displayPanel;
	//LUis
	//Image itemInfo;
	[SerializeField] private GameObject itemInfo;
	//LUis

	void Awake()
    {
        displayPanel = GameObject.Find("DisplayPanel").GetComponent<DisplayPanel>();
		itemInfo = GameObject.Find("ItemInfo");
    }

    public void OnPointerEnter()
    {
        displayPanel.OnMouseClick += GameObjectReturns;

        
		if (itemInfo == null) return; // On healthInventory there is no place to show image on mouse over 
		
		itemInfo.GetComponent<Mask>().enabled = false; 
		itemInfo.GetComponent<Image>().sprite = this.gameObject.GetComponent<ISlot>().StoredItem.Icon;

		Debug.Log("entrou");
    }
    public void OnPointerExit()
    {
        displayPanel.OnMouseClick -= GameObjectReturns;

        if (itemInfo == null) return; // On healthInventory there is no place to show image on mouse over 

		itemInfo.GetComponent<Mask>().enabled = true;

		Debug.Log("saiu");
    }

    GameObject GameObjectReturns() => this.gameObject;
    

}
