using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
	[SerializeField]
	DisplayPanel displayPanel;
	Health playerHealth;
	
	
	
	// Start is called before the first frame update
	void  Awake()
	{
		playerHealth = new Health(100);
		displayPanel.HealthPlus += HealHealth;
	}


	private void DamageHealth()
	{
		

	}
	private void HealHealth()
	{

		playerHealth.currenthealth = playerHealth.maxHealth;
	}

	public float GetPlayerHP => playerHealth.currenthealth;
}
