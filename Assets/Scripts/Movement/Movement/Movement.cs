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


    [SerializeField] private List<Transform> randomPatrolPoints;
    private int randomTargetNumber;
    private int lastRandom;


    [SerializeField] private PathFindingAStar pathfinding;


    private List<Cell> currentPath;
    private List<Cell> nextPath;
    private int currentTargetIndex;


    void Awake()
    {
        currentPath = new List<Cell>();
        nextPath = null;

        steeringBehaviour = Instantiate(steeringBehaviour);

        currentTargetIndex = 0;
    }


    void Start()
    {
        GetNewEndTarget(true);
    }


    // Update is called once per frame
    public void MovementUpdate(Cell currentTarget)
    {
        UpdatePath();


        Steering steering = steeringBehaviour.GetSteering(this.movInfo, currentTarget.Position);

        UpdatePosition(steering);
        UpdateOrientation(steering);

        


      
    }

    private void LateUpdate()
    {
        movInfo.position = transform.position;
    }


    void UpdatePath()
    {
        int indexToStartCreatingNextPath = currentPath.Count - 3;

        if (currentTargetIndex == currentPath.Count)
        {
            currentTargetIndex = 0;
            currentPath = nextPath;
            nextPath = null;
            return;
        }

        if (currentTargetIndex == indexToStartCreatingNextPath && nextPath == null)
        {
            GetNewEndTarget(false);
        }
    }

    void GetNewEndTarget(bool isCurrent)
    {
      
        
        
        if (isCurrent)
            currentPath = pathfinding.FindPath(transform.position, randomPatrolPoints[randomTargetNumber].position);
        else
        {
            // nextPath = new List<Cell>();
            nextPath = pathfinding.FindPath(currentPath[currentPath.Count-1].Position, randomPatrolPoints[randomTargetNumber].position);
        }

       
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