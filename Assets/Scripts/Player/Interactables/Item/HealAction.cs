using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealAction : Action
{
    [SerializeField] PlayerHealthManager phm;

	public override void RespectiveAction(GameObject itemObject)
	{
		ISlot slot = itemObject.GetComponent<ISlot>();

        phm.HealHealth(100);
		slot.StoredItem.RemoveItem();
	}
}
