using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTabManager : MonoBehaviour
{
	[SerializeField]
	private Button inventoryTab, mapTab, healthTab;
	[SerializeField]
	private GameObject inventoryUI, mapUI, healthUI;

	int highlightHeightUp = 510;
	int highlightHeightDown = 490;

	// Start is called before the first frame update
	void Awake() 
    {
		inventoryTab.onClick.AddListener(InventoryTabOnClick);
		mapTab.onClick.AddListener(MapTabOnClick);
		healthTab.onClick.AddListener(HealthTabOnClick);
		
		InventoryTabOnClick(); // START WITH INVENTORY TAB
    }

	
    private void InventoryTabOnClick()
	{
		inventoryUI.SetActive(true);
		mapUI.SetActive(false);
		healthUI.SetActive(false);

		HighlightTab(highlightHeightUp, highlightHeightDown, highlightHeightDown);

	}

	private void MapTabOnClick()
	{
		mapUI.SetActive(true);
		inventoryUI.SetActive(false);
		healthUI.SetActive(false);

		HighlightTab(highlightHeightDown, highlightHeightUp, highlightHeightDown);

	}

	private void HealthTabOnClick()
	{
		healthUI.SetActive(true);
		inventoryUI.SetActive(false);
		mapUI.SetActive(false);

		HighlightTab(highlightHeightDown, highlightHeightDown, highlightHeightUp);

	}


	private void HighlightTab(int a, int b, int c)
	{
		inventoryTab.transform.localPosition = new Vector3(
			inventoryTab.transform.localPosition.x,
			a,
			inventoryTab.transform.localPosition.z);

		mapTab.transform.localPosition = new Vector3(
			mapTab.transform.localPosition.x,
			b,
			mapTab.transform.localPosition.z);

		healthTab.transform.localPosition = new Vector3(
			healthTab.transform.localPosition.x,
			c,
			healthTab.transform.localPosition.z);
	}

	
}
