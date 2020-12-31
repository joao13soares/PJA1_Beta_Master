
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class MoveInChasePathBTNode : BTNode
{
     private Movement enemyMovement;

    protected override void Awake()
    {
        
        enemyMovement = GetComponent<Movement>();
        base.Awake();
    }

    public override Result Execute()
    {
        enemyMovement.MovementUpdate();

        bool isLastCell = enemyMovement.CurrentTargetIndex == enemyMovement.currentPath.Count - 1;

        if (isLastCell) return Result.Success;

        return Result.Running;
    }
}
