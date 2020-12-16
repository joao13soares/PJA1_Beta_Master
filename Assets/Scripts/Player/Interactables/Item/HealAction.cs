using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Action/Heal")]

public class HealAction : Action
{
	public override void RespectiveAction(GameObject itemObject)
	{
		ISlot slot = itemObject.GetComponent<ISlot>();
		
		slot.StoredItem.RemoveItem();

		Debug.Log("BIG BIG HEAL");

		
		
	}
}
