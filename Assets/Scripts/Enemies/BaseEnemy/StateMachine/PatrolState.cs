using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PatrolState : State
{
    
    [SerializeField]private Movement baseEnemyMovement;
    [SerializeField]private Transform baseEnemyTransform;


    [SerializeField]private int currentTargetIndex;
    public int CurrentTargetIndex => currentTargetIndex;

    [SerializeField]private List<Transform> randomPatrolPoints;
    private int lastRandom;

    
    [SerializeField] private PathFindingAStar pathFinding;
    private List<Cell> currentPath;

    
    protected override void CreateActions()
    {
        onEnterActions.Add(EnterPatrolState);
        onStayActions.Add(PatrolAction);
    }

 
    private void PatrolAction()
    {
        if (currentPath == null) return;
        currentTargetIndex = GetTargetIndex(currentPath[currentTargetIndex],currentTargetIndex);
        
        Cell currentTarget = currentPath[currentTargetIndex];
        
        baseEnemyMovement.MovementUpdate(currentTarget);

    }

    private int GetTargetIndex(Cell currentTarget,int currentIndex)
    {
        Vector3 convertThis2D = new Vector3(baseEnemyTransform.position.x, 0f, baseEnemyTransform.position.z);
        Vector3 convertTargetTo2D = new Vector3(currentTarget.Position.x, 0f, currentTarget.Position.z);


        if (Vector3.Distance(convertThis2D, convertTargetTo2D) > 0.5f) return currentIndex;
        
         return Mathf.Clamp(++currentIndex,0,currentPath.Count-1);
        
        
    }
    
    
    private void EnterPatrolState() => GetNewPath();


    private void GetNewPath()
    {
        Vector3 randomPatrolPoint = GetRandomPatrolPoint();
        currentPath = pathFinding.FindPath(baseEnemyTransform.position, randomPatrolPoint);
        currentTargetIndex = 0;

    }

    Vector3 GetRandomPatrolPoint()
    {
        int randomNumber;
        do
        {
             randomNumber = Random.Range(0, randomPatrolPoints.Count - 1);
            
        } while (randomNumber == lastRandom);

        lastRandom = randomNumber;

        return randomPatrolPoints[randomNumber].position;
    }

    public int GetLastCellIndex()
    {
        if (currentPath != null) return currentPath.Count-1;
        else return 0;

    }


    private void OnDrawGizmos()
    {
        if (currentPath != null)

            foreach (Cell c in currentPath)
            {
                Gizmos.DrawCube(c.Position,new Vector3(1f,1f,1f));
            }
    }
}
