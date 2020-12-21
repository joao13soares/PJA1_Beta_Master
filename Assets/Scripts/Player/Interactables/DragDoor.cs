using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DragDoor : MonoBehaviour, IRaycastResponse
{
    [SerializeField] private bool startsLocked;

    [SerializeField] private PlayerMovement playerMovementScript;
    [SerializeField] private FlashlightOrientation flashlightOrientationScript;

    [SerializeField] private Transform transformToRotate;

    [SerializeField] private float unlockedMaxAngle, unlockedMinAngle;

    [SerializeField] private float minAngle,
        maxAngle,
        defaultAngle,
        nextAngle;


    public bool isObjectHeld;


    
    
    
    private const float minInputToPlaySound = 0.1f;

    public delegate void DoorAction();

    public event DoorAction moved;
    public event DoorAction stopped;


    private void Awake()
    {
        isObjectHeld = false;

        defaultAngle = transformToRotate.localEulerAngles.y;


        if (startsLocked)
        {
            maxAngle = defaultAngle + 1f;
            minAngle = defaultAngle - 1f;
        }
        else
        {
            minAngle = unlockedMinAngle;
            maxAngle = unlockedMaxAngle;
        }
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

        Transform temp = transformToRotate;

        float mouseInput = Input.GetAxis("Mouse Y") ;
        float frameAngleToAdd = mouseInput * Time.deltaTime * 20f;

        nextAngle += frameAngleToAdd;

        bool isMouseInputValidForSound = mouseInput >= minInputToPlaySound || mouseInput <= -minInputToPlaySound;
        bool isInsideLimits = nextAngle < maxAngle && nextAngle > minAngle;
        
        // Event Condition
        if (isMouseInputValidForSound && isInsideLimits)
            moved?.Invoke();

        else
            stopped?.Invoke();


        nextAngle = Mathf.Clamp(nextAngle, minAngle, maxAngle);

        temp.transform.localEulerAngles = new Vector3(temp.transform.localEulerAngles.x, nextAngle,
            temp.transform.localEulerAngles.z);
        // temp.Rotate(Vector3.up, (Input.GetAxis("Mouse Y") * Time.deltaTime) * 300f, Space.Self);

        Quaternion.Slerp(transformToRotate.localRotation, temp.localRotation, 1f);
    }


    public void UnlockDoor()
    {
        maxAngle = unlockedMaxAngle;
        minAngle = unlockedMinAngle;
    }

    private void DropObject()
    {
        isObjectHeld = false;

        playerMovementScript.enabled = true;
        flashlightOrientationScript.enabled = true;
    }

    public void OnRaycastSelect()
    {
        isObjectHeld = true;
    }
}