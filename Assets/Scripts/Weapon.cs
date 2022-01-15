using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 2)]
public class Weapon : ScriptableObject
{
    public float shootDelay;
    public float rechargeDelay;
    public bool endlessAmmo;
    public int ammoMax;
    public int magazineSize;
    public float projectileDamage;
    public float projectileSpeed;
    public GameObject projectile;
    public bool automaticWeapon;
    public bool shotgunWeapon;
    public int shotgunProjectileNumber;
    [Range(0, 90)] public float shotgunSpread;
    public float shotgunSpeedDifference;
}
