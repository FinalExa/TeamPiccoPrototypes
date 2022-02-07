using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatTracker : MonoBehaviour
{
    [System.Serializable]
    public struct WeaponInfo
    {
        public Weapon weapon;
        [HideInInspector] public float shootDelayTimer;
        [HideInInspector] public bool shootReady;
        [HideInInspector] public float rechargeDelayTimer;
        [HideInInspector] public bool isRecharging;
        [HideInInspector] public int ammoCurrent;
        [HideInInspector] public int magazineCurrent;
        [HideInInspector] public GameObject projectileParent;
        [HideInInspector] public float chargedTimer;
        [HideInInspector] public float chargePercentage;
        [HideInInspector] public float hitscanVisibleTimer;
        [HideInInspector] public float secondaryCooldownTimer;
        [HideInInspector] public bool secondaryCooldownActive;
    }
    public WeaponInfo[] weaponInfo;

    private void Start()
    {
        GenerateWeapons();
    }

    private void GenerateWeapons()
    {
        for (int i = 0; i < weaponInfo.Length; i++)
        {
            weaponInfo[i].shootDelayTimer = weaponInfo[i].weapon.shootDelay;
            weaponInfo[i].shootReady = false;
            weaponInfo[i].rechargeDelayTimer = weaponInfo[i].weapon.rechargeDelay;
            weaponInfo[i].isRecharging = false;
            weaponInfo[i].ammoCurrent = weaponInfo[i].weapon.ammoMax;
            weaponInfo[i].magazineCurrent = weaponInfo[i].weapon.magazineSize;
            weaponInfo[i].projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent");
            weaponInfo[i].chargedTimer = weaponInfo[i].weapon.chargeTime;
            weaponInfo[i].hitscanVisibleTimer = weaponInfo[i].weapon.hitscanVisibleTime;
            if (weaponInfo[i].weapon.hasSecondaryFire && weaponInfo[i].weapon.secondaryAbility != null)
            {
                weaponInfo[i].secondaryCooldownTimer = weaponInfo[i].weapon.secondaryAbility.secondaryCooldownTime;
                weaponInfo[i].secondaryCooldownActive = false;
            }
        }
    }
}
