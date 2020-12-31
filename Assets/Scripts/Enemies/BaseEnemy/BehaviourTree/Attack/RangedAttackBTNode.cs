using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Enemy))]

public class RangedAttackBTNode : BTNode
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip rangedAttackAnimation;
    [SerializeField] private float impactMomentOfAnimation;
    [SerializeField] private LayerMask enemyMask;

    private bool attackFinished;

    protected override void Awake()
    {
        anim = this.GetComponent<Animation>();
        enemyScript = GetComponent<Enemy>();
        attackFinished = false;
        base.Awake();
    }

    public override Result Execute()
    {
        if (attackFinished)
        {
            attackFinished = false;
            return Result.Success;
        }

        // StartCoroutine(RangedAttack());
        // StartCoroutine(TEST());
        TEST();
        
        return Result.Running;
    }

    
   private void TEST()
    {
        

        RaycastHit hit;


       
        Vector3 rayDirection = playerTransform.position - transform.position;

        if (Physics.Raycast
            (transform.position,rayDirection,out hit,enemyScript.RangedAttackRange))
        {
            Debug.Log("ATINGIU QQLR MERDA");
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();
            

            temp?.TakeDamage(enemyScript.RangedAttackDamage);
        }
        attackFinished = true;

    }

    IEnumerator RangedAttack()
    {
        anim.clip = rangedAttackAnimation;
        anim.Play();
        yield return new WaitForSeconds(impactMomentOfAnimation);

        Ray ray = new Ray(transform.position,transform.forward);

        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit,enemyScript.RangedAttackRange,enemyMask))
        {
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            temp?.TakeDamage(enemyScript.RangedAttackDamage);
        }
        
        yield return new WaitForSeconds(rangedAttackAnimation.length-impactMomentOfAnimation);
        attackFinished = true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position,transform.forward * enemyScript.RangedAttackRange);
    }
}