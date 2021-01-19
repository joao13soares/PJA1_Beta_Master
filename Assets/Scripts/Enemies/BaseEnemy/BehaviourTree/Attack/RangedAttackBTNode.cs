using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Enemy))]

public class RangedAttackBTNode : BTNode
{
    [SerializeField] private Transform playerTransform;
     private Enemy enemyScript;
    private Animation anim;
    [SerializeField] private AnimationClip rangedAttackAnimation;
    [SerializeField] private float impactMomentOfAnimation;
    [SerializeField] private LayerMask enemyMask;

    private bool isAttacking;
    private bool attackFinished;

    protected override void Awake()
    {
        anim = this.GetComponent<Animation>();
        enemyScript = GetComponent<Enemy>();
        attackFinished = false;
        isAttacking = false;
        base.Awake();
    }

    public override Result Execute()
    {
        if (attackFinished)
        {
            attackFinished = false;
            return Result.Success;
        }

        if(!isAttacking)
         StartCoroutine(RangedAttack());
      
        
        return Result.Running;
    }

    
  

    IEnumerator RangedAttack()
    {
        Debug.Log("HE IS GONNA ATTACK");
        isAttacking = true;
        anim.clip = rangedAttackAnimation;
        anim.Play();
        yield return new WaitForSeconds(impactMomentOfAnimation);


        RaycastHit hit;
        
        Vector3 rayDirection = playerTransform.position - transform.position;

        
        if (Physics.Raycast(transform.position, rayDirection, out hit, enemyScript.RangedAttackRange,enemyMask))
        {
            Debug.Log("HIT SOMETHING PLZ" + hit.collider.gameObject);
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            temp?.TakeDamage(enemyScript.RangedAttackDamage);
        }
        
        yield return new WaitForSeconds(rangedAttackAnimation.length-impactMomentOfAnimation);
        attackFinished = true;
        isAttacking = false;
    }


   
}