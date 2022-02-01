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
    [SerializeField] private Text abilityText;
    [SerializeField] private Text cooldownText;

    private void Awake()
    {
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
        playerRef = this.gameObject.GetComponent<PlayerReferences>();
        shoot = this.gameObject.GetComponent<Shoot>();
    }

    private void Start()
    {
        abilityText.text = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryAbility.secondaryFireName;
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryAbility.secondaryCooldownToggle) cooldownText.text = string.Empty;
        else cooldownText.text = "READY";
    }

    private void FixedUpdate()
    {
        WeaponSwitchProcedures();
        PlayerInput();
        AbilityCooldown();
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
        if (wpn.secondaryAbility.secondaryFlies && wpn.secondaryAbility.secondaryStopsAtMousePosition) projectile = Instantiate(wpn.secondaryAbility.secondaryProjectile, playerRef.mousePos.VectorPointToShoot, Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        else projectile = Instantiate(wpn.secondaryAbility.secondaryProjectile, ProjectilePositionCalculate(), Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        AbilityProjectile proj = projectile.GetComponent<AbilityProjectile>();
        proj.target = playerRef.mousePos.VectorPointToShoot;
        proj.speed = wpn.secondaryAbility.secondaryProjectileSpeed;
        proj.damage = wpn.secondaryAbility.secondaryDamage;
        proj.lifeTime = wpn.secondaryAbility.secondaryLifetime;
        if (wpn.secondaryAbility.secondaryStopsAfterSetAmountOfTime)
        {
            proj.stopTime = wpn.secondaryAbility.stopAfterSeconds;
            proj.StopAfterTimeSetup();
        }
        if (wpn.secondaryAbility.secondaryStopsAtMousePosition && !wpn.secondaryAbility.secondaryStopsAfterSetAmountOfTime) proj.StopAtTargetLocation();
        if (wpn.secondaryAbility.secondaryCooldownToggle) weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive = true;
        proj.SetDurationInfos(true, wpn.secondaryAbility.secondaryDurationTime, wpn.secondaryAbility.playerIsStoppedDuringSecondaryDuration, wpn.secondaryAbility.normalShotsInteractWithSecondaryProjectile, projectileStartPosition);
        if (wpn.secondaryAbility.secondaryAmmoCost > 0) shoot.WeaponAmmoDecrease(wpn.secondaryAbility.secondaryAmmoCost);

    }
    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }

    private void AbilityCooldown()
    {
        for (int i = 0; i < weaponUsed.weaponInfo.Length; i++)
        {
            if (weaponUsed.weaponInfo[i].secondaryCooldownActive)
            {
                if (weaponUsed.weaponInfo[i].secondaryCooldownTimer > 0)
                {
                    weaponUsed.weaponInfo[i].secondaryCooldownTimer -= Time.fixedDeltaTime;
                    if (i == activeWeaponIndex) cooldownText.text = Math.Round(weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer, 2).ToString();
                }
                else
                {
                    weaponUsed.weaponInfo[i].secondaryCooldownTimer = weaponUsed.weaponInfo[i].weapon.secondaryAbility.secondaryCooldownTime;
                    if (i == activeWeaponIndex) cooldownText.text = "READY";
                    weaponUsed.weaponInfo[i].secondaryCooldownActive = false;
                }
            }
        }
    }

    private void WeaponSwitchProcedures()
    {
        if (activeWeaponIndex != weaponCycle.activeWeaponIndex)
        {
            activeWeaponIndex = weaponCycle.activeWeaponIndex;
            SetupAbilityCooldownText();
        }
    }

    private void SetupAbilityCooldownText()
    {
        abilityText.text = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryAbility.secondaryFireName;
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryAbility.secondaryCooldownToggle) cooldownText.text = string.Empty;
        else
        {
            if (weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer == weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryAbility.secondaryCooldownTime) cooldownText.text = "READY";
            else cooldownText.text = Math.Round(weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer, 2).ToString();
        }
    }
}
