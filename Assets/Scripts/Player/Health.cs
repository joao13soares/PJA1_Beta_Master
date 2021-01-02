using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{

	public float currenthealth;
	public float maxHealth;

	public Health(int health)
	{
		maxHealth = currenthealth = health;
	}

	public float GetHealth() => currenthealth;	
}
