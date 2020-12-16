using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletHole
{
    public float existenceCountdown = 2.0f;
    public GameObject pelletHoleObj;

    public PelletHole(GameObject newPelletHole)
    {
        this.pelletHoleObj = newPelletHole;
    }
}
