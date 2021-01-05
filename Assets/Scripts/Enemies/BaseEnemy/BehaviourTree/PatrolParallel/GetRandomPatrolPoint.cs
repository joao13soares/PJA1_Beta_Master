using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomPatrolPoint : BTNode
{
    [SerializeField]private List<Transform> patrolPoints;
    [SerializeField] private Movement enemyMovement;


    [SerializeField] private Animation enemyAnimation;
    [SerializeField] private AnimationClip walkingAnimation;
    private int lastRandom;

    private Vector3 randomPointForNewPath;


    public override Result Execute()
    {
        Debug.Log("NEED NEW PATH");
        enemyMovement.UpdatePath(GetRandomPatrolPosition());
        enemyAnimation.clip = walkingAnimation;
        enemyAnimation.Play();
        return Result.Success;
    }


    private Vector3 GetRandomPatrolPosition()
    {
        int randomNumber;
        do
        {
            randomNumber = Random.Range(0, patrolPoints.Count - 1);
            
        } while (randomNumber == lastRandom);

        lastRandom = randomNumber;

        return patrolPoints[randomNumber].position;
    }

}
