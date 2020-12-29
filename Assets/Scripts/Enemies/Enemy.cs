using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //The box's current health point total
    [SerializeField] float currentHealth;

    private float rangedAttackRange, meleeAttackRange;

    public float RangedAttackRange => rangedAttackRange;

    private AudioClip[] screams;


    public StateMachine baseEnemyStateMachine;

    private Color[] colors = {Color.black, Color.red, Color.yellow, Color.green};

    private bool isDying;


    public delegate void EnemyAction();

    public event EnemyAction DamageTaken;

    private void Awake()
    {
        baseEnemyStateMachine = this.GetComponent<StateMachine>();
        isDying = false;
    }

    void Update()
    {

        if (isDying) return;

        // baseEnemyStateMachine.ExecuteStateMachine();
        
        


    }
    
    public void ReceiveDamage(float damageAmount)
    {
        //subtract damage amount when ReceiveDamage function is called
        currentHealth -= damageAmount;

        Debug.Log($"Enemy health {currentHealth}");
        
        // int index = 0;
        // if(currentHealth >= 50.0f) index = 2;
        // else if(currentHealth >= 0.0f) index = 1;
        // else index = 0;
        //
        // this.GetComponent<AudioSource>().clip = screams[index];
        // this.GetComponent<AudioSource>().Play();
        //
        // this.GetComponent<Renderer>().material.color = colors[index];

        //Check if health has fallen below zero
        
        DamageTaken?.Invoke();
        
        
        if (currentHealth <= 0)
        {
            StartCoroutine (DestroyWithDelay()); // Destroy with delay in order to be able to listen the audio clip and to see the material color change
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        isDying = true;
        yield return new WaitForSeconds(2.0f);
        
        GameObject.Destroy(this.gameObject);
    }

    
}
