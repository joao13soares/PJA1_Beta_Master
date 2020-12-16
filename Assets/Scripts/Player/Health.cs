using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health 
{

	public int currenthealth;
	public int maxHealth;

	public Health(int health)
	{
		maxHealth = currenthealth = health;
	}

	public int GetHealth() => currenthealth;	
}
