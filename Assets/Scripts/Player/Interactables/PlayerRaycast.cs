using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    


    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private Crosshair crossair;
    
    


    private float interactRange = 3f;




    void Update()
    {
        Ray playerRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;


        
        bool isHitting = Physics.Raycast(playerRay, out hit, playerLayer);
        bool hasCollider = hit.collider != null;
        bool hasInput = Input.GetKeyDown(KeyCode.E);


        if (!isHitting || !hasCollider) return;
        
        
        GameObject objectHit = hit.collider.gameObject;
        IInteractable temp = objectHit.GetComponent<IInteractable>();


        float currentDistance = (hit.point - this.transform.position).magnitude;
        
        bool isInRange = currentDistance <= interactRange;


        if (isInRange && temp != null)
        {
            crossair.ExpandCrosshair();
            if (hasInput)
                temp.OnRaycastSelect();
        }
        else crossair.CollapseCrosshair();
    }
}