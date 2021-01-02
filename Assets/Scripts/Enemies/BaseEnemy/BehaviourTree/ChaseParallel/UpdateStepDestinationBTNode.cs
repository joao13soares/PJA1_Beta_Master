using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(FieldOfViewDetection))]
public class UpdateStepDestinationBTNode : BTNode
{
    private bool isAlreadySubscribed;
    private Movement enemyMovement;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private Animation enemyAnimation;
    [SerializeField] private AnimationClip chasingAnimation;

    private FieldOfViewDetection fovDetection;

    protected override void Awake()
    {
        
        enemyMovement = GetComponent<Movement>();
        fovDetection = GetComponent<FieldOfViewDetection>();
        base.Awake();

    }
    
    
    public override void OnInterrupt()
    {
        playerMovement.Stepped -= UpdateDestination;
        isAlreadySubscribed = false;
    }

    public override Result Execute()
    {
        
        if(fovDetection.isPlayerDetected &&
           !isAlreadySubscribed)
        {
            playerMovement.Stepped += UpdateDestination;
            isAlreadySubscribed = true;

            enemyAnimation.clip = chasingAnimation;
            enemyAnimation.Play();

        }
        
        else if (!fovDetection.isPlayerDetected &&
                 isAlreadySubscribed)
        {
            playerMovement.Stepped -= UpdateDestination;
            isAlreadySubscribed = false;
        }

        return Result.Success;

    }

    private void UpdateDestination()
    {
       enemyMovement.UpdatePath(playerMovement.lastStepPosition);
    }
    
    
}
