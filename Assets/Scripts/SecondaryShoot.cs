using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryShoot : MonoBehaviour
{
    private PlayerReferences playerRef;
    private WeaponStatTracker usedWeapon;
    private WeaponCycle weaponCycle;
    private int activeWeaponIndex;

    private void Awake()
    {
        usedWeapon = this.gameObject.GetComponent<WeaponStatTracker>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
        playerRef = this.gameObject.GetComponent<PlayerReferences>();
    }

    private void Update()
    {
        activeWeaponIndex = weaponCycle.activeWeaponIndex;
    }

    private void FixedUpdate()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (playerRef.playerInputs.RightClickInput && usedWeapon.weaponInfo[activeWeaponIndex].weapon.hasSecondaryFire && !usedWeapon.weaponInfo[activeWeaponIndex].secondaryCooldownActive)
        {

        }
    }
}
