using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private float PickupRange = 3f;

    public GameObject playerCam;

    public Image crosshair;

    float currentSize = 7;

    float maxSize = 21;

    float minSize = 7;

    private void FixedUpdate()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(playerAim, out hit, PickupRange) )
        {
            if (hit.collider != null && (hit.collider.CompareTag("Dragable") || hit.collider.CompareTag("Equipment") || hit.collider.CompareTag("Item")))
            {             
                crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize = (currentSize < maxSize) ? ++currentSize : currentSize);
                crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize);
            }
            else if(hit.collider!=null)
            {                
                crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize = (currentSize > minSize) ? --currentSize : currentSize);
                crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize);
            }
        }

        else
        {
            crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize = (currentSize > minSize) ? --currentSize : currentSize);
            crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize);
            
        }

    }
}
