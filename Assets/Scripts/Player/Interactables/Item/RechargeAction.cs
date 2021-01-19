using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeAction : Action
{
    [SerializeField] FlashLightInput fi;

    public override void RespectiveAction(GameObject itemObject)
    {
        ISlot slot = itemObject.GetComponent<ISlot>();

        fi.Recharge();
        slot.StoredItem.RemoveItem();
    }
}
