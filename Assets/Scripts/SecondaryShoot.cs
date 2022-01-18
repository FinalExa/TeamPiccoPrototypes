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
    [SerializeField] private Text cooldownText;
    [SerializeField] private Text abilityText;

    private void Awake()
    {
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
        weaponCycle = this.gameObject.GetComponent<WeaponCycle>();
        playerRef = this.gameObject.GetComponent<PlayerReferences>();
        shoot = this.gameObject.GetComponent<Shoot>();
    }

    private void Start()
    {
        abilityText.text = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryFireName;
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryCooldownToggle) cooldownText.text = string.Empty;
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
        if (wpn.secondaryFlies && wpn.secondaryStopsAtMousePosition) projectile = Instantiate(wpn.secondaryProjectile, playerRef.mousePos.VectorPointToShoot, Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        else projectile = Instantiate(wpn.secondaryProjectile, ProjectilePositionCalculate(), Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        AbilityProjectile proj = projectile.GetComponent<AbilityProjectile>();
        proj.target = playerRef.mousePos.VectorPointToShoot;
        proj.speed = wpn.secondaryProjectileSpeed;
        proj.damage = wpn.secondaryDamage;
        proj.lifeTime = wpn.secondaryLifetime;
        if (wpn.secondaryStopsAtMousePosition) proj.StopAtTargetLocation();
        if (wpn.secondaryCooldownToggle) weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive = true;
        proj.SetDurationInfos(true, wpn.secondaryDurationTime, wpn.playerIsStoppedDuringSecondaryDuration, wpn.normalShotsInteractWithSecondaryProjectile, projectileStartPosition);
        if (wpn.secondaryAmmoCost > 0) shoot.WeaponAmmoDecrease(wpn.secondaryAmmoCost);

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
                    weaponUsed.weaponInfo[i].secondaryCooldownTimer = weaponUsed.weaponInfo[i].weapon.secondaryCooldownTime;
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
        abilityText.text = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryFireName;
        if (!weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryCooldownToggle) cooldownText.text = string.Empty;
        else
        {
            if (weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer == weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryCooldownTime) cooldownText.text = "READY";
            else cooldownText.text = Math.Round(weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownTimer, 2).ToString();
        }
    }
}
