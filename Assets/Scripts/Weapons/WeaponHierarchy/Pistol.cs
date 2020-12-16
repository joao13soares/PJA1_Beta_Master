using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
	void Awake()
	{
		defaultLocalPosition = this.transform.position;
		defaultLocalRotation = this.transform.rotation;

		this.type = "Pistol";

        
		weaponAnimationManager = new WeaponAnimationManager(animationComponent,holsterWeaponAnimation,drawWeaponAnimation,reloadWeaponAnimation);
		pelletHoleManager = new PelletHoleManager();
	}
	

}