using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectItemRender : MonoBehaviour
{


    // Current slot for inspect
    public InventorySlot inventorySlotForInspect;

    // Script for the event to load the gameObject from current slot
    [SerializeField] private DisplayPanel displayPanel;
    
    // Item from slot to inspect 
    GameObject instantiatedItem;


    [SerializeField] private Camera inspectCamera;
    private float cameraZPosition;
    private const float maxXoom = -1.6f;
    private const float minXoom = -2.7f;

    
    // Event for leaving inspect mode
    public delegate void ChangeUI();
    public event ChangeUI InspectClosed;

    void Awake()
    {
        displayPanel.inspectOpen += ItemLoad;
        InspectClosed += ItemDestroy;

        cameraZPosition = inspectCamera.transform.localPosition.z;

    }

    public void ItemLoad()
    {
        GameObject itemGameObjectForInspect = inventorySlotForInspect.StoredItem.ItemGameObjectForInspect;

        Debug.Log("Spawn");
        instantiatedItem = Instantiate(itemGameObjectForInspect, this.transform.position, Quaternion.identity);
        
    }

    void Update()
    {
        // Used for rotation of item
        inventorySlotForInspect?.StoredItem.ItemActions[0].RespectiveAction(instantiatedItem);


        if (Input.mouseScrollDelta.y != 0)
        {
            cameraZPosition += Input.mouseScrollDelta.y * Time.deltaTime;

            cameraZPosition = Mathf.Clamp(cameraZPosition, minXoom, maxXoom);
            
            inspectCamera.transform.localPosition = new Vector3(inspectCamera.transform.localPosition.x, inspectCamera.transform.localPosition.y, cameraZPosition);

        }

        
        // Check for Esc key to leave (destroys item and changes to InventoryUI)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            InspectClosed?.Invoke();
            
        }
    }

    void ItemDestroy()
    {
        GameObject.Destroy(instantiatedItem);
    }
}