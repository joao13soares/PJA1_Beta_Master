using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject bat;

    public override bool CanShoot => nextShotCooldown <= 0;
    public override bool CanReload => false;

    public void Awake()
    {
        this.type = "Bat";
        
        defaultLocalPosition = this.transform.position;
        defaultLocalRotation = this.transform.rotation;
    }

    public override void Attacking()
    {

        anim.SetTrigger("OnAttacking");

        nextShotCooldown = defaultShotCooldown;

        rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        
        RaycastHit hit;
        
        if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
        {
            // Get a reference to an Enemy script attached to the collider we hit
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            float impactPercentageWithDistance = WeaponForceCalculation(hit);

            DoDamage(enemy, impactPercentageWithDistance);
        }
    }


 
}