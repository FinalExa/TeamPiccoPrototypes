using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryShoot : MonoBehaviour
{
    private PlayerReferences playerRef;
    private WeaponStatTracker weaponUsed;
    private WeaponCycle weaponCycle;
    private int activeWeaponIndex;
    [SerializeField] private GameObject projectileStartPosition;

    private void Awake()
    {
        weaponUsed = this.gameObject.GetComponent<WeaponStatTracker>();
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
        if (playerRef.playerInputs.RightClickInput && weaponUsed.weaponInfo[activeWeaponIndex].weapon.hasSecondaryFire && !weaponUsed.weaponInfo[activeWeaponIndex].secondaryCooldownActive)
        {
            LaunchAbility();
        }
    }

    private void LaunchAbility()
    {
        GameObject projectile = Instantiate(weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryProjectile, ProjectilePositionCalculate(), Quaternion.identity, weaponUsed.weaponInfo[activeWeaponIndex].projectileParent.transform);
        AbilityProjectile proj = projectile.GetComponent<AbilityProjectile>();
        proj.target = playerRef.mousePos.VectorPointToShoot;
        proj.speed = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryProjectileSpeed;
        proj.damage = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryDamage;
        proj.lifeTime = weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryLifetime;
        if (weaponUsed.weaponInfo[activeWeaponIndex].weapon.secondaryStopsAtMousePosition) proj.StopAtTargetLocation();
    }
    private Vector3 ProjectilePositionCalculate()
    {
        Vector3 position = projectileStartPosition.transform.position;
        return position;
    }
}
