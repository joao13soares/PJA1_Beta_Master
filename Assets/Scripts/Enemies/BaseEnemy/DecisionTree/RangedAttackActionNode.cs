using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackActionNode : ActionNode
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip rangedAttackAnimation;
    [SerializeField] private float impactMomentOfAnimation;
    [SerializeField] private LayerMask enemyMask;

    private bool attackFinished;

    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        enemyScript = GetComponent<Enemy>();
        attackFinished = false;
    }
    protected override void ExecuteAction()
    {
        if (attackFinished)
        {
            attackFinished = false;
            return;
        }

        // StartCoroutine(RangedAttack());
        // StartCoroutine(TEST());
        TEST();
        
        // // RANGED ATTACK LOGIC HERE
         Debug.Log("RANGED ATTACK");
    }
    
    private void TEST()
    {
       

        RaycastHit hit;

        Vector3 rayDirection = playerTransform.position - transform.position;
        if (Physics.Raycast 
            (transform.position,rayDirection,out hit,enemyScript.RangedAttackRange))
        {
           
            
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            if(temp != null) Debug.Log("HIT PLAYER");
            temp?.TakeDamage(enemyScript.RangedAttackDamage);
            
            
        }

        attackFinished = true;

    }
    
}
