using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Action/Inspect")]

public class ItemInspect : Action
{

    private Vector3 lastMousePos;

    // Update is called once per frame
    public override void RespectiveAction(GameObject itemObject)
    {
        // Gets position from mouse in the frame the user starts pressing it
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }

        // Updates lastMousePosition and rotates object
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
            itemObject.transform.rotation = Quaternion.AngleAxis(delta.magnitude * 0.1f, axis) * itemObject.transform.rotation;
        }
        
    }

    
}
