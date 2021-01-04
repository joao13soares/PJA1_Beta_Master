using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoubleDoor : MonoBehaviour, IInteractable
{

    [SerializeField] private Animation doubleDoorToOpen;
    public void OnRaycastSelect()
    {
        doubleDoorToOpen.Play();
        this.enabled = false;

    }
}
