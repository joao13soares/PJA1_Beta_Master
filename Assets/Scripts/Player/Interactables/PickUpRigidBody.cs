using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRigidBody : MonoBehaviour, IRaycastResponse
{
    public GameObject playerCam;

    private float ThrowStrength = 50f;
    private float distance = 1f;
    private float maxDistanceGrab = 4f;

    private Ray playerAim;
    public bool isObjectHeld;
    
    public float interactDistance = 0.3f;


    private bool IsInRange =>
        Vector3.Distance(this.transform.position, playerCam.transform.position) > interactDistance;

    private Rigidbody objectRb;
 

    private void Awake()
    {
        isObjectHeld = false;
        objectRb = GetComponent<Rigidbody>();
    }



    public void Update()
    {
        if(!isObjectHeld) return;
        
        HoldObject();
        
       
        // Checks if stops interacting
        if (Input.GetKeyUp(KeyCode.E) || !IsInRange)
            DropObject();
    }
  

    public void HoldObject()
    {
        isObjectHeld = true;
        objectRb.useGravity = false;
        objectRb.freezeRotation = true;
        
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 nextPos = playerCam.transform.position + playerAim.direction * distance;
        Vector3 currPos = this.transform.position;

        objectRb.velocity = (nextPos - currPos) * 20;

        
        
        if (!IsInRange)
        {
            DropObject();
        }
    }

    public void DropObject()
    {
        isObjectHeld = false;
        objectRb.useGravity = true;
        objectRb.freezeRotation = false;
    }

    public void OnRaycastSelect()
    {
        isObjectHeld = true;
    }

  
}
