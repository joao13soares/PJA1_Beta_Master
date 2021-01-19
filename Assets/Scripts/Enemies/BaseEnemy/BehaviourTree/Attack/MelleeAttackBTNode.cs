using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Enemy))]

public class MelleeAttackBTNode : BTNode
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Enemy enemyScript;
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip meleeAttackAnimation;
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
         StartCoroutine(MeleeAttack());
        // StartCoroutine(TEST());
        // TEST();
        
        return Result.Running;
    }

    
    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        anim.clip = meleeAttackAnimation;
        anim.Play();
        yield return new WaitForSeconds(impactMomentOfAnimation);


        RaycastHit hit;
        
        Vector3 rayDirection = playerTransform.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit, enemyScript.RangedAttackRange,enemyMask))
        
        {
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            temp?.TakeDamage(enemyScript.MeleeAttackDamage);
        }
        
        yield return new WaitForSeconds(meleeAttackAnimation.length-impactMomentOfAnimation);
        attackFinished = true;
        isAttacking = false;
    }
}
