using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryShoot : MonoBehaviour
{
    private WeaponStatTracker usedWeapon;
    private WeaponCycle weaponCycle;
    private int activeWeaponIndex;

    private void Awake()
    {
        usedWeapon = this.gameObject.GetComponent<WeaponStatTracker>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
    }

    private void Update()
    {
        activeWeaponIndex = weaponCycle.activeWeaponIndex;
    }
}
