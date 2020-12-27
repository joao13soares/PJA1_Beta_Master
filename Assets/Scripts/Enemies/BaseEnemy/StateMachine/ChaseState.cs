using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class ChaseState : State
{
    private Vector3 playerPositionLastStep;
    [SerializeField]private PlayerMovement playerMovement;

    [SerializeField]private Movement baseEnemyMovement;

    [SerializeField]private FieldOfViewDetection fovDetection;


    private bool isLookingFor;

    private bool isSubscribed;

    
   
    protected override void CreateActions()
    {

        onEnterActions.Add(EnterChaseState);
        onStayActions.Add(ChaseAction);
        onExitActions.Add(ExitChaseState);

    }


    
    
    private void EnterChaseState()
    {
        playerMovement.Stepped += ChangeLastStepPosition;
        isSubscribed = true;
        
        playerPositionLastStep = playerMovement.lastStepPosition;

        baseEnemyMovement.UpdatePath(playerPositionLastStep);
    }
    
    private void ChaseAction()
    {

        if (!fovDetection.isPlayerDetected && isSubscribed)
        {
            playerMovement.Stepped -= ChangeLastStepPosition;
            isSubscribed = false;

        }
        else if (fovDetection.isPlayerDetected && !isSubscribed)
        {
            playerMovement.Stepped += ChangeLastStepPosition;
            isSubscribed = true;

        }


            
        
        baseEnemyMovement.MovementUpdate();




    }

    private void ExitChaseState()
    {
        playerMovement.Stepped -= ChangeLastStepPosition;
        isSubscribed = false;

    }

    
   
    
    private void ChangeLastStepPosition()
    {
        playerPositionLastStep = playerMovement.lastStepPosition;
        baseEnemyMovement.UpdatePath(playerPositionLastStep);

    } 
    
    
    
   
    
}
