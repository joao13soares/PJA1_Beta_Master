using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [SerializeField] private IAIControlable AIController;

    private Color[] colors = {Color.black, Color.red, Color.yellow, Color.green};

    private bool isDying;


    public delegate void EnemyAction();

    public event EnemyAction DamageTaken;

    private void Awake()
    {
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
        DamageTaken?.Invoke();


        if (currentHealth <= 0)
        {
            StartCoroutine(
                DestroyWithDelay()); // Destroy with delay in order to be able to listen the audio clip and to see the material color change
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        isDying = true;
        yield return new WaitForSeconds(2.0f);

        GameObject.Destroy(this.gameObject);
    }
}