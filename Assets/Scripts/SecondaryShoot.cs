using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SecondaryShoot : MonoBehaviour
{
    private PlayerReferences playerRef;
    private WeaponStatTracker weaponUsed;
    private WeaponCycle weaponCycle;
    private Shoot shoot;
    private int activeWeaponIndex;
    [SerializeField] private GameObject projectileStartPosition;
    [SerializeField] private Text abilityCooldownText;

    private void Awake()
    {
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
        playerRef = this.gameObject.GetComponent<PlayerReferences>();
        shoot = this.gameObject.GetComponent<Shoot>();
    }

    private void Start()
    {
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryCooldownToggle) abilityCooldownText.text = string.Empty;
        else abilityCooldownText.text = "READY";
    }

    private void Update()
    {
        activeWeaponIndex = weaponCycle.activeWeaponIndex;
    }

    private void FixedUpdate()
    {
        PlayerInput();
        if (weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive) AbilityCooldown();
    }

    private void PlayerInput()
    {
        if (playerRef.playerInputs.RightClickInput && weaponUsed.weaponInfo[activeWeaponIndex].weapon.hasSecondaryFire && !weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive)
        {
            LaunchAbility();
        }
    }

    private void LaunchAbility()
    {
        GameObject projectile;
        Weapon wpn = weaponUsed.weaponInfo[activeWeaponIndex].weapon;
        if (wpn.secondaryFlies && wpn.secondaryStopsAtMousePosition) projectile = Instantiate(wpn.secondaryProjectile, playerRef.mousePos.VectorPointToShoot, Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        else projectile = Instantiate(wpn.secondaryProjectile, ProjectilePositionCalculate(), Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        AbilityProjectile proj = projectile.GetComponent<AbilityProjectile>();
        proj.target = playerRef.mousePos.VectorPointToShoot;
        proj.speed = wpn.secondaryProjectileSpeed;
        proj.damage = wpn.secondaryDamage;
        proj.lifeTime = wpn.secondaryLifetime;
        if (wpn.secondaryStopsAtMousePosition) proj.StopAtTargetLocation();
        if (wpn.secondaryCooldownToggle) weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive = true;
        if (wpn.secondaryDurationToggle) proj.SetDurationInfos(true, wpn.secondaryDurationTime, wpn.effectStartsAfterTimer, wpn.playerIsStoppedDuringSecondaryDuration);
        if (wpn.secondaryAmmoCost > 0) shoot.WeaponAmmoDecrease(wpn.secondaryAmmoCost);

    }
    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }

    private void AbilityCooldown()
    {
        if (weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer > 0)
        {
            weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer -= Time.fixedDeltaTime;
            abilityCooldownText.text = Math.Round(weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer, 2).ToString();
        }
        else
        {
            weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryCooldownTime;
            abilityCooldownText.text = "READY";
            weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive = false;
        }
    }
}
