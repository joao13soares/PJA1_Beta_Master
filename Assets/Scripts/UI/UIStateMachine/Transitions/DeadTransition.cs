using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTransition : Transition
{


    [SerializeField] private PlayerHealthManager playerHealth;
    public override bool CanTransition()
    {
        return playerHealth.GetPlayerHP <= 0;
    }
}
