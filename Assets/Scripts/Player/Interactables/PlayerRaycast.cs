using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    static public float range = 3f;

    
    [SerializeField]private LayerMask raycastLayer;

     

    [SerializeField] private Camera playerCamera;

    [SerializeField] private Inventory inventory;

    private bool isHolding = true;

    [SerializeField]
    private Vector3 mouseP;



    private float pickUpRange, dragRange;



    // Events
    public delegate void OnInteract(GameObject objectHit);





    void Update()
    {
        Vector2 mouse2D = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseP = new Vector3(mouse2D.x, mouse2D.y, 0f);

        Ray playerRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit hit;

        bool isHitting = Physics.Raycast(playerRay, out hit);
        bool hasCollider = hit.collider != null;
        bool hasInput = Input.GetKeyDown(KeyCode.E);


        if (isHitting && hasCollider && hasInput)
        {
            GameObject objectHit = hit.collider.gameObject; //.GetComponent<IRaycastable>();
            float currentDistance = (hit.point - this.transform.position).magnitude;

            if (currentDistance < range)
            {
                IRaycastResponse temp = objectHit.GetComponent<IRaycastResponse>();

                temp?.OnRaycastSelect();
            }
           
        }
       
    }




}