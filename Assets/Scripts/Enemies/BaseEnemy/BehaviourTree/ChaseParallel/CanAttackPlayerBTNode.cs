using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(FieldOfViewDetection))]

public class CanAttackPlayerBTNode : BTNode
{
    
    [SerializeField] private Transform playerTransform;
    private Enemy enemyScript;
    private FieldOfViewDetection fovDetection;

    protected override void Awake()
    {
        
        enemyScript = GetComponent<Enemy>();
        fovDetection = GetComponent<FieldOfViewDetection>();
        base.Awake();
    }

    public override Result Execute()
    {

        if (fovDetection.isPlayerDetected &&
            DistanceToPlayer() <= enemyScript.RangedAttackRange)
        {
            Debug.Log("IS IN RANGE ATTACK");
            return Result.Failure;

        }

        return Result.Success;
    }
    
    private float DistanceToPlayer()
    {
        Vector2 playerPosTo2D = new Vector2(playerTransform.position.x,playerTransform.position.z);
        Vector2 thisTransformTo2D = new Vector2(transform.position.x,transform.position.z);

        // Debug.Log(Vector2.Distance(playerPosTo2D, thisTransformTo2D));
        return Vector2.Distance(playerPosTo2D, thisTransformTo2D);
    }
}
