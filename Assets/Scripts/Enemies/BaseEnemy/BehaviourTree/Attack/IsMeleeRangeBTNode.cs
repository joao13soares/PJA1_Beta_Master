using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class IsMeleeRangeBTNode : BTNode
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Enemy enemyScript;


    protected override void Awake()
    {
        enemyScript = GetComponent<Enemy>();
        base.Awake();
    }

    public override Result Execute()
   {
      if (DistanceToPlayer() <= enemyScript.MeleeAttackRange) return Result.Success;

      return Result.Failure;
   }
   
   private float DistanceToPlayer()
   {
       Vector2 playerPosTo2D = new Vector2(playerTransform.position.x,playerTransform.position.z);
       Vector2 thisTransformTo2D = new Vector2(transform.position.x,transform.position.z);

       return Vector2.Distance(playerPosTo2D, thisTransformTo2D);
   }
}
