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

    private bool isAttacking;
    private bool attackFinished;

    private void Awake()
    {
        anim = this.GetComponent<Animation>();
        enemyScript = GetComponent<Enemy>();
        isAttacking = false;
        attackFinished = false;
    }

    protected override void ExecuteAction()
    {
        if (attackFinished)
        {
            attackFinished = false;
            return;
        }
        
        if(!isAttacking)
        StartCoroutine(RangedAttack());
        // StartCoroutine(TEST());
        // TEST();

    }

    // private void TEST()
    // {
    //    
    //
    //     RaycastHit hit;
    //
    //     Vector3 rayDirection = playerTransform.position - transform.position;
    //     if (Physics.Raycast 
    //         (transform.position,rayDirection,out hit,enemyScript.RangedAttackRange))
    //     {
    //        
    //         
    //         GameObject objectHit = hit.collider.gameObject;
    //         IDamageable temp = objectHit.GetComponent<IDamageable>();
    //
    //         if(temp != null) Debug.Log("HIT PLAYER");
    //         temp?.TakeDamage(enemyScript.RangedAttackDamage);
    //         
    //         
    //     }
    //
    //     attackFinished = true;
    //
    // }

    IEnumerator RangedAttack()
    {
        isAttacking = true;
        
        anim.clip = rangedAttackAnimation;
        anim.Play();
        yield return new WaitForSeconds(impactMomentOfAnimation);


        RaycastHit hit;
        Vector3 rayDirection = playerTransform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit, enemyScript.RangedAttackRange,~enemyMask))
        {
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            Debug.Log(objectHit.name);
            temp?.TakeDamage(enemyScript.RangedAttackDamage);
        }

        yield return new WaitForSeconds(rangedAttackAnimation.length - impactMomentOfAnimation);
        
        attackFinished = true;
        isAttacking = false;
    }
}