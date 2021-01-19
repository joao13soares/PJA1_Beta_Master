using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlashLightInput))]
public class FlashLightPickup : MonoBehaviour, IInteractable,IPickUpable
{

    [SerializeField] private Transform transformForParentEquip;
    private readonly Vector3 defaultLocalPosition = new Vector3(-0.298f,0.101f,0.281f);
    private readonly Vector3 defaultLocalRotation = new Vector3(11.59f,17.068f,-2.183f);
    
    // Variables
    [SerializeField] protected List<Action> itemActions;
    [SerializeField] protected GameObject itemGameObjectForInspect;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected Sprite highlightIcon;
    [SerializeField] protected GameObject uiIcon;


    [SerializeField] protected string type = "Flashlight";
    [SerializeField] protected bool isPermanent = true;
    
    
    

    
    //ACESSORS
    public List<Action> ItemActions => itemActions;
    public GameObject ItemGameObjectForInspect => itemGameObjectForInspect;
    public Sprite Icon => icon;
    public Sprite HighlightIcon => highlightIcon;
    public string Type => type;
    public bool IsPermanent => isPermanent;
    
    
    // Inventory to be stored
    [SerializeField] protected Inventory inventory;

    private FlashLightInput flashLightInput;


    private void Awake()
    {
        flashLightInput = GetComponent<FlashLightInput>();
    }

    public void OnRaycastSelect()
    {
        StoreItem();
    }

    public void StoreItem()
    {
       inventory.AddItemSlot(this.gameObject);
       EquipFlashlight();
    }


    private void EquipFlashlight()
    {
        transform.SetParent(transformForParentEquip);
        transform.localPosition = defaultLocalPosition;
        transform.localEulerAngles = defaultLocalRotation;
        flashLightInput.enabled = true;

        uiIcon.SetActive(true);

    }

    public void RemoveItem()
    {
        
    }
}
