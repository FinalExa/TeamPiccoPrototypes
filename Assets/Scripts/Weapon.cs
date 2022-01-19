using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 2)]
public class Weapon : ScriptableObject
{
    [Header("Weapon Basic Data")]
    public string weaponName;
    public float shootDelay;
    public float rechargeDelay;
    public int ammoMax;
    public int magazineSize;
    public float projectileDamage;
    public float projectileSpeed;
    public float projectileLifetime;
    public GameObject projectile;
    [Header("Weapon Extra Options")]
    public bool endlessAmmo;
    public bool automaticWeapon;
    [Header("Shotgun Toggle")]
    public bool shotgunWeapon;
    public int shotgunProjectileNumber;
    [Range(0, 90)] public float shotgunSpread;
    public float shotgunSpeedDifference;
    [Header("Charged Weapon Toggle")]
    public bool chargedWeapon;
    public float chargeTime;
    public float chargedMaxDamage;
    [Header("Hitscan Weapon Toggle")]
    public bool hitscanWeapon;
    public bool hitscanLimitedRange;
    public float hitscanRange;
    public float hitscanVisibleTime;
    [Header("Secondary Fire")]
    [Tooltip("Activates secondary fire on the weapon, used with right click")]
    public bool hasSecondaryFire;
    public SecondaryAbility secondaryAbility;
}
