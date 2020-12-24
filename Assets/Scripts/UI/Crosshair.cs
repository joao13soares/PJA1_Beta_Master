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

   

    public void ExpandCrosshair()
    {
        crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize = (currentSize < maxSize) ? ++currentSize : currentSize);
        crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize);
    }

    public void CollapseCrosshair()
    {
        crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentSize = (currentSize > minSize) ? --currentSize : currentSize);
        crosshair.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize);
    }
    
    
    
}
