using System.Collections.Generic;
using UnityEngine;


public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Weapon correspondentWeapon;
    [SerializeField] private List<GameObject> bulletsUI;

    //ACESSORS
    public Weapon CorrespondentWeapon => correspondentWeapon;


    void Awake()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            bulletsUI.Add(transform.GetChild(i).gameObject);
        }
    }
    
    public void WeaponReloadedUIChange()
    {
        int reloadedBullets = correspondentWeapon.BulletsInCurrentMagazine;
        int magazineSize = correspondentWeapon.DefaultMagazineSize;

        for (int i = magazineSize - 1; i >= magazineSize - reloadedBullets; i--)
        {
            bulletsUI[i].SetActive(true);
        }
            
    }

    public void WeaponShotUIChange()
    {
        if(bulletsUI.Count != 0)
        bulletsUI[bulletsUI.Count - (correspondentWeapon.BulletsInCurrentMagazine + 1)].SetActive(false);

    }

    
}