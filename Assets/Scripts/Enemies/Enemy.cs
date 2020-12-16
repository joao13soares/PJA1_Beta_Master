using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //The box's current health point total
    [SerializeField]
    public float currentHealth = 100.0f;
    [SerializeField]
    public AudioClip[] screams;


    public StateMachine baseEnemyStateMachine;

    private Color[] colors = {Color.black, Color.red, Color.yellow, Color.green};



    void Update()
    {
        if (currentHealth <= 0)
        {
            baseEnemyStateMachine.enabled = false;
            StartCoroutine(DestroyWithDelay());

        }


    }
    
    public void Damage(float damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;

        Debug.Log($"Enemy health {currentHealth}");
        
        int index = 0;
        if(currentHealth >= 50.0f) index = 2;
        else if(currentHealth >= 0.0f) index = 1;
        else index = 0;

        this.GetComponent<AudioSource>().clip = screams[index];
        this.GetComponent<AudioSource>().Play();

        this.GetComponent<Renderer>().material.color = colors[index];

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, kill it
            // GameObject targetBoxPrefab = Resources.Load("Prefabs/Target Box") as GameObject;
            // GameObject targetBox = Instantiate(targetBoxPrefab, new Vector3(0f, 3f, 0f), Quaternion.identity);
            
            StartCoroutine (DestroyWithDelay()); // Destroy with delay in order to be able to listen the audio clip and to see the material color change
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(2.0f);
        
        GameObject.Destroy(this.gameObject);
    }

    public bool isDying()
    {
        return currentHealth <= 0;
    }
}
