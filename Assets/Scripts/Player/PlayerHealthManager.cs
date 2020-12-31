using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour,IDamageable
{
	[SerializeField]
	DisplayPanel displayPanel;
	Health playerHealth;
	
	
	
	// Start is called before the first frame update
	void  Awake()
	{
		playerHealth = new Health(100);
	}


	private void HealHealth(int healAmount)
	{

		playerHealth.currenthealth =
			Mathf.Clamp(playerHealth.currenthealth + healAmount, 0, playerHealth.maxHealth);
		
	}

	public float GetPlayerHP => playerHealth.currenthealth;
	
	
	public void TakeDamage(int damage)
	{
		Debug.Log(damage);
		playerHealth.currenthealth -= damage;
	}
}
