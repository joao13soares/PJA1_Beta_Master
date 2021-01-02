using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightOrientation : MonoBehaviour
{

    [SerializeField] private Transform playerCamera;
    private Transform fakePlayerTransform;
    
    [SerializeField] private float speed;

    private Vector3 offSet;
    
    void Awake()
    {
        fakePlayerTransform = transform;
        offSet = fakePlayerTransform.position - playerCamera.transform.position;


    }


    // Late update to follow position on each frame without freaking out
    void LateUpdate()
    {
        fakePlayerTransform.position = playerCamera.position + offSet;
        fakePlayerTransform.rotation = Quaternion.Slerp(fakePlayerTransform.rotation, playerCamera.transform.rotation, speed * Time.deltaTime);

      
    }
}