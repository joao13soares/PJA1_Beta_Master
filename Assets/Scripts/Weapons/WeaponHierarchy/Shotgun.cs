using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{

	void Awake()
	{
        defaultLocalPosition = this.transform.position;
        defaultLocalRotation = this.transform.rotation;

        this.type = "Shotgun";

        
        weaponAnimationManager = new WeaponAnimationManager(animationComponent, holsterWeaponAnimation,drawWeaponAnimation,reloadWeaponAnimation);
		pelletHoleManager = new PelletHoleManager();
        
	}

	public override void Attacking()
    {

        // Update the time when our player can fire next
        nextShotCooldown = defaultShotCooldown;
        
        // Reduce the bullets available
        bulletsinCurrentMagazine--;

        gunAudio.PlayOneShot(gunSounds[0]);

        
        
        // Create a vector at the center of our camera's viewport
        rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        for (int i = 0; i < pelletsPerBulletShot; i++)
        {
            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            Vector3 pelletDirection = WeaponSpread();

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, pelletDirection, out hit, gunRange))
            {
                // Get a reference to an Enemy script attached to the collider we hit
                Enemy enemy = hit.collider.GetComponent<Enemy>();

                float impactPercentageWithDistance = WeaponForceCalculation(hit);

                base.DoDamage(enemy, impactPercentageWithDistance);

                base.ApplyWeaponForce(hit, impactPercentageWithDistance);

                //base.HoleCreation(hit);
            }

          

            base.ShotEvent();
        }

        base.WeaponRecoil();
    }


    protected override Vector3 WeaponSpread()
    {
        float recoilSpreadFactor = recoil;
        
        float pelletSpreadRadius = Random.Range(0.0f, recoilSpreadFactor / 400.0f) * pelletSpreadRadiusMultiplier;  // Pellet spread radius increases with the current weapon X rotation, due to recoil
        Vector3 pelletSpreadAngle = this.transform.TransformDirection(Random.insideUnitCircle.normalized);  // TransformDirection from local space to world space | .normalized turns it into .onUnitCircle
        Vector3 pelletDirection = this.transform.forward - (this.transform.forward.y - playerCamera.transform.forward.y) * 0.5f * Vector3.up + pelletSpreadAngle * pelletSpreadRadius;
        return pelletDirection;
    }
}
