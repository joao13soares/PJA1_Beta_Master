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

        // StartCoroutine(MeleeAttack());
        // StartCoroutine(TEST());
        TEST();
        
        return Result.Running;
    }

    private void TEST()
    {
        Ray ray = new Ray(transform.position,transform.forward);

        RaycastHit hit;

        Vector3 rayDirection = playerTransform.position - transform.position;
        if (Physics.Raycast 
            (transform.position,rayDirection,out hit,enemyScript.MeleeAttackRange))
        {
           
            
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            if(temp != null) Debug.Log("HIT PLAYER");
            temp?.TakeDamage(enemyScript.MeleeAttackDamage);
            
            
        }

        attackFinished = true;

    }
    
    
    IEnumerator MeleeAttack()
    {
        anim.clip = meleeAttackAnimation;
        anim.Play();
        yield return new WaitForSeconds(impactMomentOfAnimation);

        Ray ray = new Ray(transform.position,transform.forward);

        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit,enemyScript.MeleeAttackRange,enemyMask))
        {
            GameObject objectHit = hit.collider.gameObject;
            IDamageable temp = objectHit.GetComponent<IDamageable>();

            temp?.TakeDamage(enemyScript.MeleeAttackDamage);
        }
        
        yield return new WaitForSeconds(meleeAttackAnimation.length-impactMomentOfAnimation);
        attackFinished = true;
    }
}
