using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrawer : MonoBehaviour,IInteractable
{
    
    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private FlashlightOrientation flashlightOrientationScript;

    [SerializeField] private Transform transformToMove;
    
    [SerializeField] private float minPos,
        maxPos,
        defaultPos,
        nextPos;
    
    public bool isObjectHeld;
    
    public delegate void DrawerAction();
    
    public event DrawerAction moved;
    public event DrawerAction stopped;
    
    private const float minInputToPlaySound = 0.1f;

    
    private void Awake()
    {
        isObjectHeld = false;

        defaultPos = transformToMove.localPosition.z;
        minPos = defaultPos;

    }
    
    private void Update()
    {
        
        
        if (!isObjectHeld) return;

        HoldObject();

        // Checks if stops interacting
        if (Input.GetKeyUp(KeyCode.E))
            DropObject();
    }
    
    
    private void HoldObject()
    {
        playerMovementScript.enabled = false;
        flashlightOrientationScript.enabled = false;

        Transform temp = transformToMove;

        float mouseInput = Input.GetAxis("Mouse Y") ;
        float frameTranslationToAdd = mouseInput * Time.deltaTime;//* 5f;

        nextPos += frameTranslationToAdd;

        bool isMouseInputValidForSound = mouseInput >= minInputToPlaySound || mouseInput <= -minInputToPlaySound;
        bool isInsideLimits = nextPos < maxPos && nextPos > minPos;
        
        // Event Condition
        if (isMouseInputValidForSound && isInsideLimits)
            moved?.Invoke();

        else
            stopped?.Invoke();


        nextPos = Mathf.Clamp(nextPos, minPos, maxPos);
    
        temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, temp.transform.localPosition.y,
            nextPos);
       

        Vector3.Slerp(transformToMove.localPosition, temp.localPosition, 1f);
        
        
        // // Open handle direction
        // handleDirectionToRotate = -1;
       

        
    }
    
    private void DropObject()
    {
        isObjectHeld = false;

        playerMovementScript.enabled = true;
        flashlightOrientationScript.enabled = true;
        
        stopped?.Invoke();

       


    }
    
    
    public void OnRaycastSelect()
    {
        isObjectHeld = true;
    }
}
