using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    [SerializeField] MovementInfo movInfo;

    [SerializeField] SteeringBehaviour steeringBehaviour;

    [SerializeField, Range(0, 1)] float linearDrag = 0.95f,
        angularDrag = 0.95f;

    [SerializeField] float walkSpeed = 2f;

    [SerializeField] private float rotationSensitivity = 1.5f;


    // [SerializeField] private List<Transform> randomPatrolPoints;
    // private int randomTargetNumber;
    // private int lastRandom;


    [SerializeField] private PathFindingAStar pathfinding;
    public List<Cell> currentPath;
    private int currentTargetIndex;

    public int CurrentTargetIndex => currentTargetIndex;


    void Awake()
    {
        
        steeringBehaviour = Instantiate(steeringBehaviour);

        currentTargetIndex = 0;
    }


  

    public void MovementUpdate()
    {
        if (currentPath == null) return;
        currentTargetIndex = GetTargetIndex(currentPath[currentTargetIndex],currentTargetIndex);
        
        
        Cell currentTarget = currentPath[currentTargetIndex];

        
        
        Steering steering = steeringBehaviour.GetSteering(this.movInfo, currentTarget.Position);

        UpdatePosition(steering);
        UpdateOrientation(steering);
      
    }

    private void LateUpdate()
    {
        movInfo.position = transform.position;
    }


    public void UpdatePath(Vector3 endPosition)
    {
        currentPath = pathfinding.FindPath(this.transform.position, endPosition);
        currentTargetIndex = 0;
    }
    
    private int GetTargetIndex(Cell currentTarget,int currentIndex)
    {
        Vector3 convertThis2D = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 convertTargetTo2D = new Vector3(currentTarget.Position.x, 0f, currentTarget.Position.z);


        if (Vector3.Distance(convertThis2D, convertTargetTo2D) > 0.5f) return currentIndex;
        
        return Mathf.Clamp(++currentIndex,0,currentPath.Count-1);
        
        
    }

    
    
    void UpdatePosition(Steering nextFrameSteering)
    {
        // Adds linear to velocity
        movInfo.velocity += nextFrameSteering.linear * walkSpeed;

        // Do not exceed our max velocity
        movInfo.velocity = Vector3.ClampMagnitude(movInfo.velocity, walkSpeed);

        transform.position += new Vector3(movInfo.velocity.x, 0f, movInfo.velocity.z) * Time.deltaTime;
        // Apply drag
        movInfo.velocity *= linearDrag;
    }

    void UpdateOrientation(Steering nextFrameSteering)
    {
        movInfo.rotation += nextFrameSteering.angular * rotationSensitivity;

        // Do not forget to transform radians into degrees
        movInfo.orientation = AuxMethods.NormAngle(movInfo.orientation);

        //Reset rotation
        transform.rotation = Quaternion.identity;


        // Update our orientation 
        movInfo.orientation += movInfo.rotation * Time.deltaTime;
        

        transform.Rotate(transform.up, movInfo.orientation.x * Mathf.Rad2Deg);
        // transform.Rotate(transform.right, movInfo.orientation.y * Mathf.Rad:2Deg);

        movInfo.rotation *= angularDrag;
    }


    private void OnDrawGizmos()
    {
        if (currentPath != null)
        {
            foreach (Cell c in currentPath)
            {
                Gizmos.DrawCube(c.Position, Vector3.one);
            }
        }
    }
    
    
    
}