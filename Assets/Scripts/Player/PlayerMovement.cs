using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    MovementInfo info;

    
    
    
    [SerializeField]
    private float linearDrag = 0.95f, 
                  angularDrag = 0.70f;
  
    [SerializeField]
    private float walkVelocity = 2f;

    [SerializeField]
    private float mouseSensitivity = 0.1f;

    
    [SerializeField]
    private GameObject playerCamera;

     private Vector3 forwardVector => transform.forward;
     private Vector3 rightVector => transform.right;

    

    
     [SerializeField] private float distanceBetweenStepsSounds = 0.25f;
     Vector3 lastStepSoundPosition;
     [SerializeField]StepsSoundManager stepsSoundManager;
    
    
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Checks for camera 
        if (playerCamera != null) playerCamera = GameObject.Find("Main Camera");
        
        
        lastStepSoundPosition = this.transform.position;
        


    }

    // Update is called once per frame
    void Update()
    {
        
        Steering nextFrameSteering = new Steering();
        PlayerMovementUpdate(nextFrameSteering);
        PlayerLookUpdate(nextFrameSteering);
       

    }

 

    private void LateUpdate()
    {
        // Keeps up to date with the tranform position cause of colisions
        info.position = transform.position;
        
        // Check if plays sound manager according to new position
        if (Vector3.Distance(transform.position, lastStepSoundPosition) >= distanceBetweenStepsSounds)
        {
            stepsSoundManager.PlayAudioSample();
            lastStepSoundPosition = transform.position;
        }
    }

    private void PlayerMovementUpdate(Steering nextFrameSteering)
    {
        // Read Horizontal and Vertical Axes, and update velocity/rotation
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Next frame movement
        nextFrameSteering.linear = (forwardVector * vertical + rightVector * horizontal) ;

        // Updates vectors for next frame calculus
        info.velocity += nextFrameSteering.linear * walkVelocity ;

        // Do not exceed our max velocity
        info.velocity = Vector3.ClampMagnitude(info.velocity, walkVelocity) ;
        

         transform.position += info.velocity * Time.deltaTime;


         
         
         
        // Apply drag
        info.velocity *= linearDrag;

       


    }

    private void PlayerLookUpdate(Steering nextFrameSteering)
    {

        // Read Mouse Movement
        Vector2 mouseXY = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));


        // Gets mouse inputs
        if (Mathf.Abs(mouseXY.x) != 0 || Mathf.Abs(mouseXY.y) != 0)
        {
            // Gets this frame's value to rotate in X
            nextFrameSteering.angular.x = mouseXY.x ;
            // Gets this frame's value to rotate in Y(inverted axis)
            nextFrameSteering.angular.y = -1 * mouseXY.y ;
            
        }
        
        // Gets rotation for next frame
        info.rotation += nextFrameSteering.angular * mouseSensitivity ;

        
        // Update our orientation according to current rotation vector 
        info.orientation += info.rotation * Time.deltaTime ;
        
        //Normalize orientation
        info.orientation = AuxMethods.NormAngle(info.orientation);

        //Y axis clamp
        info.orientation.y = Mathf.Clamp(info.orientation.y, -Mathf.PI / 2, Mathf.PI / 2);

        // Rotates
        RotateTransform();
        
        // Add drag
        info.rotation *= angularDrag;
      
    }


    private void RotateTransform()
    {
        // Resets rotations for next frame(values are additive and dont reset after update)
        playerCamera.transform.localRotation = Quaternion.identity;
        transform.rotation = Quaternion.identity;
        
        // Rotates all player right-left
        this.transform.Rotate(transform.up, info.orientation.x * Mathf.Rad2Deg, Space.World);

        // Only rotates camera up-down
        playerCamera.transform.Rotate( playerCamera.transform.right, info.orientation.y * Mathf.Rad2Deg, Space.World);


    }
    
    
    

}
