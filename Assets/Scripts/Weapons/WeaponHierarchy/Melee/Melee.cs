using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] Animation anim;
    [SerializeField] private AnimationClip attackAnimation;

    public override bool CanShoot => nextShotCooldown <= 0;
    public override bool CanReload => false;
    
    
    

    public void Awake()
    {
        this.type = "Knife"; 
        
        
        defaultLocalPosition = this.transform.position;
        defaultLocalRotation = this.transform.rotation;
    }

    public override void Attacking()
    {

        anim.clip = attackAnimation;
        anim.Play();

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