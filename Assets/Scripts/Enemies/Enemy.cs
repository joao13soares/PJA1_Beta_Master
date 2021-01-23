using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animation))]
public class Enemy : MonoBehaviour
{
    //The box's current health point total
    [SerializeField] float currentHealth;

    [SerializeField] private float rangedAttackRange;
    [SerializeField] private float meleeAttackRange;

    [SerializeField] private float rangedAttackDamage;
    [SerializeField] private float meleeAttackDamage;

    public float RangedAttackRange => rangedAttackRange;
    
    public float MeleeAttackRange => meleeAttackRange;
    public float RangedAttackDamage => rangedAttackDamage;

    public float MeleeAttackDamage => meleeAttackDamage;


    private AudioClip[] screams;


    private IAIControlable AIController;

    [SerializeField]
    private float _removeTime;
    private bool isDying;


     private Animation anim;
    [SerializeField] private AnimationClip deathAnimation;

    public delegate void EnemyAction();

    public event EnemyAction died;

    private void Awake()
    {
        anim = GetComponent<Animation>();
        AIController = this.GetComponent<IAIControlable>();
        isDying = false;
    }

    void Update()
    {
        if (isDying) return;

        AIController?.ExecuteAIControl();
    }

    public void ReceiveDamage(float damageAmount)
    {
        //subtract damage amount when ReceiveDamage function is called
        currentHealth -= damageAmount;
        


        if (currentHealth <= 0)
        {
            StartCoroutine(
                DestroyWithDelay()); // Destroy with delay in order to be able to listen the audio clip and to see the material color change
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        isDying = true;

        anim.clip = deathAnimation;
        anim.Play();
        
        died?.Invoke();
        
        
        yield return new WaitForSeconds(deathAnimation.length+_removeTime);
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
        
    }
}