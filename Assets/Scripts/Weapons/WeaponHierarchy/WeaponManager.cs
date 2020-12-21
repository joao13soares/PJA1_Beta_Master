using System;
using System.Collections.Generic;
using UnityEngine;


public class WeaponManager : MonoBehaviour
{
    // Value obtained by hand testing best position
    private readonly Vector3 defaultOffSet = new Vector3(0.46f, -0.101f, 1.1f);

    [SerializeField] private List<Weapon> ownedGuns; // SERIALIZED FOR DEBUGGING

    /*[SerializeField]*/ private Weapon equippedWeapon; // SERIALIZED FOR DEBUGGING

    [SerializeField] private GameObject previousGun;

    // Input keycodes 
    [SerializeField] private readonly KeyCode shootKeycode = KeyCode.Mouse0;
    [SerializeField] private readonly KeyCode reloadKeycode = KeyCode.R;

    // ACESSORS
    public Weapon EquippedWeapon => equippedWeapon;

    // Events for UI change
    public delegate void WeaponAction(Weapon weaponInfo);
    public event WeaponAction WeaponSwitched;
    public event WeaponAction WeaponReloaded;
    public event WeaponAction WeaponShot;
    
    private void Awake()
    {
        ownedGuns = new List<Weapon>();
    }

    private void Update()
    {
        WeaponSelectionInput();

        WeaponActionInput();
    }

    private void WeaponSelectionInput()
    {
        // Check for input if u pressed any of the weapons associated keys
        foreach (Weapon weaponInfo in ownedGuns)
        {
            if (Input.GetKeyDown(weaponInfo.weaponKeyCode))
            {
                // If so switch for respective weapon
                SwitchWeapon(weaponInfo);
            }
        }
    }

    private void WeaponActionInput()
    {
        // Guard clause
        if (equippedWeapon == null) return;


        if (Input.GetKeyDown(shootKeycode) && equippedWeapon.CanShoot)
        {
                equippedWeapon.Attacking();
                WeaponShot?.Invoke(equippedWeapon);
            
            
        }

        if (Input.GetKeyDown(reloadKeycode) && equippedWeapon.CanReload)
        {
                equippedWeapon.Recharge();
                WeaponReloaded?.Invoke(equippedWeapon);

        }
    }


    private void SwitchWeapon(Weapon weaponToEquip)
    {
        GameObject weaponToEquipObject = weaponToEquip.gameObject;

        try
        {
            // Gives exception in the first weapon picked up
            previousGun.gameObject.SetActive(false);
        }
        catch (Exception)
        {
            // Exception happening in the first weapon picked up

        }
        finally
        {
            // Equips new weapon and deactivates previous equiped one
            weaponToEquipObject.SetActive(true);
            equippedWeapon = weaponToEquip;
            previousGun = weaponToEquipObject;
            
            WeaponSwitched?.Invoke(equippedWeapon);
        }
    }

    public void AddWeapon(GameObject newWeapon, Transform parentCamera)
    {
        Weapon weaponToAdd = newWeapon.GetComponent<Weapon>();

        ownedGuns.Add(weaponToAdd);

        // Sets position and rotation to parent
        newWeapon.transform.position = Vector3.zero;
        newWeapon.transform.SetParent(parentCamera);
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localPosition = defaultOffSet;
        newWeapon.transform.LookAt(parentCamera.position + (parentCamera.forward * 50f));

        // Initiates default position and rotation 
        weaponToAdd.OnPickUpDefaultInit(Quaternion.identity, defaultOffSet);

        //Switches for new weapon
        equippedWeapon = weaponToAdd;
        SwitchWeapon(weaponToAdd);
        
        
    }
}