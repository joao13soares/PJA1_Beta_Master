using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PatrolState : State
{
    
    [SerializeField] private Animation enemyAnimation;
    [SerializeField] private AnimationClip walkingAnimation;
    [SerializeField]private Movement baseEnemyMovement;


    // [SerializeField]private int currentTargetIndex;
    // public int CurrentTargetIndex => currentTargetIndex;

    [SerializeField]private List<Transform> randomPatrolPoints;
    private int lastRandom;

    
    // [SerializeField] private PathFindingAStar pathFinding;
    // private List<Cell> currentPath;

    

    
    protected override void CreateActions()
    {
        onEnterActions.Add(EnterPatrolState);
        onStayActions.Add(PatrolAction);
    }

 
    private void PatrolAction()
    {
       
        baseEnemyMovement.MovementUpdate();

    }




    private void EnterPatrolState()
    {
        GetNewPath();
        enemyAnimation.Stop();
        enemyAnimation.clip = walkingAnimation;
        enemyAnimation.Play();

    } 


    private void GetNewPath()
    {
       
        // Gets random patrol point
        Vector3 randomPatrolPoint = GetRandomPatrolPoint();
        
        baseEnemyMovement.UpdatePath(randomPatrolPoint);
        
        // currentTargetIndex = 0;

    }

    private Vector3 GetRandomPatrolPoint()
    {
        int randomNumber;
        do
        {
             randomNumber = Random.Range(0, randomPatrolPoints.Count - 1);
            
        } while (randomNumber == lastRandom);

        lastRandom = randomNumber;

        return randomPatrolPoints[randomNumber].position;
    }

    
    // public int GetLastCellIndex()
    // {
    //     if (currentPath != null) return currentPath.Count-1;
    //     return 0;
    //
    // }
    //
    //
    // private void OnDrawGizmos()
    // {
    //     if (currentPath != null)
    //
    //         foreach (Cell c in currentPath)
    //         {
    //             Gizmos.DrawCube(c.Position,new Vector3(1f,1f,1f));
    //         }
    // }
}
