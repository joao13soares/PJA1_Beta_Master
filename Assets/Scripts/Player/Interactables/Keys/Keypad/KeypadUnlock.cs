using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeypadUnlock : MonoBehaviour
{
    [SerializeField] private List<int> correctCode;
    public List<int> currentCode;



    [SerializeField]private DragDoor dragDoor;



    private bool CanUnlock()
    {

        if (currentCode.Count < correctCode.Count) return false;
        
        for (int i = 0; i < currentCode.Count; i++)
        {
            if ( currentCode[i] != correctCode[i]) return false;
        }

        return true;


    }


    public void AddNumber(int numberToAdd)
    {
        if (currentCode.Count >= correctCode.Count) return;
        
            currentCode.Add(numberToAdd);




    }

    public void ClearKey() => currentCode.Clear();

    public void EnterKey()
    {
        Debug.Log("ENTERKEY CLICKED");
        
        
        Debug.Log(CanUnlock());
        if (CanUnlock()) dragDoor.UnlockDoor();
    } 
    
}
