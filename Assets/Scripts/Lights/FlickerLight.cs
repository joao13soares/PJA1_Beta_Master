using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlickerLight : MonoBehaviour
{
    [SerializeField]private float upTime,
                                  currentTimer;
    [SerializeField]private float downTime;

    private const float minUpTime = 3.0f,
                        maxUpTime = 5.0f,
                        minDownTime = 0.01f,
                        maxDownTime = 0.1f;

    [SerializeField]private Light lightToTurnOff;
    
    private void Awake()
    {
        
        
        currentTimer = 0f;
        upTime = Random.Range(minUpTime, maxUpTime);
        downTime = Random.Range(minDownTime, maxDownTime);
    }


    private void Update()
    {

        if (!lightToTurnOff.enabled) return;

        if (currentTimer >= upTime)
        {
            StartCoroutine(TurnOffLight());
            currentTimer = 0f;
            
          upTime = Random.Range(minUpTime, maxUpTime);

        }
        else currentTimer += Time.deltaTime;
    }

    IEnumerator TurnOffLight()
    {
        lightToTurnOff.enabled = false;
        
        yield return new WaitForSeconds(downTime);
        
        downTime = Random.Range(minDownTime, maxDownTime);

        lightToTurnOff.enabled = true;

    }
}
