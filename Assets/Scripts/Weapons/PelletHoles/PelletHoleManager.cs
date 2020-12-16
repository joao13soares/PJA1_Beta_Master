using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletHoleManager
{
    List<PelletHole> pelletHoles;
    GameObject pelletHolePrefab;
    
    
    [SerializeField] private Camera playerCamera;
    
    public PelletHoleManager()
    {
        pelletHoles = new List<PelletHole>();
        
        pelletHolePrefab = Resources.Load("Prefabs/Pellet Hole") as GameObject;
        
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void NewPelletHole(Vector3 hitPoint, GameObject hitObject)
    {
        GameObject newPelletHole = Object.Instantiate(pelletHolePrefab, hitPoint, playerCamera.transform.rotation);
        
        newPelletHole.transform.parent = hitObject.transform;
        
        pelletHoles.Add(new PelletHole(newPelletHole));
    }
    
    public void CheckPelletHoles()
    {
        for (int i = 0; i < pelletHoles.Count; i++)
        {
            pelletHoles[i].existenceCountdown -= Time.deltaTime;

            if(pelletHoles[i].existenceCountdown <= 0)
            {
                GameObject.Destroy(pelletHoles[i].pelletHoleObj);
                pelletHoles.RemoveAt(i);
            }
        }
    }
}

