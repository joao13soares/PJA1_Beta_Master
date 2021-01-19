using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPanel : MonoBehaviour
{
	public GameObject panel;

	// Returns the slot in which the mouse is hovering
	public delegate GameObject OnClick();
	public event OnClick OnMouseClick;

	// InventorySlot selected
	GameObject slotSelected;
	InventorySlot currentInventorySlot;
	private List<Action> currentSlotActions;


	[SerializeField]
	private InspectItemRender inspectItemRender;
	[SerializeField] private GameObject buttonPrefab;

	bool isOpen = false;
	

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (!isOpen)
			{
				// Gets slot where mouse is selected 
				slotSelected = OnMouseClick?.Invoke();
				
				if (slotSelected != null)
				{

					currentInventorySlot = slotSelected.GetComponent<InventorySlot>(); // Get InventorySlot
					currentSlotActions = currentInventorySlot.StoredItem.ItemActions;
					CreatePanelWithActions();

					panel.transform.position = Input.mousePosition; // Mete o o painel junto ao rato
					panel.SetActive(true); // ativa painel

					isOpen = !isOpen; // Altera o bool do painel 

				}
			}
			else
			{
				panel.SetActive(false);
				Transform childPannel = transform.GetChild(0);

				for (int i = 0; i < childPannel.childCount; i++)
				{
					Destroy(childPannel.GetChild(i).gameObject);
				}


				isOpen = !isOpen;
			}

		}
	}


	public delegate void OnAction();

	public event OnAction inspectOpen;
	public event OnAction HealthPlus;
	
	private InventorySlot ReturnSlot() => currentInventorySlot;

	private void CreatePanelWithActions()
	{
		foreach (Action action in currentSlotActions)
		{
			// CREATE BUTTON
			Button newButton = Instantiate(buttonPrefab, transform.GetChild(0)).GetComponent<Button>();

			// CHANGES CHARECTERISTICS
			newButton.GetComponentInChildren<Text>().text = action.actionName;



			///////////////////////////////////////////
			// button.onclick(action.RespectiveAction)
			newButton.onClick.AddListener(
				delegate
				{
					action.RespectiveAction(slotSelected);

                    switch (action.actionName)
                    {
                        case "Inspect":
                            inspectItemRender.inventorySlotForInspect = currentInventorySlot;
                            inspectOpen?.Invoke();
                            DisablePanel(); // Disables panel

                            break;
                        case "Heal":
                            DisablePanel(); // Disables panel
                            HealthPlus?.Invoke();
                            break;

                        case "Recharge":
                            DisablePanel();
                            break;
                    }
                });
		}
	}

	private void DisablePanel() => this.gameObject.transform.GetChild(0).gameObject.SetActive(false);


}
